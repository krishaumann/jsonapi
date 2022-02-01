using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSONAPI.Utilities
{
    public static class CommandLine
    {
        //JSONAPI runTestSuite <testSuiteName> <userName>:<password>
        //E.g. JSONAPI.exe runTestSuite Demo new_user:new_user
        // args[1] runTestSuite - Mandatory
        // args[2] TestSuiteName - Mandatory
        // args[3] username:password - Mandatory
        // args[4] parallel users - Optional, Default = 1
        // args[5] duration of run - Optional, if not specified, tests will only be executed once
        public static string[] commandLineArray = new string[] { "runTestSuite" };
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
            Random rnd = new Random();
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
                                    userExist = Users.ValidateUser(userArray[0], userArray[1]);
                                    if (userExist)
                                    {
                                        if (args[4] == "1" & args[5] == "1")
                                        {
                                            ThreadPool.QueueUserWorkItem(_ => StartTasks(args[3], args[2]));
                                            Thread.Sleep(1000);
                                            returnValue = "Executed Successfully.  See Output for more details.";
                                        }
                                        else
                                        {
                                            if (int.TryParse(args[5], out tempNr) & int.TryParse(args[4], out tempNr))
                                            {
                                                Dictionary<string, int> threadList = TestSuite.GetConcurrentUsers(args[2]);
                                                foreach (KeyValuePair<string, int> entry in threadList)
                                                {
                                                    ThreadStart testThread = delegate { RunUser(entry.Value); };
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

        static void RunUser(int n)
        {
            ParallelLoopResult parallelLoopResult = Parallel.For(0, n,
                (int i, ParallelLoopState loopControl) =>
                {
                    if (i > n - 2)
                    {
                        loopControl.Stop();
                    }
                    else
                    {
                        Logs.NewLogItem("Working on " + i, TraceEventType.Information);
                        //TO Do call the Send Request
                    }
                });

            if (!parallelLoopResult.IsCompleted)
            {
                Logs.NewLogItem("Problem with theread parallel loop.", TraceEventType.Error);
            }
        }
    }
}
