using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace JSONAPI.Utilities
{
    public static class CommandLine
    {
        //JSONAPI runTestSuite <testSuiteName> <userName>:<password>
        //E.g. JSONAPI.exe runTestSuite PerfTest test:test 5 5
        // args[1] runTestSuite - Mandatory
        // args[2] TestSuiteName - Mandatory
        // args[3] username:password - Mandatory
        // args[4] parallel users - Optional, Default = 1
        // args[5] duration of run - Optional, if not specified, tests will only be executed once
        // args[6] delay between each message - Optional, if not specified, value is 1000ms
        public static string[] commandLineArray = new string[] { "runTestSuite" };
        private static List<TestSuite.PerformanceTestDetail> perfTests = new List<TestSuite.PerformanceTestDetail>();
        public static bool timerRunning = false;
        private static List<Variables.SessionVariable> sessionVariables = new List<Variables.SessionVariable>();

        public static void StartTasks(string userName, string testSuiteName)
        {
            System.Environment.SetEnvironmentVariable("jsonapiuserName", userName, EnvironmentVariableTarget.User);
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Task task1 = Task.Factory.StartNew(() => TestSuite.RunTestSuite(testSuiteName));
            Task task2 = Task.Factory.StartNew(() => UpdateConsole(100));

            int taskIndex = Task.WaitAny(task1, task2);
            tokenSource.Cancel();
        }

        public static void UpdateConsole(int millisecs)
        {
            while (true)
            {
                Thread.Sleep(millisecs);
                Console.Write(".");
            }
        }

        public static string HandleCommandLine(string[] args)
        {
            int count = 0;

            string returnValue = "";
            bool validCommandLineFlag = false;
            bool validTestSuite = false;
            bool userExist = false;
            string commandLineStr = "";
            int tempNr = 0;
            try
            {
                if (args[4] == null & args[5] == null)
                {
                    args[4] = "1";
                    args[5] = "1";
                }
                if (args[4] != null & args[5] == null)
                {
                    args[4] = "1";
                    if (!int.TryParse(args[5], out tempNr)) args[5] = "1";
                }
                if (args[6] == null)
                {
                    args[6] = "1000";
                }
                for (int c = 0; c < commandLineArray.Length; c++)
                {
                    commandLineStr += commandLineArray[c];
                    if (args[1] == commandLineArray[c])
                    {
                        validCommandLineFlag = true;
                    }
                    if (validCommandLineFlag)
                    {
                        if (args[2] != null)
                        {
                            validTestSuite = TestSuite.IsValidTestSuiteName(args[2]);
                            if (validTestSuite)
                            {
                                if (args[3] != null)
                                {
                                    string[] userArray = args[3].Split(":".ToCharArray());
                                    userExist = true; //Users.ValidateUser(userArray[0], userArray[1]);
                                    if (userExist)
                                    {
                                        if (args[4] == "1" & args[5] == "1")
                                        {
                                            ThreadPool.QueueUserWorkItem(_ => StartTasks(userArray[0], args[2]));
                                            Thread.Sleep(1000);
                                            returnValue = "Executed Successfully.  See Output for more details.";
                                        }
                                        else
                                        {
                                            if (int.TryParse(args[5], out tempNr) & int.TryParse(args[4], out tempNr))
                                            {
                                                BuildPerformanceTestMessages(args[2], int.Parse(args[4]));
                                                string testRunName = "PerformanceTest" + DateTime.Now.ToString("yyyyMMddhh24mmss");
                                                //ExecutionResult.NewTestRun(testRunName, args[2], "");
                                                StoredProcedureTimer.Start(int.Parse(args[5]));
                                                for (int user = 0; user < perfTests.Count; user++)
                                                {
                                                    ThreadStart testThread = delegate { RunUser(user, args[2], int.Parse(args[6]), testRunName); };
                                                    new Thread(testThread).Start();
                                                }

                                                returnValue = "Executed Successfully.  See Output for more details.";
                                            }
                                            else
                                            {
                                                returnValue = "Invalid number of total concurrent users and/or execution time in minutes given.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        returnValue = "Invalid credentials given.";
                                    }
                                }
                            }
                            else
                            {
                                returnValue = "Invalid Test Suite Name given.";
                            }
                        }
                    }
                    else
                    {
                        returnValue = "Invalid command given. Valid options are: " + commandLineStr;
                    }
                }
                count = int.Parse(args[4]);
            }
            catch (Exception e)
            {
                Logs.NewLogItem("Number of concurrent threads not specified." + e.Message, System.Diagnostics.TraceEventType.Information);
                count = 1;
                returnValue = "Unexpected error on running command " + commandLineStr;
            }
            return returnValue;
        }

        static void RunUser(int n, string testSuiteName, int delay, string testRunName)
        {
  
            ParallelLoopResult parallelLoopResult = Parallel.For(0, n,
                (int i, ParallelLoopState loopControl) =>
                {
                    Thread.Sleep(delay);

                    if (!timerRunning)
                    {
                        loopControl.Stop();
                        StoredProcedureTimer.Stop();
                    }
                    else
                    {
                        string prevTestName = "";
                        Logs.NewLogItem("Working on " + i, TraceEventType.Information);
                        while (StoredProcedureTimer.status)
                        {
                            //TO Do call the Send Request
                            foreach (TestSuite.PerformanceTestDetail perfTest in perfTests)
                            {
                                if (perfTest.UserNumber == i)
                                {
                                    for (int c = 0; c < perfTest.PerformanceTestMessageDetailList.Count; c++)
                                    {
                                        string execStatus = "Pass";
                 
                                        var watch = System.Diagnostics.Stopwatch.StartNew();
                                        bool messageStatus = Utilities.SendPerformanceTestMessage(perfTest.PerformanceTestMessageDetailList[c].TestName, perfTest.PerformanceTestMessageDetailList[c].DetailRequest, perfTest.UserNumber);
                                        watch.Stop();
                                        var elapsedMs = watch.ElapsedMilliseconds;
                                        if (messageStatus) execStatus = "Pass";
                                        else execStatus = "Fail";
                                        ExecutionResult.AddPerformanceTestResult(testRunName, testSuiteName, perfTest.PerformanceTestMessageDetailList[c].TestName, c, "Response", "Success", elapsedMs.ToString(), execStatus);
                                        //TestSuite.UpdateTestStatus_NewInstance(testSuiteName, perfTest.PerformanceTestMessageDetailList[c].TestName, execStatus, "", "", "", "", c, 100, false);
                                        Logs.NewLogItem("Sending message " + c.ToString() + " as user " + i.ToString(), TraceEventType.Information);
                                        prevTestName = perfTest.PerformanceTestMessageDetailList[c].TestName;
                                    }
                                }
                            }
                        }
                    }
                    if (loopControl.IsStopped)
                    {
                        return;
                    }
                });

            if (!parallelLoopResult.IsCompleted)
            {
                Logs.NewLogItem("Problem with theread parallel loop.", TraceEventType.Error);
            }
        }

        public static void UpdateSessionVariableValue(int userName, string variableName, string updateValue)
        {
            foreach (var item in sessionVariables.ToList())
            {
                if (item.UserNr == userName & item.VariableName == variableName)
                    item.SavedValue = updateValue;
            }
        }
        public static List<Variables.SessionVariable> GetSessionVariables(int userName)
        {
            List<Variables.SessionVariable> returnList = new List<Variables.SessionVariable>();
            foreach (var item in sessionVariables.ToList())
            {
                if (item.UserNr == userName) returnList.Add(item);
            }
            return returnList;
        }


        public static void NewSessionVariableValue(int userName, string variableName, string updateValue, string replaceWhere, string searchForElement)
        {
            Variables.SessionVariable itemVariable = new Variables.SessionVariable(userName, variableName, updateValue, replaceWhere, searchForElement, "", 0);
            sessionVariables.Add(itemVariable);
        }

        public static string GetSessionVariableValue(int userName, string variableName)
        {
            string returnValue = "";
            foreach (var item in sessionVariables.ToList())
            {
                if (item.UserNr == userName & item.VariableName == variableName)
                    returnValue = item.SavedValue.ToString();

            }
            return returnValue;
        }
        public static string GetSessionVariableReplaceWhere(int userName, string variableName)
        {
            string returnValue = "";
            foreach (var item in sessionVariables.ToList())
            {
                if (item.UserNr == userName & item.VariableName == variableName)
                    returnValue = item.ReplaceWhere.ToString();

            }
            return returnValue;
        }


        public static void BuildPerformanceTestMessages(string testSuiteName, int totalUsers)
        {
            Dictionary<string, int> concurrentUserDetails = TestSuite.GetTestScenarioWeigthing(testSuiteName);
            List<Variables.VariableList> variableList = Variables.GetVariableDocuments();
            Dictionary<string, int[]> availableUserDetails = new Dictionary<string, int[]>();
            foreach (var item in concurrentUserDetails.ToList())
            {
                int[] numArray = new int[] { (int)Math.Ceiling((float)item.Value / 100 * totalUsers), (int)Math.Ceiling((float)item.Value / 100 * totalUsers) };
                availableUserDetails.Add(item.Key, numArray);
            }
            string testName = "";
            TestSuite.Test testDetails;
            List<TestSuite.PerformanceTestMessageDetail> performanceTestMessageList = new List<TestSuite.PerformanceTestMessageDetail>();
            sessionVariables.Clear();
            for (int c = 1; c <= totalUsers; c++)
            {
                foreach (var variable in variableList.ToList())
                {
                    Variables.SessionVariable itemVariable = new Variables.SessionVariable(c, variable.VariableName, variable.SavedValue, variable.ReplaceWhere, variable.SearchForElement, "", 0);
                    sessionVariables.Add(itemVariable);
                }
                foreach (var item in availableUserDetails.ToList())
                {
                    testName = item.Key;
                    availableUserDetails[testName][1]--;
                    if (availableUserDetails[testName][1] >= 0)
                    {
                        testDetails = TestSuite.GetTestDetails(testName);
                        List<string> detailMessages = Utilities.GenerateBatchDetail(testDetails.Input);

                        foreach (string detailMessage in detailMessages)
                        {
                            TestSuite.PerformanceTestMessageDetail perfMessage = new TestSuite.PerformanceTestMessageDetail(testName, detailMessage);
                            performanceTestMessageList.Add(perfMessage);
                        }
                    }
                }
                TestSuite.PerformanceTestDetail perfTest = new TestSuite.PerformanceTestDetail(c, performanceTestMessageList);
                perfTests.Add(perfTest);
            }
        }

        public static class StoredProcedureTimer
        {
            static System.Timers.Timer SPTimer;
            static bool iscreated = false;


            // Create the timer
            static void CreateTimer(int interval)
            {
                // Set multiple in seconds
                SPTimer = new System.Timers.Timer(1000 * 60 * interval);
                SPTimer.Elapsed += new ElapsedEventHandler(SPTimer_Elapsed);

                //enable
                SPTimer.Enabled = true;

                // Stop garbage collection being a right gay
                GC.KeepAlive(SPTimer);

                iscreated = true;
            }

            static void SPTimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                // do stuff
                timerRunning = false;
                Console.WriteLine("Timer completed.");
            }

            // Start the timer
            public static void Start(int interval)
            {
                if (iscreated == false)
                {
                    // create timer
                    CreateTimer(interval);
                }
                else
                {
                    // set timer
                    SPTimer.Interval = (1000 * 60 * interval);
                    // re-enable timer
                    SPTimer.Enabled = true;
                }

                // Update isrunning
                timerRunning = true;
            }

            // Stop the timer
            public static void Stop()
            {
                // only attempt to stop the timer if it exists.
                if (iscreated == true)
                {
                    SPTimer.Enabled = false;
                }

                // Update isrunning
                timerRunning = false;
            }

            // Check the timer is running
            public static bool status
            {
                get
                {
                    return timerRunning;
                }
            }
        }
    }
}
