
namespace JSONAPI.Options
{
    partial class frmFieldList
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
            this.dgFieldResults = new System.Windows.Forms.DataGridView();
            this.ctxFieldList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colRunNr = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colIncrementCounter = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colFieldExpectedValue = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colFieldActualValue = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colResult = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.bsFieldList = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FilterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ShowAllLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewAutoFilterTextBoxColumn1 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn2 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.colFieldName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn3 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn4 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.dataGridViewAutoFilterTextBoxColumn5 = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgFieldResults)).BeginInit();
            this.ctxFieldList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsFieldList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgFieldResults
            // 
            this.dgFieldResults.AllowUserToAddRows = false;
            this.dgFieldResults.AllowUserToDeleteRows = false;
            this.dgFieldResults.AllowUserToOrderColumns = true;
            this.dgFieldResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFieldResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewAutoFilterTextBoxColumn1,
            this.dataGridViewAutoFilterTextBoxColumn2,
            this.colFieldName,
            this.dataGridViewAutoFilterTextBoxColumn3,
            this.dataGridViewAutoFilterTextBoxColumn4,
            this.dataGridViewAutoFilterTextBoxColumn5});
            this.dgFieldResults.ContextMenuStrip = this.ctxFieldList;
            this.dgFieldResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFieldResults.Location = new System.Drawing.Point(0, 0);
            this.dgFieldResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgFieldResults.Name = "dgFieldResults";
            this.dgFieldResults.ReadOnly = true;
            this.dgFieldResults.RowHeadersVisible = false;
            this.dgFieldResults.RowHeadersWidth = 51;
            this.dgFieldResults.RowTemplate.Height = 25;
            this.dgFieldResults.Size = new System.Drawing.Size(1351, 308);
            this.dgFieldResults.TabIndex = 0;
            // 
            // ctxFieldList
            // 
            this.ctxFieldList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxFieldList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxExportMenuItem});
            this.ctxFieldList.Name = "ctxFieldList";
            this.ctxFieldList.Size = new System.Drawing.Size(122, 28);
            // 
            // ctxExportMenuItem
            // 
            this.ctxExportMenuItem.Name = "ctxExportMenuItem";
            this.ctxExportMenuItem.Size = new System.Drawing.Size(121, 24);
            this.ctxExportMenuItem.Text = "Export";
            // 
            // colRunNr
            // 
            this.colRunNr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRunNr.DataPropertyName = "runNr";
            this.colRunNr.DropDownListBoxMaxLines = 50;
            this.colRunNr.HeaderText = "RunNr";
            this.colRunNr.MinimumWidth = 6;
            this.colRunNr.Name = "colRunNr";
            this.colRunNr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colIncrementCounter
            // 
            this.colIncrementCounter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIncrementCounter.DataPropertyName = "incrementNr";
            this.colIncrementCounter.DropDownListBoxMaxLines = 50;
            this.colIncrementCounter.HeaderText = "Increment Counter";
            this.colIncrementCounter.MinimumWidth = 6;
            this.colIncrementCounter.Name = "colIncrementCounter";
            this.colIncrementCounter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colFieldExpectedValue
            // 
            this.colFieldExpectedValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFieldExpectedValue.DataPropertyName = "expectedValue";
            this.colFieldExpectedValue.HeaderText = "Expected Value";
            this.colFieldExpectedValue.MinimumWidth = 6;
            this.colFieldExpectedValue.Name = "colFieldExpectedValue";
            this.colFieldExpectedValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colFieldActualValue
            // 
            this.colFieldActualValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFieldActualValue.DataPropertyName = "actualValue";
            this.colFieldActualValue.HeaderText = "Actual Value";
            this.colFieldActualValue.MinimumWidth = 6;
            this.colFieldActualValue.Name = "colFieldActualValue";
            this.colFieldActualValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colResult
            // 
            this.colResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colResult.DataPropertyName = "result";
            this.colResult.HeaderText = "Result";
            this.colResult.MinimumWidth = 6;
            this.colResult.Name = "colResult";
            this.colResult.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterStatusLabel,
            this.ShowAllLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 282);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1351, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FilterStatusLabel
            // 
            this.FilterStatusLabel.Name = "FilterStatusLabel";
            this.FilterStatusLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // ShowAllLabel
            // 
            this.ShowAllLabel.IsLink = true;
            this.ShowAllLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ShowAllLabel.Name = "ShowAllLabel";
            this.ShowAllLabel.Size = new System.Drawing.Size(67, 20);
            this.ShowAllLabel.Text = "Show &All";
            // 
            // dataGridViewAutoFilterTextBoxColumn1
            // 
            this.dataGridViewAutoFilterTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn1.DataPropertyName = "runNr";
            this.dataGridViewAutoFilterTextBoxColumn1.HeaderText = "Run Nr";
            this.dataGridViewAutoFilterTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewAutoFilterTextBoxColumn1.Name = "dataGridViewAutoFilterTextBoxColumn1";
            this.dataGridViewAutoFilterTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewAutoFilterTextBoxColumn2
            // 
            this.dataGridViewAutoFilterTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn2.DataPropertyName = "incrementCounter";
            this.dataGridViewAutoFilterTextBoxColumn2.HeaderText = "Increment Counter";
            this.dataGridViewAutoFilterTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewAutoFilterTextBoxColumn2.Name = "dataGridViewAutoFilterTextBoxColumn2";
            this.dataGridViewAutoFilterTextBoxColumn2.ReadOnly = true;
            // 
            // colFieldName
            // 
            this.colFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFieldName.DataPropertyName = "fieldName";
            this.colFieldName.HeaderText = "FieldName";
            this.colFieldName.MinimumWidth = 6;
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.ReadOnly = true;
            this.colFieldName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewAutoFilterTextBoxColumn3
            // 
            this.dataGridViewAutoFilterTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn3.DataPropertyName = "expectedValue";
            this.dataGridViewAutoFilterTextBoxColumn3.HeaderText = "Expected Value";
            this.dataGridViewAutoFilterTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewAutoFilterTextBoxColumn3.Name = "dataGridViewAutoFilterTextBoxColumn3";
            this.dataGridViewAutoFilterTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewAutoFilterTextBoxColumn4
            // 
            this.dataGridViewAutoFilterTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn4.DataPropertyName = "actualValue";
            this.dataGridViewAutoFilterTextBoxColumn4.HeaderText = "Actual Value";
            this.dataGridViewAutoFilterTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewAutoFilterTextBoxColumn4.Name = "dataGridViewAutoFilterTextBoxColumn4";
            this.dataGridViewAutoFilterTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewAutoFilterTextBoxColumn5
            // 
            this.dataGridViewAutoFilterTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewAutoFilterTextBoxColumn5.DataPropertyName = "result";
            this.dataGridViewAutoFilterTextBoxColumn5.HeaderText = "Result";
            this.dataGridViewAutoFilterTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewAutoFilterTextBoxColumn5.Name = "dataGridViewAutoFilterTextBoxColumn5";
            this.dataGridViewAutoFilterTextBoxColumn5.ReadOnly = true;
            // 
            // frmFieldList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 308);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgFieldResults);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmFieldList";
            this.Text = "Results per Field";
            ((System.ComponentModel.ISupportInitialize)(this.dgFieldResults)).EndInit();
            this.ctxFieldList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsFieldList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFieldResults;
        private System.Windows.Forms.BindingSource bsFieldList;
        private System.Windows.Forms.ContextMenuStrip ctxFieldList;
        private System.Windows.Forms.ToolStripMenuItem ctxExportMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel FilterStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel ShowAllLabel;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colRunNr;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colIncrementCounter;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colFieldExpectedValue;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colFieldActualValue;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colResult;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn1;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn2;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn colFieldName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn3;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn4;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dataGridViewAutoFilterTextBoxColumn5;
    }
}