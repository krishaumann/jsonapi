using CefSharp.WinForms;
using CefSharp.WinForms.Host;
using HtmlAgilityPack;
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


namespace JSONAPI
{
    public partial class frmObjectMap : Form
    {
        public string oldObjectName = "";
        public string oldXPath = "";
        public string oldURL = "";
        public bool oldIsValidated = false;
        public int selectedRow = 0;
        private ChromiumWebBrowser browser;
        public frmObjectMap()
        {
            InitializeComponent();
            AddEventHandlers();
            btnAddObject.Enabled = false;
            bsObjectMap.DataSource = Utilities.GUIObjects.GetGUIObjectDocuments();
            dgObjectMap.DataSource = bsObjectMap;
            dgObjectMap.Refresh();
        }

        private void AddEventHandlers()
        {
            this.btnValidateXPath.Click += new EventHandler(this.btnValidateXPath_Click);
            this.dgObjectMap.CellDoubleClick += new DataGridViewCellEventHandler(this.dgObjectMap_CellContentDoubleClick);
            this.tsMenuDeleteObjectMap.Click += new EventHandler(this.tsMenuDeleteVariable_Click);
        }

        private void dgObjectMap_RightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxObjectMapGridOptions.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbObjectElement.SelectedTab == tbBrowser)
            {

                browser = new ChromiumWebBrowser();
                {
                    Dock = DockStyle.Fill;
                    Bounds = tbBrowser.Bounds;
                };

                tbBrowser.Controls.Add(browser);
                browser.Load("www.google.com");
            }
        }

        

        private void dgObjectMap_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.oldObjectName = (string)dgObjectMap[1, e.RowIndex].Value;
            this.oldXPath = (string)dgObjectMap[2, e.RowIndex].Value;
            this.oldURL = (string)dgObjectMap[3, e.RowIndex].Value;
            this.oldIsValidated = (bool)dgObjectMap[4, e.RowIndex].Value;

            btnAddObject.Text = "Update";
            txtName.Text = (string)dgObjectMap[1, e.RowIndex].Value;
            txtXPath.Text = (string)dgObjectMap[2, e.RowIndex].Value;
            txtUrl.Text = (string)dgObjectMap[3, e.RowIndex].Value;
            chkValidated.Checked = (bool)dgObjectMap[4, e.RowIndex].Value;
        }

        private void tsMenuDeleteVariable_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string deleteItem = dgObjectMap.SelectedCells[0].Value.ToString();

            Utilities.GUIObjects.DeleteGuiObjectDocument(deleteItem);
            bsObjectMap.DataSource = Utilities.Variables.GetVariableDocuments();
            dgObjectMap.DataSource = bsObjectMap;
            dgObjectMap.Refresh();
            Cursor.Current = Cursors.Default;
        }
        private void btnValidateXPath_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text;
            string xpath = txtXPath.Text;
            bool validXPath = true;
            if (url.Length == 0)
            {
                MessageBox.Show("Please specify a valid URL");
            }
            else
            {
                bool passed = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!passed)
                {
                    MessageBox.Show("Please an existing URL");
                }
                else
                {
                    try
                    {
                        System.Xml.XPath.XPathExpression expr = System.Xml.XPath.XPathExpression.Compile(xpath);
                    }
                    catch (System.Xml.XPath.XPathException)
                    {
                        validXPath = false;
                    }
                    if (!validXPath)
                    {
                        MessageBox.Show("Please specify valid XPath syntax.");
                    }
                    else
                    {
                        HtmlWeb web = new HtmlWeb();
                        HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                        var nodes = doc.DocumentNode.SelectNodes(xpath);
                        if (nodes != null)
                        {
                            MessageBox.Show("XPath is valid and exist and can be saved on the ObjectMap");
                            chkValidated.Checked = true;
                            btnAddObject.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("XPath is valid but does noy exist and cannot be saved on the ObjectMap");
                            chkValidated.Checked = false;
                            btnAddObject.Enabled = true;
                        }
                    }

                }
            }
        }

        private void dgObjectMap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update the balance column whenever the value of any cell changes.
            if (e.RowIndex > -1)
            {
                Utilities.GUIObjects.UpdateGUIObjectDocument(this.oldObjectName, txtName.Text, this.oldXPath, txtXPath.Text, this.oldURL, this.txtUrl.Text, this.oldIsValidated, this.chkValidated.Checked);
                bsObjectMap.DataSource = Utilities.GUIObjects.GetGUIObjectDocuments();
                dgObjectMap.DataSource = bsObjectMap;
                dgObjectMap.Refresh();
            }
        }

        void dgObjectMap_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgObjectMap.BeginEdit(true);
            try
            {
                txtName.Text = dgObjectMap.CurrentRow.Cells["colObjectName"].Value.ToString();
                txtXPath.Text = dgObjectMap.CurrentRow.Cells["colXPath"].Value.ToString();
                txtUrl.Text = dgObjectMap.CurrentRow.Cells["colUrl"].Value.ToString();
                chkValidated.Checked = (bool)dgObjectMap.CurrentRow.Cells["colIsValidated"].Value;
                this.selectedRow = dgObjectMap.CurrentCell.RowIndex;
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some ObjectMap DataGrid Issue:" + f.Message, TraceEventType.Error);
            }
        }

        private void btnCancelObject_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtUrl.Text = "";
            txtXPath.Text = "";
            btnAddObject.Text = "Add";
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            if (btnAddObject.Text.ToLower() == "add")
            {
                Utilities.GUIObjects.NewGuiObjectDocument(this.txtName.Text, this.txtXPath.Text, this.txtUrl.Text, this.chkValidated.Checked);
            }
            else
            {
                if (btnAddObject.Text.ToLower() == "select")
                {
                    foreach (DataGridViewRow row in dgObjectMap.Rows)
                    {
                        if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelected"]).FormattedValue)
                        {
                            this.Close();
                        }
                      }
                }
                Utilities.GUIObjects.UpdateGUIObjectDocument(this.oldObjectName, this.txtName.Text, this.oldXPath, this.txtXPath.Text, this.oldURL, this.txtUrl.Text, this.oldIsValidated, this.chkValidated.Checked);
                btnAddObject.Text = "Add";
            }
            bsObjectMap.DataSource = Utilities.GUIObjects.GetGUIObjectDocuments();
            dgObjectMap.DataSource = bsObjectMap;
            dgObjectMap.Refresh();
            txtName.Text = "";
            txtUrl.Text = "";
            txtXPath.Text = "";
            chkValidated.Checked = false;
        }

        private void dgObjectMap_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                btnAddObject.Text = "Select";
            }
        }

    }
}
