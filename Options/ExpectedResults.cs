using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Options
{
    public partial class frmExpectedResults : Form
    {
        public Boolean useBuilderFlag = false;
        public frmExpectedResults()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            LoadValues();
            AddEventHandlers();
        }

        public void LoadValues()
        {
            List<string> testRuns = Utilities.ExecutionResult.GetTestRunNames();
            List<string> testSuiteNames = Utilities.TestSuite.GetTestSuiteNames();
            List<string> testNames = Utilities.TestSuite.GetTestNames();
            int c = 0;
            foreach (string newValue in testRuns)
            {
                cmbTestRun.Items.Insert(c, newValue);
                c++;
            }
            c = 0;
            foreach (string newValue in testSuiteNames)
            {
                cmbTestSuite.Items.Insert(c, newValue);
                c++;
            }
            c = 0;
            foreach (string newValue in testNames)
            {
                cmbTestName.Items.Insert(c, newValue);
                c++;
            }
        }

        private void AddEventHandlers()
        {
            this.btnResultSearch.Click += new EventHandler(this.searchButton_Click);
            this.exportToolStripMenuItem.Click += new EventHandler(this.ExportResultsMenuItem_Click);
            this.dgExecutionResults.CellClick += new DataGridViewCellEventHandler(this.dgExecutionResults_CellClick);
            this.dgExecutionResults.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgExecutionResults_CellPainting);
        }

        private void searchButton_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (chkUseBuilder.Checked) useBuilderFlag = true;
            else useBuilderFlag = false;
            bsExecutionResult.DataSource = Utilities.ExecutionResult.GetGroupedResultList(cmbTestRun.Text, cmbTestSuite.Text, cmbTestName.Text);
            dgExecutionResults.DataSource = bsExecutionResult;
            dgExecutionResults.Refresh();
            lblRecords.Text = dgExecutionResults.Rows.Count.ToString() + " rows found.";
            Cursor.Current = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgExecutionResults.Rows.Clear();
            cmbTestName.Text = "";
            cmbTestRun.Text = "";
            cmbTestSuite.Text = "";
            lblRecords.Text = "";
        }

        private void btnUpdateExpectedResults_Click(object sender, EventArgs e)
        {
            List<Utilities.TestSuite.FieldExpectedResult> expectedResultList = new List<Utilities.TestSuite.FieldExpectedResult>();
            Utilities.TestSuite.FieldExpectedResult testResult = new Utilities.TestSuite.FieldExpectedResult();
            
            string testName1 = "" ;
            string testName2 = "";
            for (int rows = 0; rows < dgExecutionResults.Rows.Count; rows++)
            {
                if (rows > 0) testName1 = dgExecutionResults.Rows[rows-1].Cells["colTestName"].Value.ToString();
                else testName1 = dgExecutionResults.Rows[0].Cells["colTestName"].Value.ToString();
                testName2 = dgExecutionResults.Rows[rows].Cells["colTestName"].Value.ToString();
                if ((testName1 != testName2) | rows == 0)
                {
                    Utilities.TestSuite.Test newTest = new Utilities.TestSuite.Test(testName1);
                    //newTest.FieldList.InsertRange(testResult);
                }
                for (int col = 0; col < dgExecutionResults.Rows[rows].Cells.Count; col++)
                {
                    var tempValue = dgExecutionResults.Rows[rows].Cells["colExpectedResult"].Value;
                    if (tempValue != null)
                    {
                        testResult.FieldName = dgExecutionResults.Rows[rows].Cells["colFieldName"].Value.ToString();
                        testResult.ExpectedResult = dgExecutionResults.Rows[rows].Cells["colExpectedResult"].Value.ToString();
                        expectedResultList.Add(testResult);
                    }

                }
                testName1 = dgExecutionResults.Rows[rows].Cells["colTestName"].Value.ToString();
            }
            bool retValue = Utilities.TestSuite.UpdateExpectedResults(testName1, expectedResultList);
            if (retValue) MessageBox.Show("Expected results updated successfully");
            else MessageBox.Show("There was an issue with updating the Expected results");
        }

        private void dgExecutionResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 & this.dgExecutionResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null & useBuilderFlag)
            {
                var groupResultRow = (Utilities.ExecutionResult.GroupedResult)dgExecutionResults.Rows[e.RowIndex].DataBoundItem;
                frmBuildExpectedResult tlForm = new frmBuildExpectedResult(groupResultRow);
                System.Environment.SetEnvironmentVariable("expectedRowNr", e.RowIndex.ToString(), EnvironmentVariableTarget.User);
                tlForm.TopMost = true;
                tlForm.Show();
            }
        }

        private void dgExecutionResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 & useBuilderFlag)
                {
                    DataGridViewCell cell = this.dgExecutionResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.Value == null)
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~(DataGridViewPaintParts.ContentForeground));
                        var r = e.CellBounds;
                        r.Inflate(-1, -1);
                        e.Graphics.FillRectangle(Brushes.CornflowerBlue, r);
                        e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                        e.Handled = true;
                        cell.ReadOnly = true;
                        cell.ToolTipText = "Click here to Build an Expected Result expression..";
                        //DataGridViewButtonCell dgButton = new DataGridViewButtonCell();
                        //this.dgExecutionResults.Rows[e.RowIndex].Cells[e.ColumnIndex] = dgButton;
                        //dgExecutionResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { BackColor = Color.CornflowerBlue };
                        //dgExecutionResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Build..";

                    }
                }                
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("Error defining Button for dgExecutionResults" + fe.ToString(), System.Diagnostics.TraceEventType.Error);
            }
        }

        private void btnPreviewBuilder_Click(object sender, EventArgs e)
        {
            frmBuildExpectedResult tlForm = new frmBuildExpectedResult();
            tlForm.TopMost = true;
            tlForm.Show();
        }

        public void ExportResultsMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "ExecutionResults.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgExecutionResults, fileName);
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

        private void btnImportResults_Click(object sender, EventArgs e)
        {
            string jsonInputStr = "";
            string testName = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);

            dialog.Title = "Open JSON Response Sample Output File";
            dialog.DefaultExt = "json";
            dialog.Filter = "JSON Files (*.json) | *.json";
            dialog.FileName = "sample.json";
            dialog.ShowDialog();
            Cursor.Current = Cursors.WaitCursor;
            string fileName = dialog.FileName;
            if (File.Exists(fileName))
            {
                jsonInputStr = File.ReadAllText(fileName);
                var fileNameArray = fileName.Split("\\".ToCharArray());
                testName = fileNameArray[fileNameArray.Length - 1].Split(".".ToCharArray())[0];
                Utilities.TestSuite.NewExpectedResult(testName, Utilities.Utilities.BuildExpectedResultList(testName, jsonInputStr));

                bsExecutionResult.DataSource = Utilities.ExecutionResult.GetGroupedResultList("", "", testName);
                dgExecutionResults.DataSource = bsExecutionResult;
                dgExecutionResults.Refresh();
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
