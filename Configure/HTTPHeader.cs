﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Configure
{
    public partial class frmHttpHeader : Form
    {
        public Boolean dirtySaveFlag = false;
        public int selectedRow = 0;
        public frmHttpHeader()
        {
            InitializeComponent();
            AddEventHandlers();
            readHttpHeaderFile();
        }

        private void InitFormComponents()
        {
            this.txtCustomAttr.Visible = false;
            this.cmbHttpAttrs.Visible = false;
            this.btnAttributeAdd.Text = "Add";
            Cursor.Current = Cursors.Default;
            this.Icon = Properties.Resources.jsonapi;
        }

        private void AddEventHandlers()
        {
            this.btnAttributeAdd.Click += new EventHandler(this.addAttribute_Click);
            this.btnSaveAttributes.Click += new EventHandler(this.saveAttribute_Click);
            this.dgHeaderElements.CellClick += new DataGridViewCellEventHandler(this.dgHeaderElements_Click);
            this.cmbHttpAttrs.SelectedValueChanged += new EventHandler(this.cmbHttpAttrs_SelectedValueChanged);
            this.dgHeaderElements.CellDoubleClick += new DataGridViewCellEventHandler(this.dgHeaderElements_CellContentDoubleClick);
            this.ctxGridOptions.ItemClicked += new ToolStripItemClickedEventHandler(this.menuItem_Click);
        }
        
        private void cmbHttpAttrs_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (cmbHttpAttrs.SelectedIndex != -1)
            {
                string selectedAttribute = cmbHttpAttrs.SelectedItem.ToString();
                if (selectedAttribute.ToLower() == "other")
                {
                    txtCustomAttr.Visible = true;
                    cmbHttpAttrs.Visible = false;
                }
            }
        }
        
        private void dgHeaderElements_Click(Object sender, DataGridViewCellEventArgs e)
        { 
                ctxGridOptions.Show();//places the menu at the pointer position
        }

        private void addAttribute_Click(object sender, System.EventArgs e)
        {
            string[] row0 = new string[2]; ;
            if (txtCustomAttr.Visible)
            {
                row0[0] = txtCustomAttr.Text.ToString();
                row0[1] = txtAttrValue.Text.ToString();
            }
            else
            {
                row0[0] = cmbHttpAttrs.SelectedItem.ToString();
                row0[1] = txtAttrValue.Text.ToString();
            }
            if (btnAttributeAdd.Text == "Add")
            {
                dgHeaderElements.Rows.Add(row0);               
            }
            else
            {
                dgHeaderElements.Rows[this.selectedRow].Cells["colAttrValue"].Value = txtAttrValue.Text;
                if (cmbHttpAttrs.Visible)
                {
                    dgHeaderElements.Rows[this.selectedRow].Cells["colAttrType"].Value = cmbHttpAttrs.Text;
                }
                else
                {
                    dgHeaderElements.Rows[this.selectedRow].Cells["colAttrType"].Value = txtCustomAttr.Text;
                }   
            }
            dgHeaderElements.Refresh();
            txtAttrValue.Text = "";
            txtCustomAttr.Text = "";
            cmbHttpAttrs.Text = "";
            this.dirtySaveFlag = true;
        }
        
        private void saveAttribute_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            UpdateHeader();
            this.dirtySaveFlag = false;
            Cursor.Current = Cursors.Default;
        }

        private void frmHttpHeader_Close(object sender, FormClosingEventArgs e)
        {
            if (this.dirtySaveFlag)
            {
                Cursor.Current = Cursors.WaitCursor;
                UpdateHeader();
                this.dirtySaveFlag = false;
                Cursor.Current = Cursors.Default;
            }
        }

        void menuItem_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            string menuItem = e.ClickedItem.Name;

            if (menuItem == "ctxRemoveRow")
            {
                if (this.dgHeaderElements.SelectedRows.Count > 0)
                {
                    dgHeaderElements.Rows.RemoveAt(this.dgHeaderElements.SelectedRows[0].Index);
                }
            }
            this.dirtySaveFlag = true;
        }

        void readHttpHeaderFile()
        {
            string path = "";
            try
            {
                path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();

            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("Issue with file. Nothing will be displayed:" + e.Message, System.Diagnostics.TraceEventType.Error);
            }
            if (path.Length == 0)
            {
                path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();
            }
            string[] tempPath = path.Split(".".ToCharArray());
            path = tempPath[0] + ".h";
            int counter = 1;
            //MessageBox.Show("Header file; " + path);
            if (File.Exists(path))
            {
                string[] httpAttrs = new string[2];
                foreach (string line in System.IO.File.ReadLines(path))
                {
                    if (counter == 1)
                    {
                        txtURL.Text = line;
                    }
                    if (counter == 2)
                    {
                        cmbHttpType.Text = line;
                        cmbHttpType.SelectedItem = line;
                    }
                    if (counter == 3)
                    {
                        cmbAuthorization.Text = line;
                        cmbAuthorization.SelectedItem = line;
                    }
                    if (counter > 3)
                    {
                        httpAttrs = line.Split(',');
                        dgHeaderElements.Rows.Add(httpAttrs);
                    }
                    counter++;
                }
            }
        }

        void UpdateHeader()
        {
            string path = "";
            string testName = "";
            try
            {
                path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();
                var testNameArray = path.Split("\\".ToCharArray());
                testName = testNameArray[testNameArray.Length - 1].Split(".".ToCharArray())[0];
            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("Header file will be created:" + e.Message, System.Diagnostics.TraceEventType.Error);
            }

            if (path.Length == 0)
            {
                path = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User) + "new_json.json";
            }
            string[] tempPath = path.Split(".".ToCharArray());
            StringBuilder sb = new StringBuilder();
            path = tempPath[0] + ".h";
            /* Header file layout:
             * Line 1 --> url
             * Line 2 --> Http Method (Post, Get, etc)
             * Line 3 --> Authorization (Bearer, Token, etc)
             * Line 4 to end --> Other Http header items*/

            if (txtURL.Text.Length > 0)
            {
                File.WriteAllText(path, txtURL.Text + Environment.NewLine, Encoding.UTF8);
                sb.Append(txtURL.Text + Environment.NewLine);
            }
            else
            {
                File.WriteAllText(path, "http://localhost" + Environment.NewLine, Encoding.UTF8);
                sb.Append("http://localhost" + Environment.NewLine);
            }
            if (cmbHttpType.Text.Length > 0)
            {
                File.AppendAllText(path, cmbHttpType.Text + Environment.NewLine, Encoding.UTF8);
                sb.Append(cmbHttpType.Text + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(path, "GET" + Environment.NewLine, Encoding.UTF8);
                sb.Append("GET" + Environment.NewLine);
            }
            try
            {
                if (cmbAuthorization.Text.Length > 0)
                {
                    File.AppendAllText(path, cmbAuthorization.SelectedItem.ToString() + Environment.NewLine, Encoding.UTF8);
                    sb.Append(cmbAuthorization.SelectedItem.ToString() + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(path, "None" + Environment.NewLine, Encoding.UTF8);
                    sb.Append("None" + Environment.NewLine);
                }
            }
            catch (Exception ca)
            {
                Utilities.Utilities.WriteLogItem("No selected authorization to update:" + ca.ToString(), System.Diagnostics.TraceEventType.Error);
            }
            int lineCount = 0;
            //Only add these attributes from Line 4
            using (var reader = File.OpenText(path))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            for (int i = lineCount; i < 3; i++)
            {
                File.AppendAllText(path, Environment.NewLine, Encoding.UTF8);
                sb.Append(Environment.NewLine);
            }

            foreach (DataGridViewRow row in dgHeaderElements.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString().Length > 0)
                    {
                        File.AppendAllText(path, row.Cells[0].Value + "," + row.Cells[1].Value + Environment.NewLine, Encoding.UTF8);
                        sb.Append(row.Cells[0].Value + "," + row.Cells[1].Value + Environment.NewLine);
                    }
                }
            }

            Utilities.TestSuite.NewTestWithDetail(testName, sb.ToString(), "");
            MessageBox.Show("Header File Updated successfully");
        }

        void dgHeaderElements_CellContentDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgHeaderElements.BeginEdit(true);
            try
            {
                txtAttrValue.Text = dgHeaderElements.CurrentRow.Cells["colAttrValue"].Value.ToString();
                string attrType = dgHeaderElements.CurrentRow.Cells["colAttrType"].Value.ToString();
                if (attrType.ToLower() == "other")
                {
                    txtCustomAttr.Visible = true;
                    cmbHttpAttrs.Visible = false;
                    txtCustomAttr.Text = "Other";
                }
                else
                {
                    Boolean foundFlag = false;
                    for (int i = 0; i < cmbHttpAttrs.Items.Count; i++)
                    {
                        if (attrType.ToLower() == cmbHttpAttrs.Items[i].ToString().ToLower())
                        {
                            foundFlag = true;
                        }
                        else
                        {
                            foundFlag = false;
                        }
                    }
                    if (foundFlag)
                    {
                        txtCustomAttr.Visible = false;
                        cmbHttpAttrs.Visible = true;
                        cmbHttpAttrs.Text = dgHeaderElements.CurrentRow.Cells["colAttrType"].Value.ToString();
                    }
                    else
                    {
                        txtCustomAttr.Visible = true;
                        cmbHttpAttrs.Visible = false;
                        txtCustomAttr.Text = dgHeaderElements.CurrentRow.Cells["colAttrType"].Value.ToString();
                    }
                }
                btnAttributeAdd.Text = "Update";
                this.selectedRow = dgHeaderElements.CurrentCell.RowIndex;
            }
            catch (Exception f)
            {
                Utilities.Utilities.WriteLogItem("Some DataGrid Issue:" + f.Message, System.Diagnostics.TraceEventType.Error);
            }
        }
    }
}