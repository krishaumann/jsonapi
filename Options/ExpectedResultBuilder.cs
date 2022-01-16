using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Options
{
    public partial class frmBuildExpectedResult : Form
    {
        public string testName = "";
        public frmBuildExpectedResult(Utilities.ExecutionResult.GroupedResult groupRow)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            txtFieldName.Text = groupRow.FieldName;
            // Get majority value
            txtExpectedValue.Text = groupRow.FieldValue1;
            txtFieldName.Enabled = true;
            btnVariableSave.Text = "Save";
        }

        public frmBuildExpectedResult()
        {
            InitializeComponent();
            txtFieldName.Enabled = false;
            btnVariableSave.Text = "Copy";
        }

        private void btnAddExpectedValue_Click(object sender, EventArgs e)
        {
            if (lstExpectedType.SelectedIndex == 0)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[VARIABLE(Variable_Name)]";
            }
            if (lstExpectedType.SelectedIndex == 1)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[ISNUMBER]";
            }
            if (lstExpectedType.SelectedIndex == 2)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[ISDATE]";
            }
            if (lstExpectedType.SelectedIndex == 3)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[ISTEXT)]";
            }
            if (lstExpectedType.SelectedIndex == 4)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[ISGUID]";
            }
            if (lstExpectedType.SelectedIndex == 5)
            {
                txtExpectedValue.Text = txtExpectedValue.Text + "[RANGE(Range_Name)]";
            }
            if (lstExpectedType.SelectedIndex == 6)
            {
                txtExpectedValue.Text = "";
            }
        }

        private void btnValidateExpectedResult_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Result= " + Utilities.Utilities.ValidateDetail(txtExpectedValue.Text,"JSONAPI"));
        }
    

        private void btnVariableSave_Click(object sender, EventArgs e) 
        {
            if (btnAddExpectedValue.Text == "Save")
            {
                Utilities.TestSuite.UpdateExpectedResult(this.testName, txtFieldName.Text, txtExpectedValue.Text);
            }
            else
            {
                Clipboard.SetText(txtExpectedValue.Text);
            }
            this.Hide();
            this.Dispose();
            frmExpectedResults expectedResultForm = new frmExpectedResults();
            expectedResultForm.Activate();
        }
    }
}
