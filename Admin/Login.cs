using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Admin
{
    public partial class frmLogin : Form
    {
        public int loginCounter = 0;
        public bool loginSuccessful = false;
        public bool profileUpdatesNeeded = false;
        public int executionCounter = 0;

        public frmLogin()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            System.Environment.SetEnvironmentVariable("jsonapiregisterusername", "none", EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", "none", EnvironmentVariableTarget.User);
        }

        private void AddEventHandlers()
        {
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (userName.Length > 0 & password.Length > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (chkKeepSession.Checked)
                {
                    System.Environment.SetEnvironmentVariable("keepLoggedIn", "yes", EnvironmentVariableTarget.User);
                }
                else
                {
                    System.Environment.SetEnvironmentVariable("keepLoggedIn", "none", EnvironmentVariableTarget.User);
                }
                Utilities.Users.User tempUser = Utilities.Users.GetNewUser(userName);
                //If user exist
                if (tempUser.UserName != "")
                {
                    //If user registered
                    if (tempUser.UserStatus)
                    {
                        bool loginResult = Utilities.Users.ValidateUser(txtUserName.Text, txtPassword.Text);
                        if (!loginResult & this.loginCounter <= 3)
                        {
                            MessageBox.Show("Invalid login credentials supplied.  Please try again.");
                            this.loginCounter++;
                            this.loginSuccessful = false;
                        }
                        if (this.loginCounter == 3)
                        {
                            Utilities.Users.DeactivateUser(txtUserName.Text);
                            MessageBox.Show("User Account locked. Contact the JSONAPI Support Team:  jsonapi.unmanned@gmail.com");
                            System.Environment.SetEnvironmentVariable("jsonapiuserName", "none", EnvironmentVariableTarget.User);
                            this.loginSuccessful = false;
                        }
                        if (loginResult)
                        {
                            System.Environment.SetEnvironmentVariable("jsonapiuserName", tempUser.UserName, EnvironmentVariableTarget.User);
                            System.Environment.SetEnvironmentVariable("jsonapiregisterusername", "none", EnvironmentVariableTarget.User);
                            System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", "none", EnvironmentVariableTarget.User);
                            System.Environment.SetEnvironmentVariable("workfolder", tempUser.UserHome, EnvironmentVariableTarget.User);
                            this.loginSuccessful = true;
                            this.profileUpdatesNeeded = false;
                            Close();
                        }
                        else
                        {
                            this.loginSuccessful = false;
                            this.profileUpdatesNeeded = false;
                        }
                    }
                    else
                    {
                        //If not activated then activate
                        System.Environment.SetEnvironmentVariable("jsonapiregisterusername", tempUser.UserName, EnvironmentVariableTarget.User);
                        System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", Utilities.Users.Decrypt(tempUser.UserPassword), EnvironmentVariableTarget.User);

                        this.profileUpdatesNeeded = true;
                        this.loginSuccessful = false;
                        System.Environment.SetEnvironmentVariable("jsonapiuserName", "none", EnvironmentVariableTarget.User);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("User does not exist. You will be directed to the Registration screen.");
                    this.profileUpdatesNeeded = true;
                    this.loginSuccessful = false;
                    if (txtUserName.Text != "") userName = txtUserName.Text;
                    if (txtPassword.Text != "") password = txtPassword.Text;
                    System.Environment.SetEnvironmentVariable("jsonapiregisterusername", userName, EnvironmentVariableTarget.User);
                    System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", password, EnvironmentVariableTarget.User);
                    Close();
                }
                this.executionCounter++;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string userName = "none";
            string password = "none";
            if (txtUserName.Text != "")
            {
                userName = txtUserName.Text;
                if (!Utilities.Users.UserExists(userName))
                {
                    if (txtPassword.Text != "") password = txtPassword.Text;
                    System.Environment.SetEnvironmentVariable("jsonapiregisterusername", userName, EnvironmentVariableTarget.User);
                    System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", password, EnvironmentVariableTarget.User);
                    this.profileUpdatesNeeded = true;
                    this.loginSuccessful = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("User already registered. Please click on the Login button");
                }
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
