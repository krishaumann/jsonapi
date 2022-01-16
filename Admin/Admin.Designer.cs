
namespace JSONAPI.Admin
{
    partial class frmAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnUpdateUsers = new JSONAPI.Controls.JSONAPIButton();
            this.btnDeactivateUsers = new JSONAPI.Controls.JSONAPIButton();
            this.btnActivateUsers = new JSONAPI.Controls.JSONAPIButton();
            this.btnDeleteUsers = new JSONAPI.Controls.JSONAPIButton();
            this.dgUsers = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgLogs = new System.Windows.Forms.DataGridView();
            this.btnPurge = new JSONAPI.Controls.JSONAPIButton();
            this.btnDeleteSelected = new JSONAPI.Controls.JSONAPIButton();
            this.bsUsers = new System.Windows.Forms.BindingSource(this.components);
            this.bsLogs = new System.Windows.Forms.BindingSource(this.components);
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserRole = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colUserEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserHome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceiveNotifications = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colActivationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSelected2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLogId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1421, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1413, 567);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 4);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnUpdateUsers);
            this.splitContainer2.Panel1.Controls.Add(this.btnDeactivateUsers);
            this.splitContainer2.Panel1.Controls.Add(this.btnActivateUsers);
            this.splitContainer2.Panel1.Controls.Add(this.btnDeleteUsers);
            this.splitContainer2.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgUsers);
            this.splitContainer2.Size = new System.Drawing.Size(1407, 559);
            this.splitContainer2.SplitterDistance = 38;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnUpdateUsers
            // 
            this.btnUpdateUsers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnUpdateUsers.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateUsers.Location = new System.Drawing.Point(523, 4);
            this.btnUpdateUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateUsers.Name = "btnUpdateUsers";
            this.btnUpdateUsers.Size = new System.Drawing.Size(166, 31);
            this.btnUpdateUsers.TabIndex = 6;
            this.btnUpdateUsers.Text = "Update Selected";
            this.btnUpdateUsers.UseVisualStyleBackColor = false;
            // 
            // btnDeactivateUsers
            // 
            this.btnDeactivateUsers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDeactivateUsers.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeactivateUsers.Location = new System.Drawing.Point(351, 4);
            this.btnDeactivateUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeactivateUsers.Name = "btnDeactivateUsers";
            this.btnDeactivateUsers.Size = new System.Drawing.Size(166, 31);
            this.btnDeactivateUsers.TabIndex = 5;
            this.btnDeactivateUsers.Text = "Deactivate Selected";
            this.btnDeactivateUsers.UseVisualStyleBackColor = false;
            // 
            // btnActivateUsers
            // 
            this.btnActivateUsers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnActivateUsers.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnActivateUsers.Location = new System.Drawing.Point(178, 4);
            this.btnActivateUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnActivateUsers.Name = "btnActivateUsers";
            this.btnActivateUsers.Size = new System.Drawing.Size(166, 31);
            this.btnActivateUsers.TabIndex = 4;
            this.btnActivateUsers.Text = "Activate Selected";
            this.btnActivateUsers.UseVisualStyleBackColor = false;
            // 
            // btnDeleteUsers
            // 
            this.btnDeleteUsers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDeleteUsers.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteUsers.Location = new System.Drawing.Point(6, 4);
            this.btnDeleteUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteUsers.Name = "btnDeleteUsers";
            this.btnDeleteUsers.Size = new System.Drawing.Size(166, 31);
            this.btnDeleteUsers.TabIndex = 3;
            this.btnDeleteUsers.Text = "Delete Selected";
            this.btnDeleteUsers.UseVisualStyleBackColor = false;
            // 
            // dgUsers
            // 
            this.dgUsers.AllowUserToAddRows = false;
            this.dgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colUserName,
            this.colUserPassword,
            this.colUserRole,
            this.colUserEmail,
            this.colUserHome,
            this.colReceiveNotifications,
            this.colActivationCode,
            this.colUserStatus});
            this.dgUsers.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUsers.Location = new System.Drawing.Point(0, 0);
            this.dgUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgUsers.Name = "dgUsers";
            this.dgUsers.RowHeadersVisible = false;
            this.dgUsers.RowHeadersWidth = 51;
            this.dgUsers.RowTemplate.Height = 25;
            this.dgUsers.Size = new System.Drawing.Size(1407, 516);
            this.dgUsers.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(1413, 567);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgLogs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnPurge);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteSelected);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Size = new System.Drawing.Size(1407, 559);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgLogs
            // 
            this.dgLogs.AllowUserToAddRows = false;
            this.dgLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected2,
            this.colLogId,
            this.colLogMessage,
            this.colLogType,
            this.colLogDate,
            this.dataGridViewTextBoxColumn2});
            this.dgLogs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLogs.Location = new System.Drawing.Point(0, 0);
            this.dgLogs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgLogs.Name = "dgLogs";
            this.dgLogs.RowHeadersVisible = false;
            this.dgLogs.RowHeadersWidth = 51;
            this.dgLogs.RowTemplate.Height = 25;
            this.dgLogs.Size = new System.Drawing.Size(1407, 491);
            this.dgLogs.TabIndex = 0;
            // 
            // btnPurge
            // 
            this.btnPurge.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnPurge.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnPurge.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPurge.Location = new System.Drawing.Point(178, 3);
            this.btnPurge.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(166, 44);
            this.btnPurge.TabIndex = 3;
            this.btnPurge.Text = "Purge";
            this.btnPurge.UseVisualStyleBackColor = false;
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDeleteSelected.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnDeleteSelected.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteSelected.Location = new System.Drawing.Point(6, 4);
            this.btnDeleteSelected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(166, 44);
            this.btnDeleteSelected.TabIndex = 2;
            this.btnDeleteSelected.Text = "Delete Selected";
            this.btnDeleteSelected.UseVisualStyleBackColor = false;
            // 
            // colSelected
            // 
            this.colSelected.HeaderText = "Select";
            this.colSelected.MinimumWidth = 80;
            this.colSelected.Name = "colSelected";
            this.colSelected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSelected.Width = 80;
            // 
            // colUserName
            // 
            this.colUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserName.DataPropertyName = "userName";
            this.colUserName.HeaderText = "UserName";
            this.colUserName.MinimumWidth = 6;
            this.colUserName.Name = "colUserName";
            // 
            // colUserPassword
            // 
            this.colUserPassword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserPassword.DataPropertyName = "userPassword";
            this.colUserPassword.HeaderText = "Password";
            this.colUserPassword.MinimumWidth = 6;
            this.colUserPassword.Name = "colUserPassword";
            this.colUserPassword.ReadOnly = true;
            // 
            // colUserRole
            // 
            this.colUserRole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserRole.DataPropertyName = "userRole";
            this.colUserRole.HeaderText = "Role";
            this.colUserRole.Items.AddRange(new object[] {
            "Admin",
            "User"});
            this.colUserRole.MinimumWidth = 6;
            this.colUserRole.Name = "colUserRole";
            this.colUserRole.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUserRole.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colUserEmail
            // 
            this.colUserEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserEmail.DataPropertyName = "userEmail";
            this.colUserEmail.HeaderText = "Email";
            this.colUserEmail.MinimumWidth = 6;
            this.colUserEmail.Name = "colUserEmail";
            // 
            // colUserHome
            // 
            this.colUserHome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserHome.DataPropertyName = "userHome";
            this.colUserHome.HeaderText = "User Home";
            this.colUserHome.MinimumWidth = 6;
            this.colUserHome.Name = "colUserHome";
            this.colUserHome.ReadOnly = true;
            // 
            // colReceiveNotifications
            // 
            this.colReceiveNotifications.DataPropertyName = "receiveNotifications";
            this.colReceiveNotifications.HeaderText = "Notifications";
            this.colReceiveNotifications.MinimumWidth = 6;
            this.colReceiveNotifications.Name = "colReceiveNotifications";
            this.colReceiveNotifications.ReadOnly = true;
            this.colReceiveNotifications.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colReceiveNotifications.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colReceiveNotifications.Width = 125;
            // 
            // colActivationCode
            // 
            this.colActivationCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colActivationCode.DataPropertyName = "activationCode";
            this.colActivationCode.HeaderText = "Activation Code";
            this.colActivationCode.MinimumWidth = 6;
            this.colActivationCode.Name = "colActivationCode";
            this.colActivationCode.ReadOnly = true;
            // 
            // colUserStatus
            // 
            this.colUserStatus.DataPropertyName = "userStatus";
            this.colUserStatus.HeaderText = "User Status";
            this.colUserStatus.MinimumWidth = 6;
            this.colUserStatus.Name = "colUserStatus";
            this.colUserStatus.ReadOnly = true;
            this.colUserStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUserStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colUserStatus.Width = 125;
            // 
            // colSelected2
            // 
            this.colSelected2.HeaderText = "Select";
            this.colSelected2.MinimumWidth = 80;
            this.colSelected2.Name = "colSelected2";
            this.colSelected2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSelected2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSelected2.Width = 80;
            // 
            // colLogId
            // 
            this.colLogId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLogId.DataPropertyName = "logId";
            this.colLogId.HeaderText = "Log Id";
            this.colLogId.MinimumWidth = 6;
            this.colLogId.Name = "colLogId";
            this.colLogId.ReadOnly = true;
            // 
            // colLogMessage
            // 
            this.colLogMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLogMessage.DataPropertyName = "logMessage";
            this.colLogMessage.HeaderText = "Message";
            this.colLogMessage.MinimumWidth = 6;
            this.colLogMessage.Name = "colLogMessage";
            this.colLogMessage.ReadOnly = true;
            // 
            // colLogType
            // 
            this.colLogType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLogType.DataPropertyName = "logType";
            this.colLogType.HeaderText = "Log Type";
            this.colLogType.MinimumWidth = 6;
            this.colLogType.Name = "colLogType";
            this.colLogType.ReadOnly = true;
            // 
            // colLogDate
            // 
            this.colLogDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLogDate.DataPropertyName = "logDateTime";
            this.colLogDate.HeaderText = "Date";
            this.colLogDate.MinimumWidth = 6;
            this.colLogDate.Name = "colLogDate";
            this.colLogDate.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "userName";
            this.dataGridViewTextBoxColumn2.HeaderText = "User Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 600);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmAdmin";
            this.Text = "Admin";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgLogs;
        private JSONAPI.Controls.JSONAPIButton btnDeleteSelected;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgUsers;
        private JSONAPI.Controls.JSONAPIButton btnDeleteUsers;
        private JSONAPI.Controls.JSONAPIButton btnUpdateUsers;
        private JSONAPI.Controls.JSONAPIButton btnDeactivateUsers;
        private JSONAPI.Controls.JSONAPIButton btnActivateUsers;
        private System.Windows.Forms.BindingSource bsUsers;
        private System.Windows.Forms.BindingSource bsLogs;
        private JSONAPI.Controls.JSONAPIButton btnPurge;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserPassword;
        private System.Windows.Forms.DataGridViewComboBoxColumn colUserRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserHome;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colReceiveNotifications;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivationCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colUserStatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}