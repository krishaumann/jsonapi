using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Admin
{
    public partial class frmUserProfile : Form
    {
        public string activateCode = "";
        public bool registrationSuccessful = false;
        public Utilities.Users.User profileUser = null;


        public frmUserProfile()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            //Existing registered user accessing profile screen
            string validatedUserName = System.Environment.GetEnvironmentVariable("jsonapiuserName", EnvironmentVariableTarget.User);
            //Existing user accessing profile screen for registration
            string tempUserName = System.Environment.GetEnvironmentVariable("jsonapiregisterusername", EnvironmentVariableTarget.User);
            string tempPassword = System.Environment.GetEnvironmentVariable("jsonapiregisterpassword", EnvironmentVariableTarget.User);
            if (tempUserName == "none") tempUserName = "";
            if (tempPassword == "none") tempPassword = "";
            if (validatedUserName == null) validatedUserName = "none";
            if (validatedUserName != "none") profileUser = Utilities.Users.GetNewUser(validatedUserName);
            else
            {
                if (tempUserName != "")
                    profileUser = Utilities.Users.GetNewUser(tempUserName);
                else profileUser = new Utilities.Users.User(); //new user accessing screen;
            }
            if (profileUser.UserName == "")
            {
                //Register new user
                txtUserName.Text = tempUserName;
                txtUserName.Enabled = true;
                txtPassword1.Text = tempPassword;
                txtPassword2.Text = "";
                txtActivationCode.Text = "";
                txtActivationCode.Enabled = false;
                txtEmail.Text = "";

                chkRecNotifications.Checked = true;
                chkRecNotifications.Enabled = true;
                btnUpdate.Text = "Register";
                btnUpdatePassword.Enabled = false;
                btnLogout.Enabled = false;
                btnAdmin.Visible = false;
                this.registrationSuccessful = false;
            }
            else
            {
                txtUserName.Text = profileUser.UserName;
                txtUserName.Enabled = false;
                txtPassword1.Text = Utilities.Users.Decrypt(profileUser.UserPassword);
                txtEmail.Text = profileUser.UserEmail;
                txtUserHome.Text = profileUser.UserHome;
                if (profileUser.ReceiveNotifications) chkRecNotifications.Checked = true;
                if (profileUser.UserRole.ToLower() == "admin") btnAdmin.Visible = true;
                else btnAdmin.Visible = false;
                if (!profileUser.UserStatus)
                {
                    //Activate user
                    btnUpdate.Text = "Validate";
                    this.activateCode = profileUser.ActivationCode;
                    txtActivationCode.Enabled = true;
                    txtEmail.Enabled = false;
                    txtPassword1.Enabled = false;
                    txtPassword2.Enabled = false;
                    txtUserHome.Enabled = false;
                    chkRecNotifications.Enabled = false;
                    btnUpdatePassword.Enabled = false;
                    this.registrationSuccessful = false;
                    btnLogout.Enabled = false;
                }
                else
                {
                    btnUpdate.Text = "Update";
                    txtActivationCode.Text = profileUser.ActivationCode;
                    txtActivationCode.Enabled = false;
                    txtEmail.Enabled = true;
                    txtPassword1.Enabled = true;
                    txtPassword2.Enabled = true;
                    txtUserHome.Enabled = true;
                    chkRecNotifications.Enabled = true;
                    btnUpdatePassword.Enabled = true;
                    this.registrationSuccessful = true;
                    btnLogout.Enabled = true;
                }
            }
        }

        private void AddEventHandlers()
        {
            this.btnUpdate.Click += new EventHandler(this.updateButton_Click);
            this.btnAdmin.Click += new EventHandler(this.adminButton_Click);
            this.btnLogout.Click += new EventHandler(this.logoutButton_Click);
            this.btnClearCache.Click += new EventHandler(this.btnClearCache_Click);
            this.FormClosing += this.frmUserProfile_FormClosing;
        }

        private void adminButton_Click(object sender, System.EventArgs e)
        {
            frmAdmin adminForm = new frmAdmin();
            adminForm.Show();
        }

        private void updateButton_Click(object sender, System.EventArgs e)
        {
            if (btnUpdate.Text == "Validate")
            {
                if (this.activateCode == txtActivationCode.Text)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Utilities.Users.ActivateUser(this.txtUserName.Text);
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("User activated successfully.");
                    this.registrationSuccessful = true;
                    Close();
                    System.Environment.SetEnvironmentVariable("jsonapiuserName", this.txtUserName.Text, EnvironmentVariableTarget.User);
                }
                else
                {
                    MessageBox.Show("User not activated successfully. Please try again.");
                    this.registrationSuccessful = false;
                }
            }
            if (btnUpdate.Text == "Register")
            {
               if (txtUserName.Text.Length == 0 | txtEmail.Text.Length == 0 | !Utilities.Users.IsValidEmail(txtEmail.Text))
               {
                    MessageBox.Show("Please specify valid Email address and User Name.");
                    this.registrationSuccessful = false;
                }
               else
               {
                    if (txtPassword1.Text != txtPassword1.Text | txtPassword1.Text.Length == 0 | txtPassword2.Text.Length == 0)
                    {
                        MessageBox.Show("Please specify valid passwords.");
                        this.registrationSuccessful = false;
                    }
                    else
                    {
                        if (Utilities.Users.UserNameEmailUnique(txtUserName.Text, txtEmail.Text))
                        {
                            MessageBox.Show("User name and/or email address already exist.  Please use unique details.");
                            this.registrationSuccessful = false;
                        }
                        else
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            if (!System.IO.Directory.Exists(txtUserHome.Text)) System.IO.Directory.CreateDirectory(txtUserHome.Text);
                            
                            bool result = Utilities.Users.NewUser(txtUserName.Text, txtPassword1.Text, txtEmail.Text, txtUserHome.Text);
                            Cursor.Current = Cursors.Default;
                            if (result)
                            {
                                MessageBox.Show("User created successfully.  An activation code was sent to your email.  Please login and update the activation code.");
                                System.Environment.SetEnvironmentVariable("jsonapiregisterusername", "none", EnvironmentVariableTarget.User);
                                System.Environment.SetEnvironmentVariable("jsonapiregisterpassword", "none", EnvironmentVariableTarget.User);
                                this.registrationSuccessful = true;
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Error on User creation.  Please try again.");
                                this.registrationSuccessful = false;
                            }
                        }                      
                    }
               }
            }
            if (btnUpdate.Text == "Update")
            {
                string email = txtEmail.Text.Trim();
                string userHome = txtUserHome.Text.Trim();
                string userName = txtUserName.Text.Trim();
                if (Utilities.Users.IsValidEmail(email))
                {
                    bool tempValue = Utilities.Users.UpdateEmailAddress(userName, email);
                    if (tempValue)
                    {
                        if (System.IO.Directory.Exists(userHome))
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update your User Home Directory?", "User Home update", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                tempValue = Utilities.Users.UpdateUserHome(userName, userHome);
                                if (tempValue)
                                {
                                    MessageBox.Show("User home directory updated successfully. Treeview will refresh once you log in again.");
                                    // Need to refresh the grid with new user home
                                    this.registrationSuccessful = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Email address updated successfully.");
                                this.registrationSuccessful = true;
                            }

                        }
                        else
                        {
                            MessageBox.Show("User home directory does not exist.  Please create folder");
                            this.registrationSuccessful = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error on Email address update.  Please try again.");
                        this.registrationSuccessful = false;
                    }                   
                }
                else
                {
                    MessageBox.Show("Wrong Email address format specified.  Please try again.");
                    this.registrationSuccessful = false;
                }
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            var keepSettings = System.Environment.GetEnvironmentVariable("keepLoggedIn", EnvironmentVariableTarget.User);
            if (keepSettings == null | keepSettings == "none")
            {
                System.Environment.SetEnvironmentVariable("jsonapiuserName", "none", EnvironmentVariableTarget.User);
            }
            this.registrationSuccessful = true;
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword1.Text == txtPassword2.Text)
            {
                bool result = Utilities.Users.UpdatePassword(txtUserName.Text, txtPassword1.Text);
                if (result)
                {
                    MessageBox.Show("Password update successful");
                    this.registrationSuccessful = true;
                }
                else MessageBox.Show("Password update failed.  Please try another password.");
            }
            else
            {
                MessageBox.Show("Password texts need to be the same.  Please update accordingly.");
                this.registrationSuccessful = false;
            }
        }

        private void frmUserProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.registrationSuccessful)
            {
                MessageBox.Show("Please make the necessary changes before closing this form.");
                e.Cancel = true;
            }
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("keepLoggedIn", "none", EnvironmentVariableTarget.User);
            MessageBox.Show("Cache cleared successfully.");
        }
    }
}
