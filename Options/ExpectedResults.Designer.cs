
namespace JSONAPI.Options
{
    partial class frmExpectedResults
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUseBuilder = new System.Windows.Forms.CheckBox();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnClear = new JSONAPI.Controls.JSONAPIButton();
            this.cmbTestRun = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnResultSearch = new JSONAPI.Controls.JSONAPIButton();
            this.cmbTestName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTestSuite = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgExecutionResults = new System.Windows.Forms.DataGridView();
            this.colTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpectedResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldValue10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxExecutionResultOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bsExecutionResult = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnPreviewBuilder = new JSONAPI.Controls.JSONAPIButton();
            this.btnUpdateExpectedResults = new JSONAPI.Controls.JSONAPIButton();
            this.btnImportResults = new JSONAPI.Controls.JSONAPIButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExecutionResults)).BeginInit();
            this.ctxExecutionResultOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsExecutionResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUseBuilder);
            this.groupBox1.Controls.Add(this.lblRecords);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cmbTestRun);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnResultSearch);
            this.groupBox1.Controls.Add(this.cmbTestName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbTestSuite);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1209, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // chkUseBuilder
            // 
            this.chkUseBuilder.AutoSize = true;
            this.chkUseBuilder.Location = new System.Drawing.Point(24, 55);
            this.chkUseBuilder.Name = "chkUseBuilder";
            this.chkUseBuilder.Size = new System.Drawing.Size(171, 19);
            this.chkUseBuilder.TabIndex = 9;
            this.chkUseBuilder.Text = "Use Expected Result Builder";
            this.chkUseBuilder.UseVisualStyleBackColor = true;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(24, 62);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 15);
            this.lblRecords.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnClear.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClear.Location = new System.Drawing.Point(1101, 32);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 31);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbTestRun
            // 
            this.cmbTestRun.FormattingEnabled = true;
            this.cmbTestRun.Location = new System.Drawing.Point(72, 22);
            this.cmbTestRun.Name = "cmbTestRun";
            this.cmbTestRun.Size = new System.Drawing.Size(244, 23);
            this.cmbTestRun.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Test Run";
            // 
            // btnResultSearch
            // 
            this.btnResultSearch.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnResultSearch.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResultSearch.Location = new System.Drawing.Point(1011, 32);
            this.btnResultSearch.Name = "btnResultSearch";
            this.btnResultSearch.Size = new System.Drawing.Size(84, 31);
            this.btnResultSearch.TabIndex = 4;
            this.btnResultSearch.Text = "Search";
            this.btnResultSearch.UseVisualStyleBackColor = false;
            // 
            // cmbTestName
            // 
            this.cmbTestName.FormattingEnabled = true;
            this.cmbTestName.Location = new System.Drawing.Point(721, 22);
            this.cmbTestName.Name = "cmbTestName";
            this.cmbTestName.Size = new System.Drawing.Size(244, 23);
            this.cmbTestName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Test Name";
            // 
            // cmbTestSuite
            // 
            this.cmbTestSuite.FormattingEnabled = true;
            this.cmbTestSuite.Location = new System.Drawing.Point(395, 22);
            this.cmbTestSuite.Name = "cmbTestSuite";
            this.cmbTestSuite.Size = new System.Drawing.Size(244, 23);
            this.cmbTestSuite.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test Suite";
            // 
            // dgExecutionResults
            // 
            this.dgExecutionResults.AllowUserToAddRows = false;
            this.dgExecutionResults.AllowUserToDeleteRows = false;
            this.dgExecutionResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExecutionResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTestName,
            this.colFieldName,
            this.colExpectedResult,
            this.colFieldValue1,
            this.colFieldValue2,
            this.colFieldValue3,
            this.colFieldValue4,
            this.colFieldValue5,
            this.colFieldValue6,
            this.colFieldValue7,
            this.colFieldValue8,
            this.colFieldValue9,
            this.colFieldValue10,
            this.colUserName});
            this.dgExecutionResults.ContextMenuStrip = this.ctxExecutionResultOptions;
            this.dgExecutionResults.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgExecutionResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExecutionResults.Location = new System.Drawing.Point(0, 0);
            this.dgExecutionResults.Name = "dgExecutionResults";
            this.dgExecutionResults.RowHeadersVisible = false;
            this.dgExecutionResults.RowTemplate.Height = 25;
            this.dgExecutionResults.Size = new System.Drawing.Size(1209, 319);
            this.dgExecutionResults.TabIndex = 4;
            // 
            // colTestName
            // 
            this.colTestName.DataPropertyName = "testName";
            this.colTestName.HeaderText = "Test Name";
            this.colTestName.Name = "colTestName";
            // 
            // colFieldName
            // 
            this.colFieldName.DataPropertyName = "fieldName";
            this.colFieldName.HeaderText = "Field Name";
            this.colFieldName.Name = "colFieldName";
            // 
            // colExpectedResult
            // 
            this.colExpectedResult.DataPropertyName = "expectedResult";
            this.colExpectedResult.HeaderText = "Expected Result";
            this.colExpectedResult.Name = "colExpectedResult";
            // 
            // colFieldValue1
            // 
            this.colFieldValue1.DataPropertyName = "fieldValue1";
            this.colFieldValue1.HeaderText = "FieldValue1";
            this.colFieldValue1.Name = "colFieldValue1";
            // 
            // colFieldValue2
            // 
            this.colFieldValue2.DataPropertyName = "fieldValue2";
            this.colFieldValue2.HeaderText = "Field Value2";
            this.colFieldValue2.Name = "colFieldValue2";
            // 
            // colFieldValue3
            // 
            this.colFieldValue3.DataPropertyName = "fieldValue3";
            this.colFieldValue3.HeaderText = "Field Value3";
            this.colFieldValue3.Name = "colFieldValue3";
            // 
            // colFieldValue4
            // 
            this.colFieldValue4.DataPropertyName = "fieldValue4";
            this.colFieldValue4.HeaderText = "Field Value4";
            this.colFieldValue4.Name = "colFieldValue4";
            // 
            // colFieldValue5
            // 
            this.colFieldValue5.DataPropertyName = "fieldValue5";
            this.colFieldValue5.HeaderText = "Field Value5";
            this.colFieldValue5.Name = "colFieldValue5";
            // 
            // colFieldValue6
            // 
            this.colFieldValue6.DataPropertyName = "fieldValue6";
            this.colFieldValue6.HeaderText = "Field Value6";
            this.colFieldValue6.Name = "colFieldValue6";
            // 
            // colFieldValue7
            // 
            this.colFieldValue7.DataPropertyName = "fieldValue7";
            this.colFieldValue7.HeaderText = "FIeld Value7";
            this.colFieldValue7.Name = "colFieldValue7";
            // 
            // colFieldValue8
            // 
            this.colFieldValue8.DataPropertyName = "fieldValue8";
            this.colFieldValue8.HeaderText = "Field Value8";
            this.colFieldValue8.Name = "colFieldValue8";
            // 
            // colFieldValue9
            // 
            this.colFieldValue9.DataPropertyName = "fieldValue9";
            this.colFieldValue9.HeaderText = "Field Value9";
            this.colFieldValue9.Name = "colFieldValue9";
            // 
            // colFieldValue10
            // 
            this.colFieldValue10.DataPropertyName = "fieldValue10";
            this.colFieldValue10.HeaderText = "Field Value10";
            this.colFieldValue10.Name = "colFieldValue10";
            // 
            // colUserName
            // 
            this.colUserName.DataPropertyName = "userName";
            this.colUserName.HeaderText = "User Name";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = false;
            // 
            // ctxExecutionResultOptions
            // 
            this.ctxExecutionResultOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.ctxExecutionResultOptions.Name = "ctxExecutionResultOptions";
            this.ctxExecutionResultOptions.Size = new System.Drawing.Size(109, 26);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1209, 450);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnImportResults);
            this.splitContainer2.Panel1.Controls.Add(this.btnPreviewBuilder);
            this.splitContainer2.Panel1.Controls.Add(this.btnUpdateExpectedResults);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgExecutionResults);
            this.splitContainer2.Size = new System.Drawing.Size(1209, 366);
            this.splitContainer2.SplitterDistance = 43;
            this.splitContainer2.TabIndex = 5;
            // 
            // btnPreviewBuilder
            // 
            this.btnPreviewBuilder.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnPreviewBuilder.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPreviewBuilder.Location = new System.Drawing.Point(909, 9);
            this.btnPreviewBuilder.Name = "btnPreviewBuilder";
            this.btnPreviewBuilder.Size = new System.Drawing.Size(186, 31);
            this.btnPreviewBuilder.TabIndex = 6;
            this.btnPreviewBuilder.Text = "Expected Result Builder..";
            this.btnPreviewBuilder.UseVisualStyleBackColor = false;
            this.btnPreviewBuilder.Click += new System.EventHandler(this.btnPreviewBuilder_Click);
            // 
            // btnUpdateExpectedResults
            // 
            this.btnUpdateExpectedResults.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnUpdateExpectedResults.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateExpectedResults.Location = new System.Drawing.Point(1101, 9);
            this.btnUpdateExpectedResults.Name = "btnUpdateExpectedResults";
            this.btnUpdateExpectedResults.Size = new System.Drawing.Size(84, 31);
            this.btnUpdateExpectedResults.TabIndex = 5;
            this.btnUpdateExpectedResults.Text = "Update";
            this.btnUpdateExpectedResults.UseVisualStyleBackColor = false;
            this.btnUpdateExpectedResults.Click += new System.EventHandler(this.btnUpdateExpectedResults_Click);
            // 
            // btnImportResults
            // 
            this.btnImportResults.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnImportResults.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnImportResults.Location = new System.Drawing.Point(721, 9);
            this.btnImportResults.Name = "btnImportResults";
            this.btnImportResults.Size = new System.Drawing.Size(186, 31);
            this.btnImportResults.TabIndex = 7;
            this.btnImportResults.Text = "Import Response JSON..";
            this.btnImportResults.UseVisualStyleBackColor = false;
            this.btnImportResults.Click += new System.EventHandler(this.btnImportResults_Click);
            // 
            // frmExpectedResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmExpectedResults";
            this.Text = "Execution Results";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExecutionResults)).EndInit();
            this.ctxExecutionResultOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsExecutionResult)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTestSuite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTestName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgExecutionResults;
        private JSONAPI.Controls.JSONAPIButton btnResultSearch;
        private System.Windows.Forms.ComboBox cmbTestRun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource bsExecutionResult;
        private JSONAPI.Controls.JSONAPIButton btnClear;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private JSONAPI.Controls.JSONAPIButton btnUpdateExpectedResults;
        private System.Windows.Forms.CheckBox chkUseBuilder;
        private JSONAPI.Controls.JSONAPIButton btnPreviewBuilder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpectedResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldValue10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.ContextMenuStrip ctxExecutionResultOptions;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private JSONAPI.Controls.JSONAPIButton btnImportResults;
    }
}