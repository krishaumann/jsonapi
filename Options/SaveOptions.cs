using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace JSONAPI.Options
{
    public partial class frmSaveOptions : Form
    {
        public Dictionary<string, string> inputValueDict;
        public frmSaveOptions(Dictionary<string, string> inputDict)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            this.inputValueDict = inputDict;
        }

        private void btnSaveOptions_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cmbSaveOptions.Text.Length > 0)
            {
                if (cmbSaveOptions.Text.ToLower() == "both")
                {
                    foreach (var key in this.inputValueDict.Keys)
                    {
                        string path = "";
                        try
                        {
                            path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();
                            string[] tempPath = path.Split(".".ToCharArray());
                            path = tempPath[0] + ".h";
                            int lineCount = 0;
                            //Only add these attributes from Line 4
                            using (var reader = File.OpenText(path))
                            {
                                while (reader.ReadLine() != null)
                                {
                                    lineCount++;
                                }
                            }
                            for (int i = lineCount; i < 4; i++)
                            {
                                File.AppendAllText(path, Environment.NewLine, Encoding.UTF8);
                            }
                            File.AppendAllText(path, key.ToString() + "," + this.inputValueDict[key].ToString() + Environment.NewLine, Encoding.UTF8);
                            
                            Utilities.Variables.NewVariableDocument(key.ToString(), key.ToString(), this.inputValueDict[key].ToString(), "Both", "All", 0);
                        }
                        catch (Exception fe)
                        {
                            Utilities.Utilities.WriteLogItem("Issue with file. Nothing will be displayed:" + fe.Message, System.Diagnostics.TraceEventType.Warning);
                        }
                    }
                }
                if (cmbSaveOptions.Text.ToLower() == "once-off")
                {
                    foreach (var key in this.inputValueDict.Keys)
                    {
                        string path = "";
                        try
                        {
                            path = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User).ToString();
                            string[] tempPath = path.Split(".".ToCharArray());
                            path = tempPath[0] + ".h";
                            File.AppendAllText(Environment.NewLine + path, key.ToString() + "," + this.inputValueDict[key].ToString() + Environment.NewLine, Encoding.UTF8);
                        }
                        catch (Exception fe)
                        {
                            Utilities.Utilities.WriteLogItem("Issue with file. Nothing will be displayed:" + fe.Message, System.Diagnostics.TraceEventType.Warning);
                        }
                    }
                }
                if (cmbSaveOptions.Text.ToLower() == "variable")
                {
                    foreach (var key in this.inputValueDict.Keys)
                    {
                        Utilities.Variables.NewVariableDocument(key.ToString(), key.ToString(), this.inputValueDict[key].ToString(), "Both", "All", 0);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select and Save Option.");
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Save Successful.");
        }
    }
}