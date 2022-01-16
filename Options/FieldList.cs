using DataGridViewAutoFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Options
{
    public partial class frmFieldList : Form
    {
        public frmFieldList(List<Utilities.ExecutionResult.TestResult> fieldList)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            //IEnumerable<Utilities.ExecutionResult.TestResult> ds = fieldList; 
            //JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.ExecutionResult.TestResult>(dgFieldResults, ds);
            bsFieldList.DataSource = fieldList;
            dgFieldResults.DataSource = bsFieldList;
            EnableGridFilter(true);
            dgFieldResults.Refresh();
        }

        private void AddEventHandlers()
        {
            this.ctxExportMenuItem.Click += new EventHandler(this.ExportResultsMenuItem_Click);
            this.ShowAllLabel.Click += new EventHandler(this.ShowAllLabel_Click);
            this.dgFieldResults.KeyDown += new KeyEventHandler(this.dgFieldResults_KeyDown);
            this.dgFieldResults.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dgFieldResults_DataBindingComplete);
        }

        public void ExportResultsMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "FieldResults.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgFieldResults, fileName);
            Cursor.Current = Cursors.Default;
            if (!result)
            {
                MessageBox.Show("Export failed.");
            }
            else
            {
                MessageBox.Show("Export successful to file: " + fileName);
            }
        }

        private void dgFieldResults_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dgFieldResults);
            if (string.IsNullOrEmpty(filterStatus))
            {
                ShowAllLabel.Visible = false;
                FilterStatusLabel.Visible = false;
            }
            else
            {
                ShowAllLabel.Visible = true;
                FilterStatusLabel.Visible = true;
                FilterStatusLabel.Text = filterStatus;
            }
        }

        private void ShowAllLabel_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgFieldResults);
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("Filter removal error: " + fe.Message, System.Diagnostics.TraceEventType.Error);
            }
        }

        private void dgFieldResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt
            && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            && dgFieldResults.CurrentCell != null
            && dgFieldResults
            .CurrentCell.OwningColumn.HeaderCell is DataGridViewAutoFilterColumnHeaderCell filterCell)
            {
                filterCell.ShowDropDownList();
                e.Handled = true;
            }
        }

        private void EnableGridFilter(bool value)
        {
            colFieldName.FilteringEnabled = value;
            colIncrementCounter.FilteringEnabled = value;
            colFieldActualValue.FilteringEnabled = value;
            colFieldExpectedValue.FilteringEnabled = value;
            colResult.FilteringEnabled = value;
            colRunNr.FilteringEnabled = value;
        }
    }
}
