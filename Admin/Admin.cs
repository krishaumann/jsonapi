using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Admin
{
    /*  Responsible for User Admin and view of log files.  Currently only user test/test has admin functionality
     */  
    public partial class frmAdmin : Form
    {
        CheckBox checkboxHeaderUser = null;
        CheckBox checkboxHeaderLog = null;
        private bool isUserHeaderCheckBoxClicked = false;
        private bool isLogHeaderCheckBoxClicked = false;
        string oldUserName = "";

        public frmAdmin()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            AddUserChkBoxHeader_DataGridView();
            AddLogChkBoxHeader_DataGridView();
            btnActivateUsers.Enabled = false;
            btnDeactivateUsers.Enabled = false;
            btnDeleteSelected.Enabled = false;
            btnDeleteUsers.Enabled = false;
            btnUpdateUsers.Enabled = false;
            List<Utilities.Users.User> userList = Utilities.Users.GetUsers();
            IEnumerable<Utilities.Users.User> ds = userList;
            JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.Users.User>(dgUsers, bsUsers, ds);
            bsUsers.DataSource = userList;
            dgUsers.DataSource = bsUsers;
            dgUsers.Refresh();
            List<Utilities.Logs.Log> logList = Utilities.Logs.GetLogs();
            IEnumerable<Utilities.Logs.Log> ds2 = logList;
            JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.Logs.Log>(dgLogs, bsLogs, ds2);
            bsLogs.DataSource = logList;
            dgLogs.DataSource = bsLogs;
            dgLogs.Refresh();
        }

        public void AddEventHandlers()
        {
            this.btnActivateUsers.Click += new EventHandler(this.btnActivateUsers_Click);
            this.btnDeactivateUsers.Click += new EventHandler(this.btnDeactivateUsers_Click);
            this.btnDeleteUsers.Click += new EventHandler(this.btnDeleteUsers_Click);
            this.btnUpdateUsers.Click += new EventHandler(this.btnUpdateUsers_Click);
            this.btnDeleteSelected.Click += new EventHandler(this.btnDeleteSelected_Click);
            this.btnPurge.Click += new EventHandler(this.btnPurge_Click);
            this.dgUsers.CellValueChanged += new DataGridViewCellEventHandler(this.dgUsers_OnCellValueChanged);
            this.dgLogs.CellValueChanged += new DataGridViewCellEventHandler(this.dgLogs_OnCellValueChanged);
            this.dgUsers.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dgUsers_CellBeginEdit);
        }

        /* ===============================================
         * Users
         * ===============================================*/
        private void btnActivateUsers_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    Utilities.Users.ActivateUser(row.Cells["colUserName"].Value.ToString());
                }
            }
            bsUsers.DataSource = Utilities.Users.GetUsers();
            dgUsers.DataSource = bsUsers;
            dgUsers.Refresh();
            Cursor.Current = Cursors.Default;
        }
        
        private void btnDeactivateUsers_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    Utilities.Users.DeactivateUser(row.Cells["colUserName"].Value.ToString());
                }
            }
            bsUsers.DataSource = Utilities.Users.GetUsers();
            dgUsers.DataSource = bsUsers;
            dgUsers.Refresh();
            Cursor.Current = Cursors.Default;
        }
        private void btnDeleteUsers_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    Utilities.Users.DeleteUser(row.Cells["colUserName"].Value.ToString());
                }
            }
            bsUsers.DataSource = Utilities.Users.GetUsers();
            dgUsers.DataSource = bsUsers;
            dgUsers.Refresh();
            Cursor.Current = Cursors.Default;
        }
        private void btnUpdateUsers_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgUsers.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    Utilities.Users.UpdateEmailAddress(row.Cells["colUserName"].Value.ToString(), row.Cells["colUserEmail"].Value.ToString());
                    Utilities.Users.UpdateUserRole(row.Cells["colUserName"].Value.ToString(), row.Cells["colUserRole"].Value.ToString());
                    Utilities.Users.UpdateUserName(this.oldUserName, row.Cells["colUserName"].Value.ToString());
                }
            }
            bsUsers.DataSource = Utilities.Users.GetUsers();
            dgUsers.DataSource = bsUsers;
            dgUsers.Refresh();
            Cursor.Current = Cursors.Default;
        }

        private void AddUserChkBoxHeader_DataGridView()
        {
            Rectangle rect = dgUsers.GetCellDisplayRectangle(0, -1, true);
            rect.Y = rect.Location.Y + 5;
            rect.X = rect.Location.X + 55;
            checkboxHeaderUser = new CheckBox();
            checkboxHeaderUser.Size = new Size(15, 15);
            checkboxHeaderUser.Location = rect.Location;
            dgUsers.Controls.Add(checkboxHeaderUser);
            checkboxHeaderUser.MouseClick += new MouseEventHandler(checkboxHeaderUser_CheckedChanged);
        }
        
        private void dgUsers_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnActivateUsers.Enabled = true;
            btnDeactivateUsers.Enabled = true;
            btnDeleteUsers.Enabled = true;
            btnUpdateUsers.Enabled = true;
        }
        
        private void dgUsers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.oldUserName = dgUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void checkboxHeaderUser_CheckedChanged(object sender, EventArgs e)
        {
            HeaderCheckBoxClickUser((CheckBox)sender);
        }

        private void HeaderCheckBoxClickUser(CheckBox headerCheckbox)
        {
            isUserHeaderCheckBoxClicked = true;
            foreach (DataGridViewRow r in dgUsers.Rows)
            {
                ((DataGridViewCheckBoxCell)r.Cells[0]).Value = headerCheckbox.Checked;
            }
            dgUsers.RefreshEdit();
            isUserHeaderCheckBoxClicked = false;
        }

        /* ===============================================
        * Logs
        * ===============================================*/

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in dgLogs.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected2"]).FormattedValue)
                {
                    Utilities.Logs.DeleteLog(row.Cells["colLogId"].Value.ToString());
                }
            }
            bsLogs.DataSource = Utilities.Logs.GetLogs();
            dgLogs.DataSource = bsLogs;
            dgLogs.Refresh();
            Cursor.Current = Cursors.Default;
        }

        private void AddLogChkBoxHeader_DataGridView()
        {
            Rectangle rect = dgLogs.GetCellDisplayRectangle(0, -1, true);
            rect.Y = rect.Location.Y + 5;
            rect.X = rect.Location.X + 55;
            checkboxHeaderLog = new CheckBox();
            checkboxHeaderLog.Size = new Size(15, 15);
            checkboxHeaderLog.Location = rect.Location;
            dgLogs.Controls.Add(checkboxHeaderLog);
            checkboxHeaderLog.MouseClick += new MouseEventHandler(checkboxHeaderLog_CheckedChanged);
        }

        private void dgLogs_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnDeleteSelected.Enabled = true;
        }

        private void checkboxHeaderLog_CheckedChanged(object sender, EventArgs e)
        {
            HeaderCheckBoxClickLog((CheckBox)sender);
        }

        private void HeaderCheckBoxClickLog(CheckBox headerCheckbox)
        {
            isLogHeaderCheckBoxClicked = true;
            foreach (DataGridViewRow r in dgLogs.Rows)
            {
                ((DataGridViewCheckBoxCell)r.Cells[0]).Value = headerCheckbox.Checked;
            }
            dgLogs.RefreshEdit();
            isLogHeaderCheckBoxClicked = false;
        }

        private void btnPurge_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Utilities.Logs.DeleteLogs();
            bsLogs.DataSource = Utilities.Logs.GetLogs();
            dgLogs.DataSource = bsLogs;
            dgLogs.Refresh();
            Cursor.Current = Cursors.Default;
        }
    }
}
