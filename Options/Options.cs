using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace JSONAPI.Options
{
    public partial class OptionForm : Form
    {
        public string oldVariableName = "";
        public string oldSearchForElement = "";
        public string oldReplaceWhere = "";
        public string oldRangeName = "";
        public string oldSavedValue = "";
        public string oldPartialSave = "";
        public int oldNumChars = 0;
        public int selectedRow = 0;
        CheckBox checkboxHeader = null;
        bool isHeaderCheckBoxClicked = false;
        public OptionForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
        }
        
        public void AddEventHandlers()
        {
            this.tsMenuDeleteVariable.Click += new EventHandler(this.tsMenuDeleteVariable_Click);
            this.btnVariableSave.Click += new EventHandler(this.AddVariable_Click);
            this.btnVariableCancel.Click += new EventHandler(this.CancelVariable_Click);
            this.btnAddSingleTest.Click += new EventHandler(this.AddSingleTest);
            this.btnAddAll.Click += new EventHandler(this.AddAllTests);
            this.btnRemoveOne.Click += new EventHandler(this.RemoveSingleTest);
            this.btnRemoveAll.Click += new EventHandler(this.RemoveAllTests);
            this.btnAddSingleVariable.Click += new EventHandler(this.AddSingleVariable);
            this.btnAddAllVariables.Click += new EventHandler(this.AddAllVariables);
            this.btnRemoveOneVariable.Click += new EventHandler(this.RemoveSingleVariable);
            this.btnRemoveAllVariables.Click += new EventHandler(this.RemoveAllVariables);
            this.btnRunTests.Click += new EventHandler(this.RunSelectedTests);
            this.btnDeleteTestSuite.Click += new EventHandler(this.DeleteSelectedTests);
            this.dgTestSuiteList.CellContentClick += new DataGridViewCellEventHandler(this.dgTestSuiteList_CellContentClick);
            this.dgVariables.CellDoubleClick += new DataGridViewCellEventHandler(this.dgVariables_CellContentDoubleClick);
            this.dgTestSuiteList.CellDoubleClick += new DataGridViewCellEventHandler(this.dgTestSuiteList_CellContentDoubleClick);
            this.dgTestSuiteList.CellValueChanged += new DataGridViewCellEventHandler(this.dgTestSuiteList_OnCellValueChanged);
            this.dgTestSuiteList.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgTestSuiteList_CellPainting);
            this.btnExportResults.Click += new EventHandler(this.ExportResults_Click);
            this.lstRangeItems.DoubleClick += new EventHandler(this.ListRangeItems_DoubleClick);
            this.lstRanges.Click += new EventHandler(this.ListRange_Click);
            this.btnRangeAddToList.Click += new EventHandler(this.AddToRangeList_Click);
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnOpenResults.Click += new EventHandler(this.btnOpenResults_Click);
            this.exportToolStripMenuItem.Click += new EventHandler(this.ExportResultsMenuItem_Click);
            this.exportToolStripMenuItem1.Click += new EventHandler(this.ExportVariableMenuItem_Click);
            this.tbOptions.SelectedIndexChanged += new EventHandler(this.Tabs_SelectedIndexChanged);
            string tempWorkFolder = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (tbOptions.SelectedTab == tbPageOptions)
            {
                PopulateOptionsTab();
            }
            if (tbOptions.SelectedTab == tbPageTestSuite)
            {
                List<Utilities.TestSuite.TestRunList> testSuiteList = Utilities.TestSuite.GetTestRunList();
                IEnumerable<Utilities.TestSuite.TestRunList> ds2 = testSuiteList;
                JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.TestSuite.TestRunList>(dgTestSuiteList, bsTestRun, ds2);
                bsTestRun.DataSource = testSuiteList;
                dgTestSuiteList.DataSource = bsTestRun;
                dgTestSuiteList.Refresh();
                btnAddTest.Text = "Add";
                List<Utilities.Variables.VariableList> variableList = Utilities.Variables.GetVariableDocuments();
                lstVariableList.Items.Clear();
                foreach (Utilities.Variables.VariableList variable in variableList)
                {
                    lstVariableList.Items.Add(variable.VariableName + ";(Element=" + variable.SearchForElement + ")");
                }
                lstTestList.Items.Clear();
                lstTestNames.Items.Clear();
                lstVariables.Items.Clear();
                List<Utilities.TestSuite.Test> testList = Utilities.TestSuite.GetTests();
                foreach (Utilities.TestSuite.Test test in testList)
                {
                    lstTestList.Items.Add(test.TestName);
                }

                AddChkBoxHeader_DataGridView();
                btnDeleteTestSuite.Enabled = false;
                btnExportResults.Enabled = false;
                btnRunTests.Enabled = false;
            }
            if (tbOptions.SelectedTab == tbPageAuthentication)
            {
                if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == "yes")
                {
                    chkBearer.Checked = true;
                    txtTokenElement.Enabled = true;
                }
                else
                {
                    chkBearer.Checked = false;
                    System.Environment.SetEnvironmentVariable("bearerToken", "none", EnvironmentVariableTarget.User);
                    txtTokenElement.Enabled = false;
                    System.Environment.SetEnvironmentVariable("tokenElement", "none", EnvironmentVariableTarget.User);
                }
                if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == "yes")
                {
                    chkBasic.Checked = true;
                    txtUserName.Enabled = true;
                    txtPassword.Enabled = true;
                    btnGenerateKey.Enabled = true;
                    rtBasicKey.Enabled = true;
                    txtUserName.Text = System.Environment.GetEnvironmentVariable("úserName", EnvironmentVariableTarget.User);
                    txtPassword.Text = System.Environment.GetEnvironmentVariable("password", EnvironmentVariableTarget.User);
                    rtBasicKey.Text = System.Environment.GetEnvironmentVariable("basicToken", EnvironmentVariableTarget.User);
                }
                else
                {
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = false;
                    btnGenerateKey.Enabled = false;
                    rtBasicKey.Enabled = false;
                    System.Environment.SetEnvironmentVariable("úserName", "none", EnvironmentVariableTarget.User);
                    System.Environment.SetEnvironmentVariable("password", "none", EnvironmentVariableTarget.User);
                    System.Environment.SetEnvironmentVariable("basicToken", "none", EnvironmentVariableTarget.User);
                }
                if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == "yes")
                {
                    chkAPIKey.Checked = true;
                    rtAPIKey.Enabled = true;
                    rtAPIKey.Text = System.Environment.GetEnvironmentVariable("apiKey", EnvironmentVariableTarget.User);
                }
                else
                {
                    chkAPIKey.Checked = false;
                    rtAPIKey.Enabled = false;
                    System.Environment.SetEnvironmentVariable("apiKey", "none", EnvironmentVariableTarget.User);
                }
                if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == "yes")
                {
                    chkCustom.Checked = true;
                    rtCustomKey.Text = System.Environment.GetEnvironmentVariable("customKey", EnvironmentVariableTarget.User);
                }
                else
                {
                    chkCustom.Checked = false;
                    rtCustomKey.Enabled = false;
                    System.Environment.SetEnvironmentVariable("customKey", "none", EnvironmentVariableTarget.User);
                }
            }
            if (tbOptions.SelectedTab == tbPageRanges)
            {
                lstRanges.Items.Clear();
                List<Utilities.Ranges.RangeList> existingRanges = Utilities.Ranges.GetRangeLists();
                foreach (Utilities.Ranges.RangeList rangeItem in existingRanges)
                {
                    lstRanges.Items.Add(rangeItem.RangeName);
                }
                btnSaveRange.Text = "Add";
                btnRangeDelete.Enabled = false;
                btnRangeItemDelete.Enabled = false;
                btnRangeAddToList.Text = "Add Item";
            }
            Cursor.Current = Cursors.Default;
        }

        private void PopulateOptionsTab()
        {
            List<Utilities.Variables.VariableList> variableList = Utilities.Variables.GetVariableDocuments();
            IEnumerable<Utilities.Variables.VariableList> ds = variableList;
            JSONAPI.Controls.JSONAPIDataGridView.MakeSortable<Utilities.Variables.VariableList>(dgVariables, bsVariables, ds);
            bsVariables.DataSource = variableList;
            dgVariables.DataSource = bsVariables;
            dgVariables.Refresh();
            btnVariableSave.Text = "Add";

            if (System.Environment.GetEnvironmentVariable("useVariables", EnvironmentVariableTarget.User) == "yes")
            {
                grpOptionVariables.Enabled = true;
                dgVariables.Enabled = true;
            }
            else
            {
                grpOptionVariables.Enabled = false;
                dgVariables.Enabled = false;
            }
            if (System.Environment.GetEnvironmentVariable("useVariables", EnvironmentVariableTarget.User) == null)
            {
                System.Environment.SetEnvironmentVariable("useVariables", "no", EnvironmentVariableTarget.User);
            }
            if (System.Environment.GetEnvironmentVariable("useVariables", EnvironmentVariableTarget.User) == "yes")
            {
                chkUseVariables.Checked = true;
                grpOptionVariables.Enabled = true;
                dgVariables.Enabled = true;
            }
            else
            {
                chkUseVariables.Checked = false;
                grpOptionVariables.Enabled = false;
                dgVariables.Enabled = false;
            }
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tbOptions.SelectTab(tbPageOptions);
            PopulateOptionsTab();
            Cursor.Current = Cursors.Default;
        }

        /* ===============================================
         * Variables
         * ===============================================*/
        private void dgVariables_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.oldVariableName = (string)dgVariables[0, e.RowIndex].Value;
            this.oldSearchForElement = (string)dgVariables[1, e.RowIndex].Value;
            this.oldSavedValue = (string)dgVariables[2, e.RowIndex].Value;
            this.oldReplaceWhere = (string)dgVariables[3, e.RowIndex].Value;
            this.oldPartialSave = (string)dgVariables[4, e.RowIndex].Value;
            this.oldNumChars = (int)dgVariables[5, e.RowIndex].Value;
            btnVariableSave.Text = "Update";
            txtVariableName.Text = (string)dgVariables[0, e.RowIndex].Value;
            txtSearchElement.Text = (string)dgVariables[1, e.RowIndex].Value;
            txtSavedValue.Text = (string)dgVariables[2, e.RowIndex].Value;
            cmbReplaceWhere.Text = (string)dgVariables[3, e.RowIndex].Value;
            cmbSaveLen.Text = (string)dgVariables[4, e.RowIndex].Value;
            numEnd.Value = (int)dgVariables[5, e.RowIndex].Value;
        }

        private void dgVariables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update the balance column whenever the value of any cell changes.
            if (e.RowIndex > -1)
            {
                Utilities.Variables.UpdateVariableDocument(this.oldVariableName, txtVariableName.Text, this.oldSearchForElement, txtSearchElement.Text, this.oldSavedValue, this.txtSavedValue.Text, this.oldReplaceWhere, this.cmbReplaceWhere.Text, this.oldPartialSave, this.cmbSaveLen.Text, this.oldNumChars, (int)this.numEnd.Value);
                bsVariables.DataSource = Utilities.Variables.GetVariableDocuments();
                dgVariables.DataSource = bsVariables;
                dgVariables.Refresh();
            }
        }

        void dgVariables_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgVariables.BeginEdit(true);
            try
            {
                txtVariableName.Text = dgVariables.CurrentRow.Cells["colVariableName"].Value.ToString();
                txtSearchElement.Text = dgVariables.CurrentRow.Cells["colSearchForElement"].Value.ToString();
                txtSavedValue.Text = dgVariables.CurrentRow.Cells["colSavedValue"].Value.ToString();
                cmbReplaceWhere.Text = dgVariables.CurrentRow.Cells["colReplaceWhere"].Value.ToString();
                cmbSaveLen.Text = dgVariables.CurrentRow.Cells["colPartialSave"].Value.ToString();
                numEnd.Value = (int)dgVariables.CurrentRow.Cells["colNumChars"].Value;
                this.selectedRow = dgVariables.CurrentCell.RowIndex;
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some Variable DataGrid Issue:" + f.Message, TraceEventType.Error);
            }
        }

        private void tsMenuDeleteVariable_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string deleteItem = dgVariables.SelectedCells[0].Value.ToString();

            Utilities.Variables.DeleteVariableDocument(deleteItem);
            bsVariables.DataSource = Utilities.Variables.GetVariableDocuments();
            dgVariables.DataSource = bsVariables;
            dgVariables.Refresh();
            Cursor.Current = Cursors.Default;
        }

        private void dgVariables_RightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxVariableGridOptions.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void AddVariable_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string variableName = txtVariableName.Text;
            variableName = Regex.Replace(variableName, "[$|&]@", " ").Trim();
            string searchElement = txtSearchElement.Text;
            searchElement = Regex.Replace(searchElement, "[$|&]@", " ").Trim();
            string[] operation1Array = new string[] { "NUMBER", "TEXT", "VARIABLE", "YESTERDAY", "TODAY", "TOMORROW", "GUID", "RANGE" };
            bool continueFlag = true;
            for (int c= 0; c<operation1Array.Count(); c++)
            {
                if (variableName.ToLower().Contains(operation1Array[c].ToLower()))
                {
                    continueFlag = false;
                }
            }
            if (continueFlag)
            {
                string saveLen = cmbSaveLen.Text;
                if (saveLen == null | saveLen == "")
                {
                    saveLen = "All";
                }
                if (btnVariableSave.Text.ToLower() == "add")
                {
                    Utilities.Variables.NewVariableDocument(variableName, searchElement, this.txtSavedValue.Text, this.cmbReplaceWhere.Text, saveLen, (int)this.numEnd.Value);
                }
                else
                {
                    Utilities.Variables.UpdateVariableDocument(this.oldVariableName, variableName, this.oldSearchForElement, searchElement, this.oldSavedValue, this.txtSavedValue.Text, this.oldReplaceWhere, this.cmbReplaceWhere.Text, this.oldPartialSave, saveLen, this.oldNumChars, (int)this.numEnd.Value);
                    btnVariableSave.Text = "Add";
                }
                bsVariables.DataSource = Utilities.Variables.GetVariableDocuments();
                dgVariables.DataSource = bsVariables;
                dgVariables.Refresh();
                txtSearchElement.Text = "";
                txtVariableName.Text = "";
                txtSavedValue.Text = "";
                cmbReplaceWhere.Text = "";
                cmbSaveLen.Text = "";
                numEnd.Value = 0;

            }
            else
            {
                MessageBox.Show("Variable name must not be a reserved word.  Please chooose another Name.");
            }
            Cursor.Current = Cursors.Default;
        }
        public void UseVariables_Selected(object sender, EventArgs e)
        {
            if (chkUseVariables.Checked)
            {
                grpOptionVariables.Enabled = true;
                dgVariables.Enabled = true;
                System.Environment.SetEnvironmentVariable("useVariables", "yes", EnvironmentVariableTarget.User);
            }
            else
            {
                grpOptionVariables.Enabled = false;
                dgVariables.Enabled = false;
                System.Environment.SetEnvironmentVariable("useVariables", "no", EnvironmentVariableTarget.User);
            }
        }

        private void CancelVariable_Click(object sender, System.EventArgs e)
        {
            txtVariableName.Text = "";
            txtSearchElement.Text = "";
            txtSavedValue.Text = "";
            cmbReplaceWhere.Text = "";
            cmbSaveLen.Text = "";
            numEnd.Value = 0;
        }

        public void ExportVariableMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel List";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "Variables.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgVariables, fileName);
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


        /* ===============================================
         * Test Suite
         * ===============================================*/
        public void ClickHandler(List<Utilities.TestSuite.TestList> testList)
        {
            frmTestList Form1 = new frmTestList(testList);
            Form1.TopMost = true;
            Form1.ShowDialog();
        }
        private void dgTestSuiteList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                //They clicked the header column, do nothing
                return;
            }

            if (grid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
            {
                if (e.ColumnIndex == 1)
                {
                    var testRunList = (Utilities.TestSuite.TestRunList)grid.Rows[e.RowIndex].DataBoundItem;
                    var testList = testRunList.TestList;
                    frmTestList tlForm = new frmTestList(testList);
                    tlForm.TopMost = true;
                    tlForm.Show();
                }
                else
                {
                    var testRunList = (Utilities.TestSuite.TestRunList)grid.Rows[e.RowIndex].DataBoundItem;
                    var testHistory = Utilities.ExecutionResult.GetExecutedRunList_testSuite(testRunList.TestSuiteName);
                    frmTestRunHistory thForm = new frmTestRunHistory(testHistory);
                    thForm.TopMost = true;
                    thForm.Show();
                }

            }
            if (e.ColumnIndex == colSelected.Index)
            {
                btnDeleteTestSuite.Enabled = true;
                btnExportResults.Enabled = true;
                btnRunTests.Enabled = true;
            }
        }

        private void AddTest_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string testSuiteName = txtTestName.Text;
            bool includeOutput = false;
            string jsonStr = "{" + Environment.NewLine;
            string baseFolder = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);
            if (baseFolder.Length == 0)
            {
                baseFolder = "C:\\JSONAPI\\";
            }
            if (testSuiteName.Length > 0 & lstTestNames.Items.Count > 0)
            {
                if (chkExportResults.Checked) includeOutput = true;

                if (btnAddTest.Text == "Add Item")
                {
                    var testRunList = new Utilities.TestSuite.TestRunList() { TestSuiteName = txtTestName.Text, LastExecuted = "", TestStatus = "Not Executed", IncludeOutputResults = includeOutput,  RunOutput = txtSampleOutput.Text };
                    for (int i = 0; i < lstTestNames.Items.Count; i++)
                    { 
                        testRunList.TestList.Add(new Utilities.TestSuite.TestList(lstTestNames.Items[i].ToString(), 1, "Not Executed", "", "", "", "", txtTestName.Text));
                        //string fileName = baseFolder + lstTestNameList.Items[i].ToString() + ".json";
                        //string inputString = File.ReadAllText(fileName);
                        //Utilities.TestSuite.NewTestDocument(lstTestNameList.Items[i].ToString(), Utilities.Utilities.BuildTestFieldList(testSuiteName, lstTestNameList.Items[i].ToString(), inputString));
                    }
                    Utilities.TestSuite.NewTestSuiteDocument(testRunList);
                }
                else
                {
                    List<Utilities.TestSuite.TestList> testList = new List<Utilities.TestSuite.TestList>();
                    for (int i = 0; i < lstTestNames.Items.Count; i++)
                    {
                        Utilities.TestSuite.TestList test = new Utilities.TestSuite.TestList(lstTestNames.Items[i].ToString(), 1, "Not Executed", "", "", "", "", txtTestName.Text);
                        testList.Add(test);
                    }
                    Utilities.TestSuite.UpdateTestSuite(testSuiteName, txtSampleOutput.Text, includeOutput, testList);
                }
                var testRunList2 = Utilities.TestSuite.GetTestRunList();
                bsTestRun.DataSource = testRunList2;
                dgTestSuiteList.DataSource = bsTestRun;
                dgTestSuiteList.Refresh();
                txtTestName.Text = "";
                lstTestNames.Items.Clear();
                lstTestList.Items.Clear();
                lstVariables.Items.Clear();
                lstVariableList.Items.Clear();

                List<Utilities.TestSuite.Test> testList2 = Utilities.TestSuite.GetTests();

                foreach (Utilities.TestSuite.Test test in testList2)
                {
                    lstTestList.Items.Add(test.TestName);
                }

                List<Utilities.Variables.VariableList> variableList = Utilities.Variables.GetVariableDocuments();

                foreach (Utilities.Variables.VariableList variable in variableList)
                {
                    lstVariableList.Items.Add(variable.VariableName + ";(Element=" + variable.SearchForElement + ")");
                }
                btnAddTest.Text = "Add Item";
                txtSampleOutput.Text = "";
                txtTestName.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please specify a name for the Test Suite and Select Test(s) to execute.");
            }
            
            Cursor.Current = Cursors.Default;
        }

        public void ExportResultsMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "TestSuiteResults.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgTestSuiteList, fileName);
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

        private void AddSingleTest(object sender, System.EventArgs e)
        {
            for (int i = (lstTestList.SelectedItems.Count - 1); i >= 0; i--)
            {
                lstTestNames.Items.Add(lstTestList.SelectedItems[i]);
                lstTestList.Items.Remove(lstTestList.SelectedItems[i]);
            }
        }

        private void RemoveSingleTest(object sender, System.EventArgs e)
        {
            for (int i = (lstTestNames.SelectedItems.Count - 1); i >= 0; i--)
            {
                lstTestList.Items.Add(lstTestNames.SelectedItems[i]);
                lstTestNames.Items.Remove(lstTestNames.SelectedItems[i]);
            }
        }

        private void AddAllTests(object sender, System.EventArgs e)
        {
            for (int i = 0; i < (lstTestList.Items.Count); i++)
            {
                lstTestNames.Items.Add(lstTestList.Items[i]);
            }
            lstTestList.Items.Clear();
        }

        private void RemoveAllTests(object sender, System.EventArgs e)
        {
            for (int i = 0; i < (lstTestNames.Items.Count); i++)
            {
                lstTestList.Items.Add(lstTestNames.Items[i]);
            }
            lstTestNames.Items.Clear();
        }

        private void AddSingleVariable(object sender, System.EventArgs e)
        {
            string jsonStr = "{" + Environment.NewLine;
            for (int j = (lstVariableList.SelectedItems.Count - 1); j >= 0; j--)
            {
                lstVariables.Items.Add(lstVariableList.SelectedItems[j]);
                lstVariableList.Items.Remove(lstVariableList.SelectedItems[j]);
            }
            for (int i = 0; i < lstVariables.Items.Count; i++)
            {
                //variable.VariableName + ";(Element=" + variable.SearchForElement + ")")
                var colVariableNameArray = lstVariables.Items[i].ToString().Split("=".ToCharArray());
                colVariableNameArray = colVariableNameArray[1].Split(")".ToCharArray());
                if (i == (lstVariables.Items.Count - 1)) jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")" + Environment.NewLine;
                else jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")," + Environment.NewLine;
            }
            jsonStr += "}";
            txtSampleOutput.Text = jsonStr;
        }

        private void RemoveSingleVariable(object sender, System.EventArgs e)
        {
            string jsonStr = "{" + Environment.NewLine;
            for (int j = (lstVariables.SelectedItems.Count - 1); j >= 0; j--)
            {
                lstVariableList.Items.Add(lstVariables.SelectedItems[j]);
                lstVariables.Items.Remove(lstVariables.SelectedItems[j]);
            }
            for (int i = 0; i < lstVariables.Items.Count; i++)
            {
                //variable.VariableName + ";(Element=" + variable.SearchForElement + ")")
                var colVariableNameArray = lstVariables.Items[i].ToString().Split("=".ToCharArray());
                colVariableNameArray = colVariableNameArray[1].Split(")".ToCharArray());
                if (i == (lstVariables.Items.Count - 1)) jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")" + Environment.NewLine;
                else jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")," + Environment.NewLine;
            }
            jsonStr += "}";
            txtSampleOutput.Text = jsonStr;
        }

        private void AddAllVariables(object sender, System.EventArgs e)
        {
            string jsonStr = "{" + Environment.NewLine;
            for (int j = 0; j < (lstVariableList.Items.Count); j++)
            {
                lstVariables.Items.Add(lstVariableList.Items[j]);
            }
            lstVariableList.Items.Clear();
            for (int i = 0; i < lstVariables.Items.Count; i++)
            {
                //variable.VariableName + ";(Element=" + variable.SearchForElement + ")")
                var colVariableNameArray = lstVariables.Items[i].ToString().Split("=".ToCharArray());
                colVariableNameArray = colVariableNameArray[1].Split(")".ToCharArray());
                if (i == (lstVariables.Items.Count - 1)) jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")" + Environment.NewLine;
                else jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")," + Environment.NewLine;
            }
            jsonStr += "}";
            txtSampleOutput.Text = jsonStr;
        }

        private void RemoveAllVariables(object sender, System.EventArgs e)
        {
            string jsonStr = "{" + Environment.NewLine;
            for (int j = 0; j < (lstVariableList.Items.Count); j++)
            {
                lstVariableList.Items.Add(lstVariables.Items[j]);
            }
            lstVariables.Items.Clear();
            for (int i = 0; i < lstVariables.Items.Count; i++)
            {
                //variable.VariableName + ";(Element=" + variable.SearchForElement + ")")
                var colVariableNameArray = lstVariables.Items[i].ToString().Split("=".ToCharArray());
                colVariableNameArray = colVariableNameArray[1].Split(")".ToCharArray());
                if (i == (lstVariables.Items.Count - 1)) jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")" + Environment.NewLine;
                else jsonStr += "\"" + colVariableNameArray[0] + "\"" + " : REPLACE(" + i.ToString() + ")," + Environment.NewLine;
            }
            jsonStr += "}";
            txtSampleOutput.Text = jsonStr;
        }

        void dgTestSuiteList_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {         
            try
            {
                txtSampleOutput.Text = dgTestSuiteList.CurrentRow.Cells["colRunOutput"].Value.ToString();
                txtTestName.Text = dgTestSuiteList.CurrentRow.Cells["colTestSuiteName"].Value.ToString();
                txtTestName.Enabled = false;
                chkExportResults.Checked = (bool) dgTestSuiteList.CurrentRow.Cells["colExportTestResults"].Value;
                List<Utilities.TestSuite.TestList> testList = Utilities.TestSuite.GetTestList(txtTestName.Text);
                lstTestNames.Items.Clear();
                foreach (Utilities.TestSuite.TestList test in testList)
                {
                    lstTestNames.Items.Add(test.TestName);
                    lstTestList.Items.Remove(test.TestName);
                }
                lstVariables.Items.Clear();
                btnAddTest.Text = "Update Item";
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some TestSuiteList DataGrid Issue:" + f.Message, TraceEventType.Error);
            }
        }

        public void RunSeletectedTests_Files(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Utilities.Utilities.ClearSessionVariables();
            string tempValue = "";
            int incrementCounter = 1;
            Boolean testStatus = true;
            Boolean testRunStatus = true;
            System.Environment.SetEnvironmentVariable("execMode", "batch", EnvironmentVariableTarget.User);
            foreach (DataGridViewRow row in dgTestSuiteList.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    string testSuiteName = row.Cells["colTestSuiteName"].Value.ToString();
                    string testRunName = DateTime.Now.ToString("yyyyMMddhh24mmss");
                    System.Environment.SetEnvironmentVariable("testRunName", testRunName, EnvironmentVariableTarget.User);
                    List<Utilities.TestSuite.TestList> testList = Utilities.TestSuite.GetTestList(testSuiteName);
                    for (var i = 0; i < testList.Count; i++)
                    {
                        testStatus = true;
                        incrementCounter = 1;
                        string tempWorkFolder = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);
                        string testName = testList[i].TestName;
                        string filePath = tempWorkFolder + testList[i].TestName + ".json";
                        if (File.Exists(filePath))
                        {
                            System.Environment.SetEnvironmentVariable("fileType", "json", EnvironmentVariableTarget.User);
                        }
                        else
                        {
                            System.Environment.SetEnvironmentVariable("fileType", "xml", EnvironmentVariableTarget.User);
                            filePath = tempWorkFolder + testList[i].TestName + ".xml";
                        }
                        System.Environment.SetEnvironmentVariable("editFile", filePath, EnvironmentVariableTarget.User);
                        string bodyResponse = "No response";
                        string headerResponse = "No response";
                        Utilities.ExecutionResult.NewTestRun(testRunName, testSuiteName, testName);
                        System.Environment.SetEnvironmentVariable("testRunName", testRunName, EnvironmentVariableTarget.User);
                        System.Environment.SetEnvironmentVariable("testSuiteName", testSuiteName, EnvironmentVariableTarget.User);
                        System.Environment.SetEnvironmentVariable("testName", testName, EnvironmentVariableTarget.User);
                        if (File.Exists(filePath))
                        {
                            string inputString = File.ReadAllText(filePath);
                            System.Environment.SetEnvironmentVariable("jsonFlag", "yes", EnvironmentVariableTarget.User);
                            //Generate Details to send

                            List<string> detailMessages = Utilities.Utilities.GenerateBatchDetail(inputString);
                            foreach (string generatedDetailMessage in detailMessages)
                            {
                                Dictionary<string, string> responseDict = Utilities.Utilities.SendHttpRequest(generatedDetailMessage, incrementCounter, testSuiteName, testName, "In Progress");
                                if (responseDict.TryGetValue("Response", out tempValue))
                                {
                                    if (tempValue.ToLower() == "ok")
                                    {
                                        testStatus = true;
                                    }
                                    else
                                    {
                                        testStatus = false;
                                        if (testRunStatus != false)
                                        {
                                            testRunStatus = true;
                                        }
                                        else
                                        {
                                            testRunStatus = false;
                                        }
                                    }
                                }
                                foreach (string key in responseDict.Keys)
                                {
                                    if (key.ToLower().Contains("header_"))
                                    {
                                        headerResponse += "\"" + key.Substring(7) + "\"" + ":" + "\"" + responseDict[key] + "\"" + ",";
                                    }
                                }
                                if (responseDict.TryGetValue("Body", out tempValue))
                                {
                                    bodyResponse = responseDict["Body"].ToString();
                                }
                                string headerRequest = "";
                                if (responseDict.TryGetValue("HeaderRequest", out tempValue))
                                {
                                    headerRequest = responseDict["HeaderRequest"].ToString();
                                }
                                if (testStatus) { tempValue = "Pass"; }
                                if (!testStatus) { tempValue = "Fail"; }
                                Utilities.TestSuite.UpdateTestStatus_NewInstance(testSuiteName, testName, tempValue, headerRequest, generatedDetailMessage, headerResponse, bodyResponse, incrementCounter);
                                incrementCounter++;
                            }
                        }
                    }
                    if (testRunStatus) { tempValue = "Pass"; }
                    if (!testRunStatus) { tempValue = "Fail"; }
                    Utilities.TestSuite.UpdateTestSuiteStatus(testSuiteName, tempValue);
                    bsTestRun.DataSource = Utilities.TestSuite.GetTestRunList();
                    dgTestSuiteList.DataSource = bsTestRun;
                    dgTestSuiteList.Refresh();
                    testStatus = true;
                }
            }

            Cursor.Current = Cursors.Default;
        }


        public void RunSelectedTests(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Utilities.Utilities.ClearSessionVariables();
            System.Environment.SetEnvironmentVariable("execMode", "batch", EnvironmentVariableTarget.User);
            foreach (DataGridViewRow row in dgTestSuiteList.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    string testSuiteName = row.Cells["colTestSuiteName"].Value.ToString();
                    
                    Utilities.TestSuite.RunTestSuite(testSuiteName);
                    //Utilities.TestSuite.HandleTestSuiteOutput(testSuiteName, "AutoGeneratedTestResults.csv");

                    bsTestRun.DataSource = Utilities.TestSuite.GetTestRunList();
                    dgTestSuiteList.DataSource = bsTestRun;
                    dgTestSuiteList.Refresh();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public void DeleteSelectedTests(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (DataGridViewRow row in dgTestSuiteList.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    Utilities.TestSuite.DeleteTestSuite(row.Cells["coltestSuiteName"].Value.ToString());
                }
            }
            bsTestRun.DataSource = Utilities.TestSuite.GetTestRunList();
            dgTestSuiteList.DataSource = bsTestRun;
            dgTestSuiteList.Refresh();
            Cursor.Current = Cursors.Default;
        }

        private void ExportResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            int selectCount = 0;
            foreach (DataGridViewRow row in dgTestSuiteList.Rows)
            {
                if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                {
                    selectCount++;
                }
            }
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "Results.csv";
            if (selectCount > 0)
            {
                dialog.ShowDialog();
                string fileName = dialog.FileName;
                Cursor.Current = Cursors.WaitCursor;
                string result = Utilities.TestSuite.ExportTestResultsToCsv(dgTestSuiteList, fileName);
                Cursor.Current = Cursors.Default;
                if (result != "success")
                {
                    MessageBox.Show("Export failed due to: " + result);
                }
                else
                {
                    MessageBox.Show("Export successful to file: " + fileName);
                }
            }
        }

        public void btnOpenResults_Click(object sender, EventArgs e)
        {
            frmExpectedResults expectedResultsForm = new frmExpectedResults();
            expectedResultsForm.TopMost = true;
            expectedResultsForm.ShowDialog();
        }

        private void AddChkBoxHeader_DataGridView()
        {
            Rectangle rect = dgTestSuiteList.GetCellDisplayRectangle(0, -1, true);
            rect.Y = rect.Location.Y + 5;
            rect.X = rect.Location.X + 55;
            checkboxHeader = new CheckBox();
            checkboxHeader.Size = new Size(15, 15);
            checkboxHeader.Location = rect.Location;
            dgTestSuiteList.Controls.Add(checkboxHeader);
            checkboxHeader.MouseClick += new MouseEventHandler(checkboxHeader_CheckedChanged);
        }


        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBoxClick(CheckBox headerCheckbox)
        {
            isHeaderCheckBoxClicked = true;
            foreach (DataGridViewRow r in dgTestSuiteList.Rows)
            {
                ((DataGridViewCheckBoxCell)r.Cells[0]).Value = headerCheckbox.Checked;
            }
            dgTestSuiteList.RefreshEdit();
            isHeaderCheckBoxClicked = false;
        }


        private void dgTestSuiteList_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colSelected.Index)
            {
                btnDeleteTestSuite.Enabled = true;
                btnExportResults.Enabled = true;
                btnRunTests.Enabled = true;
            }
        }

        private void dgTestSuiteList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    DataGridViewCell cell = this.dgTestSuiteList.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                Utilities.Utilities.WriteLogItem("Error defining Button for dgTestSuiteList" + fe.ToString(), TraceEventType.Error);
            }
        }


        /* ===============================================
         * Authentication
         * ===============================================*/
        public void BasicAuth_Selected(object sender, EventArgs e)
        {
            if (chkBasic.Checked)
            {
                this.chkBearer.Checked = false;
                this.chkAPIKey.Checked = false;
                this.chkCustom.Checked = false;
                System.Environment.SetEnvironmentVariable("useBearerToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBasicToken", "yes", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useCustomKey", "no", EnvironmentVariableTarget.User);
                this.txtPassword.Enabled = true;
                this.txtUserName.Enabled = true;
                this.btnGenerateKey.Enabled = true;
                this.rtBasicKey.Enabled = true;
            }
            else
            {
                this.txtPassword.Enabled = false;
                this.txtUserName.Enabled = false;
                this.btnGenerateKey.Enabled = false;
                this.rtBasicKey.Enabled = false;
                System.Environment.SetEnvironmentVariable("useBasicToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("basicToken", "none", EnvironmentVariableTarget.User);
            }
        }

        public void BearerToken_Selected(object sender, EventArgs e)
        {
            if (chkBearer.Checked)
            {
                this.chkBasic.Checked = false;
                this.chkAPIKey.Checked = false;
                this.chkCustom.Checked = false;
                System.Environment.SetEnvironmentVariable("useBearerToken", "yes", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBasicToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useCustomKey", "no", EnvironmentVariableTarget.User);
                if (txtTokenElement.Text != "") System.Environment.SetEnvironmentVariable("tokenElement", txtTokenElement.Text, EnvironmentVariableTarget.User);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("useBearerToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("bearerToken", "none", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("tokenElement", "none", EnvironmentVariableTarget.User);
            }
        }

        public void APIKey_Selected(object sender, EventArgs e)
        {
            if (chkAPIKey.Checked)
            {
                this.chkBasic.Checked = false;
                this.chkBearer.Checked = false;
                this.chkCustom.Checked = false;
                System.Environment.SetEnvironmentVariable("useBearerToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBasicToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useCustomKey", "yes", EnvironmentVariableTarget.User);
                this.rtAPIKey.Enabled = true;
            }
            else
            {
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("apiKey", "none", EnvironmentVariableTarget.User);
                this.rtAPIKey.Enabled = false;
            }
        }

        public void CustomKey_Selected(object sender, EventArgs e)
        {
            if (chkCustom.Checked)
            {
                this.chkBasic.Checked = false;
                this.chkBearer.Checked = false;
                this.chkAPIKey.Checked = false;
                System.Environment.SetEnvironmentVariable("useBearerToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBasicToken", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useCustomKey", "yes", EnvironmentVariableTarget.User);
                this.rtCustomKey.Enabled = true;
            }
            else
            {
                System.Environment.SetEnvironmentVariable("useCustomKey", "no", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("customKey", "none", EnvironmentVariableTarget.User);
                this.rtCustomKey.Enabled = false;
            }

        }

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            var byteArray = Encoding.ASCII.GetBytes(txtUserName.Text + ":" + txtPassword.Text);
            string basicKey = Convert.ToBase64String(byteArray);
            rtBasicKey.Text = basicKey;
            System.Environment.SetEnvironmentVariable("userName", txtUserName.Text, EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("password", txtPassword.Text, EnvironmentVariableTarget.User);
            System.Environment.SetEnvironmentVariable("basicKey", basicKey, EnvironmentVariableTarget.User);
        }

        private void rtAPIKey_TextChanged(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("apiKey", rtAPIKey.Text, EnvironmentVariableTarget.User);
        }

        private void rtCustomKey_TextChanged(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("customKey", rtCustomKey.Text, EnvironmentVariableTarget.User);
        }

        private void txtTokenElement_Change(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable("tokenElement", txtTokenElement.Text, EnvironmentVariableTarget.User);
        }

        /* ===============================================
         * Ranges
         * ===============================================*/
        private void txtRange_CellBeginEdit(object sender, EventArgs e)
        {
            this.oldRangeName = txtRangeName.Text;
        }

        private void btnSaveRange_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string[] rList = lstRangeItems.Items.OfType<string>().ToArray();
            if (btnSaveRange.Text == "Add")
            {         
                Utilities.Ranges.NewRangeDocument(txtRangeName.Text, txtRangeDesc.Text, rList);

                btnSaveRange.Text = "Update";
            }
            else
            {
                if (this.oldRangeName.Length == 0) this.oldRangeName = txtRangeName.Text;
                Utilities.Ranges.UpdateRangeValues(this.oldRangeName, txtRangeName.Text, txtRangeDesc.Text, rList);
                txtRangeName.Text = "";
                txtRangeDesc.Text = "";
                txtRangeValue.Text = "";
                lstRangeItems.Items.Clear();
                btnSaveRange.Text = "Add";
            }
            lstRanges.Items.Clear();
            List<Utilities.Ranges.RangeList> existingRanges = Utilities.Ranges.GetRangeLists();
            foreach (Utilities.Ranges.RangeList rangeItem in existingRanges)
            {
                lstRanges.Items.Add(rangeItem.RangeName);
            }
            Cursor.Current = Cursors.Default;
        }

        private void ListRange_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string rList = lstRanges.SelectedItem.ToString();
            Utilities.Ranges.RangeList rangeDetails = Utilities.Ranges.GetRangeListDetails(rList);
            if (rangeDetails != null)
            {
                txtRangeName.Text = rangeDetails.RangeName;
                txtRangeDesc.Text = rangeDetails.RangeDescription;
                lstRangeItems.Items.Clear();

                for (int i = 0; i < rangeDetails.RangeValues.Length; i++)
                {
                    if (rangeDetails.RangeValues[i] != null)
                        lstRangeItems.Items.Add(rangeDetails.RangeValues[i].ToString());
                }
                btnSaveRange.Text = "Update";
                btnRangeAddToList.Text = "Add Item";
                btnRangeDelete.Enabled = true;
            }
            else
            {
                btnSaveRange.Text = "Add";
                btnRangeDelete.Enabled = false;
            }
            Cursor.Current = Cursors.Default;
        }
        private void AddToRangeList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            lstRangeItems.Items.Add(txtRangeValue.Text.Trim());
            txtRangeValue.Text = "";
            Cursor.Current = Cursors.Default;
        }

        private void ListRangeItems_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnRangeItemDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void ListRangeItems_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtRangeValue.Text = lstRangeItems.SelectedItem.ToString();
            btnRangeAddToList.Text = "Update Item";
            btnRangeItemDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
            btnRangeAddToList.Text = "Add Item";
        }

        private void btnRangeDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Utilities.Ranges.DeleteRangeDocument(txtRangeName.Text);
            lstRanges.Items.Clear();
            List<Utilities.Ranges.RangeList> existingRanges = Utilities.Ranges.GetRangeLists();
            foreach (Utilities.Ranges.RangeList rangeItem in existingRanges)
            {
                lstRanges.Items.Add(rangeItem.RangeName);
            }
            txtRangeName.Text = "";
            txtRangeDesc.Text = "";
            txtRangeValue.Text = "";
            lstRangeItems.Items.Clear();
            btnSaveRange.Text = "Add";
            Cursor.Current = Cursors.Default;
        }

        private void btnRangeItemDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string selectedValue = lstRangeItems.GetItemText(lstRangeItems.SelectedItem); ;
            if (selectedValue == "") selectedValue = txtRangeValue.Text;
            string[] updatedList = new string[1000];
            int c = 0;
            foreach (string item in lstRangeItems.Items)
            {
                if (item != selectedValue )
                {
                    updatedList[c] = item;
                    c++;
                }
            }
            Utilities.Ranges.UpdateRangeValues(txtRangeName.Text, txtRangeName.Text, txtRangeDesc.Text, updatedList);
            lstRangeItems.Items.Clear();
            string[] existingRangeItems = Utilities.Ranges.GetRangeListValues(txtRangeName.Text.ToString());
            for (int d = 0; d < existingRangeItems.Length; d++)
            {
                if (existingRangeItems[d] != null)
                    lstRangeItems.Items.Add(existingRangeItems[d]);
            }
            txtRangeValue.Text = "";
            Cursor.Current = Cursors.Default;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                // Calculate new index using move direction
                if (lstTestNames.SelectedItem == null)
                    return;

                var idx = lstTestNames.SelectedIndex;
                var elem = lstTestNames.SelectedItem;
                lstTestNames.Items.RemoveAt(idx);
                lstTestNames.Items.Insert(idx - 1, elem);
            }
            catch (Exception me)
            {
                Utilities.Utilities.WriteLogItem(me.ToString(), TraceEventType.Warning);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {// Calculate new index using move direction
                if (lstTestNames.SelectedItem == null)
                    return;

                var idx = lstTestNames.SelectedIndex;
                var elem = lstTestNames.SelectedItem;
                lstTestNames.Items.RemoveAt(idx);
                lstTestNames.Items.Insert(idx + 1, elem);
            }
            catch (Exception me)
            {
                Utilities.Utilities.WriteLogItem(me.ToString(), TraceEventType.Warning);
            }
        }

    }
}        
