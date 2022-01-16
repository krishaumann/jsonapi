
namespace JSONAPI.Options
{
    partial class frmTestList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsTestList = new System.Windows.Forms.BindingSource(this.components);
            this.dgTestList = new System.Windows.Forms.DataGridView();
            this.colTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExecutionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIncrementCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeaderRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBodyRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponseHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponseDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFIeldResults = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colTestSuiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxTestOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsTestList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTestList)).BeginInit();
            this.ctxTestOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgTestList
            // 
            this.dgTestList.AllowUserToAddRows = false;
            this.dgTestList.AllowUserToDeleteRows = false;
            this.dgTestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTestList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTestName,
            this.colExecutionStatus,
            this.colIncrementCounter,
            this.colHeaderRequest,
            this.colBodyRequest,
            this.colResponseHeader,
            this.colResponseDetail,
            this.colFIeldResults,
            this.colTestSuiteName});
            this.dgTestList.ContextMenuStrip = this.ctxTestOptions;
            this.dgTestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTestList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgTestList.Location = new System.Drawing.Point(0, 0);
            this.dgTestList.MultiSelect = false;
            this.dgTestList.Name = "dgTestList";
            this.dgTestList.ReadOnly = true;
            this.dgTestList.RowHeadersVisible = false;
            this.dgTestList.RowTemplate.Height = 25;
            this.dgTestList.ShowEditingIcon = false;
            this.dgTestList.Size = new System.Drawing.Size(1281, 216);
            this.dgTestList.TabIndex = 0;
            this.dgTestList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTestList_CellContentClick);
            // 
            // colTestName
            // 
            this.colTestName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestName.DataPropertyName = "testName";
            this.colTestName.HeaderText = "Test Name";
            this.colTestName.Name = "colTestName";
            this.colTestName.ReadOnly = true;
            // 
            // colExecutionStatus
            // 
            this.colExecutionStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExecutionStatus.DataPropertyName = "executionStatus";
            this.colExecutionStatus.HeaderText = "Execution Status";
            this.colExecutionStatus.Name = "colExecutionStatus";
            this.colExecutionStatus.ReadOnly = true;
            // 
            // colIncrementCounter
            // 
            this.colIncrementCounter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIncrementCounter.DataPropertyName = "incrementCounter";
            this.colIncrementCounter.HeaderText = "Increment";
            this.colIncrementCounter.Name = "colIncrementCounter";
            this.colIncrementCounter.ReadOnly = true;
            // 
            // colHeaderRequest
            // 
            this.colHeaderRequest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHeaderRequest.DataPropertyName = "headerRequest";
            this.colHeaderRequest.HeaderText = "Header Request";
            this.colHeaderRequest.Name = "colHeaderRequest";
            this.colHeaderRequest.ReadOnly = true;
            // 
            // colBodyRequest
            // 
            this.colBodyRequest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBodyRequest.DataPropertyName = "bodyRequest";
            this.colBodyRequest.HeaderText = "Request Body";
            this.colBodyRequest.Name = "colBodyRequest";
            this.colBodyRequest.ReadOnly = true;
            // 
            // colResponseHeader
            // 
            this.colResponseHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colResponseHeader.DataPropertyName = "headerResponse";
            this.colResponseHeader.HeaderText = "Header Response";
            this.colResponseHeader.Name = "colResponseHeader";
            this.colResponseHeader.ReadOnly = true;
            // 
            // colResponseDetail
            // 
            this.colResponseDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colResponseDetail.DataPropertyName = "bodyResponse";
            this.colResponseDetail.HeaderText = "Response Detail";
            this.colResponseDetail.Name = "colResponseDetail";
            this.colResponseDetail.ReadOnly = true;
            // 
            // colFIeldResults
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.colFIeldResults.DefaultCellStyle = dataGridViewCellStyle1;
            this.colFIeldResults.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colFIeldResults.HeaderText = "Fields..";
            this.colFIeldResults.MinimumWidth = 80;
            this.colFIeldResults.Name = "colFIeldResults";
            this.colFIeldResults.ReadOnly = true;
            this.colFIeldResults.Text = "..";
            this.colFIeldResults.ToolTipText = "Click for more details..";
            this.colFIeldResults.Width = 80;
            // 
            // colTestSuiteName
            // 
            this.colTestSuiteName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestSuiteName.DataPropertyName = "testSuiteName";
            this.colTestSuiteName.HeaderText = "TestSuiteName";
            this.colTestSuiteName.Name = "colTestSuiteName";
            this.colTestSuiteName.ReadOnly = true;
            this.colTestSuiteName.Visible = false;
            // 
            // ctxTestOptions
            // 
            this.ctxTestOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.ctxTestOptions.Name = "ctxTestOptions";
            this.ctxTestOptions.Size = new System.Drawing.Size(109, 26);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // frmTestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 216);
            this.Controls.Add(this.dgTestList);
            this.Name = "frmTestList";
            this.Text = "Test Execution Status";
            ((System.ComponentModel.ISupportInitialize)(this.bsTestList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTestList)).EndInit();
            this.ctxTestOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bsTestList;
        private System.Windows.Forms.DataGridView dgTestList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExecutionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIncrementCounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeaderRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBodyRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponseHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponseDetail;
        private System.Windows.Forms.DataGridViewButtonColumn colFIeldResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestSuiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.ContextMenuStrip ctxTestOptions;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}