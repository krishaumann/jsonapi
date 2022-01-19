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
            cmbInputData.Items.Clear();
            List<Utilities.Ranges.RangeList> existingRanges = Utilities.Ranges.GetRangeLists();
            foreach (Utilities.Ranges.RangeList rangeItem in existingRanges)
            {
                cmbInputData.Items.Add(rangeItem.RangeName);
            }
            cmbInputData.Items.Add("Static");
            bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
            dgGUISteps.DataSource = bsGUISteps;
            dgGUISteps.Refresh();
            btnAddStep.Text = "Add";
        }

        private void AddEventHandlers()
        {
            
        }

        private void cmbOperationDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgGUISteps_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.oldTestName = (string)dgGUISteps[0, e.RowIndex].Value;
            this.oldInput = (string)dgGUISteps[1, e.RowIndex].Value;
            this.oldURL = (string)dgGUISteps[2, e.RowIndex].Value;
            this.oldXPath = (string)dgGUISteps[3, e.RowIndex].Value;
            this.oldOperation = (string)dgGUISteps[4, e.RowIndex].Value;

            btnAddStep.Text = "Update";
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
            if (cmbInputData.SelectedText.ToLower() == "static")
            {
                txtInputValue.Enabled = true;
            }
            else
            {
                txtInputValue.Enabled = false;
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
            if (btnAddStep.Text.ToLower() == "add")
            {
                string inputString = "";
                if (txtInputValue.Enabled) inputString = txtInputValue.Text;
                else inputString = cmbInputData.Text;
                Utilities.TestSuite.NewTestWithDetail(txtStepName.Text, "", inputString);
                Utilities.TestSuite.AddGUITestAttributes((int)numSequence.Value, txtStepName.Text, cmbURL.Text, cmbObjectMapItem.Text, cmbOperationDesc.Text);
                string expectedResult = cmbAttribute.Text + "," + cmbVerificationType.Text + "," + txtExpectedValue.Text;
                List<Utilities.TestSuite.FieldExpectedResult> expectedResultList = new List<Utilities.TestSuite.FieldExpectedResult>();
                Utilities.TestSuite.FieldExpectedResult expectedField = new Utilities.TestSuite.FieldExpectedResult(txtElementName.Text, expectedResult);
                expectedResultList.Add(expectedField);
                Utilities.TestSuite.NewExpectedResult(txtStepName.Text, expectedResultList);
                bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
                dgGUISteps.DataSource = bsGUISteps;
                dgGUISteps.Refresh();
            }
            else
            {

            }
            btnAddStep.Text = "Add";

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAddStep.Text = "Add";

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

        private void dgObjectMap_RightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxObjectMapGridOptions.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void tsMenuDeleteVariable_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string deleteItem = dgGUISteps.SelectedCells[0].Value.ToString();

            Utilities.GUIObjects.DeleteGuiObjectDocument(deleteItem);
            bsGUISteps.DataSource = Utilities.TestSuite.GetGUITests();
            dgGUISteps.DataSource = bsGUISteps;
            dgGUISteps.Refresh();
            Cursor.Current = Cursors.Default;
        }

        void dgGUISteps_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgGUISteps.BeginEdit(true);
            try
            {
                txtStepName.Text = dgGUISteps.CurrentRow.Cells["colTestName"].Value.ToString();
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
                btnAddStep.Text = "Update";
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some ObjectMap DataGrid Issue:" + f.Message, TraceEventType.Error);
            }
        }
    }
}
