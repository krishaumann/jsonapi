using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSONAPI.Utilities
{
    public static class CommandLine
    {
        //JSONAPI runTestSuite <testSuiteName> <userName>:<password>
        //E.g. JSONAPI.exe runTestSuite Demo new_user:new_user
        // args[1] runTestSuite
        // args[2] TestSuiteName
        // args[3] username:password
        public static string[] commandLineArray = new string[] { "runTestSuite" };
        public static string HandleCommandLine(string[] args)
        {
            string returnValue = "";
            bool validCommandLineFlag = false;
            bool validTestSuite = false;
            bool userExist = false;
            string commandLineStr = "";
            try
            {
                for (int c = 0; c < commandLineArray.Length; c++)
                {
                    commandLineStr += commandLineArray[c];
                    if (args[1] == commandLineArray[c])
                    {
                        validCommandLineFlag = true;
                    }
                }
                if (validCommandLineFlag)
                {
                    if (args[2] != null)
                    {
                        validTestSuite = TestSuite.IsValidTestSuiteName(args[2]);
                        if (validTestSuite)
                        {
                            if (args[2] != null)
                            {
                                string[] userArray = args[3].Split(":".ToCharArray());
                                userExist = Users.ValidateUser(userArray[0], userArray[1]);
                                if (userExist)
                                {                            
                                    System.Environment.SetEnvironmentVariable("jsonapiuserName", userArray[0], EnvironmentVariableTarget.User);
                                    var tokenSource = new CancellationTokenSource();
                                    var token = tokenSource.Token;
                                    Task task1 = Task.Factory.StartNew(() => TestSuite.RunTestSuite(args[2]));
                                    Task task2 = Task.Factory.StartNew(() => UpdateConsole(100));
   
                                    int taskIndex = Task.WaitAny(task1, task2);
                                    tokenSource.Cancel();
                                    returnValue = "Executed Successfully.  See Output for more details.";
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
            catch (Exception e)
            {
               Utilities.WriteLogItem("HandleCommandLine failed; " + e.ToString(), System.Diagnostics.TraceEventType.Error);
            }
            return returnValue;
        }

        public static void UpdateConsole(int millisecs)
        {
            while (true)
            {
                Thread.Sleep(millisecs);
                Console.Write(".");
            }
        }
    }
}
