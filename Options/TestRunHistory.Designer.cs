
namespace JSONAPI.Options
{
    partial class frmTestRunHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsTestRunHistory = new System.Windows.Forms.BindingSource(this.components);
            this.dgTestSuiteHistory = new System.Windows.Forms.DataGridView();
            this.colTestRunName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestSuiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestList = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsTestRunHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTestSuiteHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTestSuiteHistory
            // 
            this.dgTestSuiteHistory.AllowUserToAddRows = false;
            this.dgTestSuiteHistory.AllowUserToDeleteRows = false;
            this.dgTestSuiteHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTestSuiteHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTestRunName,
            this.colTestSuiteName,
            this.colTestName,
            this.colTestList,
            this.colUserName});
            this.dgTestSuiteHistory.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgTestSuiteHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTestSuiteHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTestSuiteHistory.Location = new System.Drawing.Point(0, 0);
            this.dgTestSuiteHistory.Name = "dgTestSuiteHistory";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTestSuiteHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgTestSuiteHistory.RowHeadersVisible = false;
            this.dgTestSuiteHistory.RowHeadersWidth = 70;
            this.dgTestSuiteHistory.RowTemplate.Height = 25;
            this.dgTestSuiteHistory.ShowEditingIcon = false;
            this.dgTestSuiteHistory.Size = new System.Drawing.Size(944, 274);
            this.dgTestSuiteHistory.TabIndex = 1;
            // 
            // colTestRunName
            // 
            this.colTestRunName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestRunName.DataPropertyName = "testRunName";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colTestRunName.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTestRunName.HeaderText = "Test Run Name";
            this.colTestRunName.MinimumWidth = 50;
            this.colTestRunName.Name = "colTestRunName";
            this.colTestRunName.ReadOnly = true;
            // 
            // colTestSuiteName
            // 
            this.colTestSuiteName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestSuiteName.DataPropertyName = "testSuiteName";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colTestSuiteName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTestSuiteName.HeaderText = "Test Suite Name";
            this.colTestSuiteName.MinimumWidth = 100;
            this.colTestSuiteName.Name = "colTestSuiteName";
            this.colTestSuiteName.ReadOnly = true;
            // 
            // colTestName
            // 
            this.colTestName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestName.DataPropertyName = "testName";
            this.colTestName.HeaderText = "Test Name";
            this.colTestName.Name = "colTestName";
            // 
            // colTestList
            // 
            this.colTestList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colTestList.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTestList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colTestList.HeaderText = "Fields..";
            this.colTestList.MinimumWidth = 70;
            this.colTestList.Name = "colTestList";
            this.colTestList.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTestList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colTestList.Text = "..";
            this.colTestList.ToolTipText = "Click for more details..";
            this.colTestList.Width = 70;
            // 
            // colUserName
            // 
            this.colUserName.DataPropertyName = "userName";
            this.colUserName.HeaderText = "User Name";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = false;
            // 
            // frmTestRunHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 274);
            this.Controls.Add(this.dgTestSuiteHistory);
            this.Name = "frmTestRunHistory";
            this.Text = "Test Suite History";
            ((System.ComponentModel.ISupportInitialize)(this.bsTestRunHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTestSuiteHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsTestRunHistory;
        private System.Windows.Forms.DataGridView dgTestSuiteHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestRunName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestSuiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestName;
        private System.Windows.Forms.DataGridViewButtonColumn colTestList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
    }
}