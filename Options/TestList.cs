using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Options
{
    public partial class frmTestList : Form
    {
        public frmTestList(List<Utilities.TestSuite.TestList> testList)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            IEnumerable<Utilities.TestSuite.TestList> ds = testList;
            JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.TestSuite.TestList>(dgTestList, bsTestList, ds);
            bsTestList.DataSource = testList;
            dgTestList.DataSource = bsTestList;
            dgTestList.Refresh();
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            this.exportToolStripMenuItem.Click += new EventHandler(this.ExportTestResultsMenuItem_Click);
        }

        private void dgTestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                if (dgTestList[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
                {
                    var testRunList = (Utilities.TestSuite.TestList)dgTestList.Rows[e.RowIndex].DataBoundItem;
                    string testRunName = System.Environment.GetEnvironmentVariable("testRunName", EnvironmentVariableTarget.User).ToString();
                    var fieldList = Utilities.ExecutionResult.GetFieldResultList(testRunName, testRunList.TestName, testRunList.IncrementCounter);
                    frmFieldList tlForm = new frmFieldList(fieldList);
                    tlForm.TopMost = true;
                    tlForm.Show();
                }
            }
        }

        public void ExportTestResultsMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "TestResults.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgTestList, fileName);
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
    }
}