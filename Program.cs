using JSONAPI.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONAPI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        ///  If run from command line: JSONAPI runTestSuite <testSuiteName> <username>:<password>
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    //Console.WriteLine("Hello Args[0]=" + args[0] + ".Args[1]=" + args[1] + ".Args[2]=" + args[2]);
                    string result = Utilities.CommandLine.HandleCommandLine(args);
                    Console.WriteLine(result);
                }
                else
                {
                    //Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    string userName = System.Environment.GetEnvironmentVariable("jsonapiuserName", EnvironmentVariableTarget.User);
                    if (userName == null) userName = "none";
                    if (userName != "none")
                    {
                        JSONAPIForm mainForm = new JSONAPIForm();
                        mainForm.WindowState = FormWindowState.Maximized;
                        Application.Run(mainForm);
                    }
                    else
                    {
                        Admin.frmLogin loginForm = new Admin.frmLogin();

                        Application.Run(loginForm);
                        if (loginForm.profileUpdatesNeeded)
                        {
                            //activate
                            Admin.frmUserProfile userProfileForm = new Admin.frmUserProfile();
                            Application.Run(userProfileForm);
                            if (userProfileForm.registrationSuccessful)
                            {
                                Admin.frmLogin loginForm2 = new Admin.frmLogin();
                                Application.Run(loginForm2);
                                if (loginForm2.loginSuccessful)
                                {
                                    JSONAPIForm mainForm = new JSONAPIForm();
                                    mainForm.WindowState = FormWindowState.Maximized;
                                    Application.Run(mainForm);
                                }
                            }
                        }
                        if (loginForm.loginSuccessful)
                        {
                            JSONAPIForm mainForm = new JSONAPIForm();
                            mainForm.WindowState = FormWindowState.Maximized;
                            Application.Run(mainForm);
                        }
                    }
                }
            }
            catch (Exception le)
            {
                Utilities.Logs.NewLogItem("UI Load Issue: " + le.ToString(), System.Diagnostics.TraceEventType.Critical);
            }
        }
    }
}
