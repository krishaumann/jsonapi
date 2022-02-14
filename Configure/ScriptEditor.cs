using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONAPI.Configure
{
    public partial class frmScriptEditor : Form
    {
        public string oldTestName = "";
        public string oldInput = "";
        public string oldURL = "";
        public string oldXPath = "";
        public string oldOperation = "";
        public int selectedRow = 0;

        public frmScriptEditor()
        {
            InitializeComponent();
            cmbURL.Items.Clear();
            List<String> tempList = Utilities.GUIObjects.GetGUIURLs();
            foreach (string objStr in tempList)
            {
                cmbURL.Items.Add(objStr);
            }

            bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
            dgGUISteps.DataSource = bsGUISteps;
            dgGUISteps.Refresh();
            btnAddGUITestStep.Text = "Add";
            AddEventHandlers();
        }

        public frmScriptEditor(string testName)
        {
            InitializeComponent();
            cmbURL.Items.Clear();
            List<String> tempList = Utilities.GUIObjects.GetGUIURLs();
            foreach (string objStr in tempList)
            {
                cmbURL.Items.Add(objStr);
            }

            bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests(testName);
            dgGUISteps.DataSource = bsGUISteps;
            dgGUISteps.Refresh();
            btnAddGUITestStep.Text = "Update";
            AddEventHandlers();
            dgGUISteps.Rows[0].Selected = true;

            txtStepName.Text = testName;
            numSequence.Value = (int)dgGUISteps.Rows[0].Cells["colSequence"].Value;
            txtElementName.Text = dgGUISteps.Rows[0].Cells["colTestName"].Value.ToString();
            cmbURL.Text = dgGUISteps.Rows[0].Cells["colURL"].Value.ToString();
            cmbObjectMapItem.Text = dgGUISteps.Rows[0].Cells["colXPath"].Value.ToString();
            cmbOperationDesc.Text = dgGUISteps.Rows[0].Cells["colOperation"].Value.ToString();
            cmbInputData.Text = dgGUISteps.Rows[0].Cells["colInput"].Value.ToString();
            txtInputValue.Text = dgGUISteps.Rows[0].Cells["colInput"].Value.ToString();
            var tempExpectedResults = Utilities.TestSuite.GetFieldTestExpectedResults(txtStepName.Text, cmbObjectMapItem.Text).Split(",");
            cmbAttribute.Text = tempExpectedResults[0];
            cmbVerificationType.Text = tempExpectedResults[1];
            txtExpectedValue.Text = tempExpectedResults[2];
            btnAddGUITestStep.Text = "Update";

            this.oldTestName = txtStepName.Text;
        }

        private void AddEventHandlers()
        {
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.dgGUISteps.CellDoubleClick += new DataGridViewCellEventHandler(this.dgGUISteps_CellContentDoubleClick);
        }

        private void cmbOperationDesc_TextChanged(object sender, EventArgs e)
        {
            if (cmbOperationDesc.Text.ToLower() == "validate" || cmbOperationDesc.Text.ToLower() == "hover" || cmbOperationDesc.Text.ToLower() == "click")
            {
                cmbInputData.Enabled = false;
                txtInputValue.Enabled = false;
                cmbInputRV.Visible = false;
                if (cmbOperationDesc.Text.ToLower() == "validate")
                {
                    txtElementName.Enabled = true;
                    cmbAttribute.Enabled = true;
                    cmbVerificationType.Enabled = true;
                    txtExpectedValue.Enabled = true;
                }
            }
            else
            {
                cmbInputData.Enabled = true;
                txtInputValue.Enabled = false;
                cmbInputRV.Visible = false;
                txtElementName.Enabled = false;
                cmbAttribute.Enabled = false;
                cmbVerificationType.Enabled = false;
                txtExpectedValue.Enabled = false;
            }
        }

        private void dgGUISteps_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.oldTestName = (string)dgGUISteps[0, e.RowIndex].Value;


            btnAddGUITestStep.Text = "Update";
        }

        private void cmbObjectMapItem_TextChanged(object sender, EventArgs e)
        {
            txtElementName.Text = cmbObjectMapItem.Text;
        }

        private void cmbURL_TextChanged(object sender, EventArgs e)
        {
            List<String> tempList = new List<String>();
            if (cmbURL.Text.Length > 0)
            {
                tempList = Utilities.GUIObjects.GetGUIObjectFromURL(cmbURL.Text);
            }
            else
            {
                tempList = Utilities.GUIObjects.GetGUIObjectFromURL();
            }
            cmbObjectMapItem.Items.Clear();
            foreach (string objStr in tempList)
            {
                cmbObjectMapItem.Items.Add(objStr);
            }
            
        }


        private void cmbInputData_TextChanged(object sender, EventArgs e)
        {
            
            if (cmbInputData.Text.ToLower() == "range" || cmbInputData.Text.ToLower() == "variable")
            {
                if (cmbInputData.Text.ToLower() == "range")
                {
                    cmbInputRV.Items.Clear();
                    List<Utilities.Ranges.RangeList> existingRanges = Utilities.Ranges.GetRangeLists();
                    foreach (Utilities.Ranges.RangeList rangeItem in existingRanges)
                    {
                        cmbInputRV.Items.Add(rangeItem.RangeName);
                    }
                }
                if (cmbInputData.Text.ToLower() == "variable")
                {
                    cmbInputRV.Items.Clear();
                    List<Utilities.Variables.VariableList> existingVariables = Utilities.Variables.GetVariableDocuments();
                    foreach (Utilities.Variables.VariableList variableItem in existingVariables)
                    {
                        cmbInputRV.Items.Add(variableItem.VariableName);
                    }
                }
                if (cmbInputRV.Items.Count == 0)
                {
                    cmbInputRV.Items.Add("None available");
                }
                txtInputValue.Visible = false;
                cmbInputRV.Visible = true;
            }
            else
            {
                txtInputValue.Enabled = true;
                txtInputValue.Visible = true;
                cmbInputRV.Visible = false;
            }
        }

        private void cmbVerificationType_TextChanged(object sender, EventArgs e)
        {
            if (cmbVerificationType.Text.ToLower() == "exist" || cmbVerificationType.Text.ToLower() == "doesnotexist")
            {
                txtExpectedValue.Enabled = false;
            }
            else
            {
                txtExpectedValue.Enabled = true;
            }
        }

        private void btnOpenObjectMap_Click(object sender, EventArgs e)
        {
            frmObjectMap newOMForm = new frmObjectMap();
            newOMForm.TopMost = true;
            newOMForm.WindowState = FormWindowState.Maximized;
            newOMForm.Show();
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {

            if (cmbObjectMapItem.Text.Length > 0)
            {
                if (btnAddGUITestStep.Text.ToLower() == "add")
                {
                    string inputString = "";
                    if (txtInputValue.Enabled) inputString = txtInputValue.Text;
                    else inputString = cmbInputData.Text;
                    Utilities.TestSuite.NewTestWithDetail(txtStepName.Text, "", null, inputString, (int)numSequence.Value, cmbObjectMapItem.Text);
                    string expectedResult = cmbAttribute.Text + "," + cmbVerificationType.Text + "," + txtExpectedValue.Text;
                    List<Utilities.TestSuite.FieldExpectedResult> expectedResultList = new List<Utilities.TestSuite.FieldExpectedResult>();
                    Utilities.TestSuite.FieldExpectedResult expectedField = new Utilities.TestSuite.FieldExpectedResult(txtElementName.Text, expectedResult);
                    expectedResultList.Add(expectedField);
                    Utilities.TestSuite.NewExpectedResult(txtStepName.Text, expectedResultList);
                    Utilities.TestSuite.AddGUITestAttributes((int)numSequence.Value, txtStepName.Text, txtStepName.Text, cmbURL.Text, cmbObjectMapItem.Text, cmbOperationDesc.Text, inputString);
                    bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
                    dgGUISteps.DataSource = bsGUISteps;
                    dgGUISteps.Refresh();
                }
                else
                {
                    string inputString = "";
                    if (txtInputValue.Enabled) inputString = txtInputValue.Text;
                    else inputString = cmbInputData.Text;

                    string expectedResult = cmbAttribute.Text + "," + cmbVerificationType.Text + "," + txtExpectedValue.Text;
                    List<Utilities.TestSuite.FieldExpectedResult> expectedResultList = new List<Utilities.TestSuite.FieldExpectedResult>();
                    Utilities.TestSuite.FieldExpectedResult expectedField = new Utilities.TestSuite.FieldExpectedResult(txtElementName.Text, expectedResult);
                    expectedResultList.Add(expectedField);
                    Utilities.TestSuite.NewExpectedResult(txtStepName.Text, expectedResultList);
                    Utilities.TestSuite.AddGUITestAttributes((int)numSequence.Value, txtStepName.Text, this.oldTestName, cmbURL.Text, cmbObjectMapItem.Text, cmbOperationDesc.Text, inputString);
                    bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
                    dgGUISteps.DataSource = bsGUISteps;
                    dgGUISteps.Refresh();
                }
                btnAddGUITestStep.Text = "Add";

                cmbURL.Text = "";
                cmbObjectMapItem.Text = "";
                cmbOperationDesc.Text = "";
                cmbInputData.Text = "";
                txtInputValue.Text = "";
                txtElementName.Text = "";
                cmbAttribute.Text = "";
                cmbVerificationType.Text = "";
                txtExpectedValue.Text = "";
            }
            else
            {
                MessageBox.Show("Please select valid XPath for the Element.", "XPath Missing");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAddGUITestStep.Text = "Add";

            txtStepName.Text = "";
            cmbURL.Text = "";
            cmbObjectMapItem.Text = "";
            cmbOperationDesc.Text = "";
            cmbInputData.Text = "";
            txtInputValue.Text = "";
            txtElementName.Text = "";
            cmbAttribute.Text = "";
            cmbVerificationType.Text = "";
            txtExpectedValue.Text = "";
        }

        private void dgGUISteps_RightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxObjectMapGridOptions.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void tsMenuDeleteGUITest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int sequence = (int) dgGUISteps.SelectedCells[0].Value;
            string testName = dgGUISteps.SelectedCells[1].Value.ToString(); ;
            Utilities.TestSuite.DeleteTest(testName, sequence);
            bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
            dgGUISteps.DataSource = bsGUISteps;
            dgGUISteps.Refresh();
            Cursor.Current = Cursors.Default;
        }

        private void tsMenuAddGUITest_Click(object sender, EventArgs e)
        {
            btnAddGUITestStep.Text = "Add";
            numSequence.Value = numSequence.Value + 1;
            txtStepName.Text = this.oldTestName;
            cmbURL.Text = "";
            cmbObjectMapItem.Text = "";
            cmbOperationDesc.Text = "";
            cmbInputData.Text = "";
            txtInputValue.Text = "";
            txtElementName.Text = "";
            cmbAttribute.Text = "";
            cmbVerificationType.Text = "";
            txtExpectedValue.Text = "";
        }

        void dgGUISteps_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgGUISteps.BeginEdit(true);
            try
            {
                txtStepName.Text = dgGUISteps.CurrentRow.Cells["colTestName"].Value.ToString();
                numSequence.Value = (int)dgGUISteps.CurrentRow.Cells["colSequence"].Value;
                txtElementName.Text = dgGUISteps.CurrentRow.Cells["colTestName"].Value.ToString();
                cmbURL.Text = dgGUISteps.CurrentRow.Cells["colURL"].Value.ToString();
                cmbObjectMapItem.Text = dgGUISteps.CurrentRow.Cells["colXPath"].Value.ToString();
                cmbOperationDesc.Text = dgGUISteps.CurrentRow.Cells["colOperation"].Value.ToString();
                cmbInputData.Text = dgGUISteps.CurrentRow.Cells["colInput"].Value.ToString();
                txtInputValue.Text = dgGUISteps.CurrentRow.Cells["colInput"].Value.ToString();
                var tempExpectedResults = Utilities.TestSuite.GetFieldTestExpectedResults(txtStepName.Text, cmbObjectMapItem.Text).Split(",");
                cmbAttribute.Text = tempExpectedResults[0];
                cmbVerificationType.Text = tempExpectedResults[1];
                txtExpectedValue.Text = tempExpectedResults[2];
                this.selectedRow = dgGUISteps.CurrentCell.RowIndex;
                btnAddGUITestStep.Text = "Update";
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some ObjectMap DataGrid Issue:" + f.Message, TraceEventType.Error);
            }
        }
    }
}
