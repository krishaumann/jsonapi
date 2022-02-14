using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSONAPI.Utilities;
using Octokit;
using System.Xml;
using Microsoft.WindowsAPICodePack.Dialogs;
using CheckBox = System.Windows.Forms.CheckBox;

namespace JSONAPI
{
    public partial class JSONAPIForm : Form
    {
        private TreeNode mySelectedNode;
        private string previous_fileName = "";
        public int selectedRow = 0;
        CheckBox checkboxHeader = null;
        public Boolean isHeaderCheckBoxClicked = false;
        public Boolean inputDirtyFlag = false;
        public JSONAPIForm()
        {
            InitializeComponent();
            ImageList myImageList = new ImageList();
            this.Icon = Properties.Resources.jsonapi;
            myImageList.Images.Add("folder", Properties.Resources.FolderIcon);
            myImageList.Images.Add("json", Properties.Resources.JSONIcon);
            myImageList.Images.Add("xml", Properties.Resources.XMLIcon);
            myImageList.Images.Add("js", Properties.Resources.JSIcon);
            myImageList.Images.Add("guitest", Properties.Resources.guitest);
            tvDirectory.ImageList = myImageList;
            try
            {
                dgHttpHeader.AutoGenerateColumns = false;
            }
            catch (Exception au)
            {
                Utilities.Utilities.WriteLogItem("dgHttpHeader generate issue:" + au.ToString(), System.Diagnostics.TraceEventType.Error);
            }

            Cursor.Current = Cursors.Default;
            string tempWorkFolder = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);
            if (tempWorkFolder == null)
            {
                System.Environment.SetEnvironmentVariable("workfolder", "C:\\JSONAPI\\", EnvironmentVariableTarget.User);
            }
            if (System.Environment.GetEnvironmentVariable("useBearerToken", EnvironmentVariableTarget.User) == null)
            {
                System.Environment.SetEnvironmentVariable("bearerToken", "none", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBearerToken", "no", EnvironmentVariableTarget.User);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("bearerToken", "none", EnvironmentVariableTarget.User);
            }
            if (System.Environment.GetEnvironmentVariable("useBasicToken", EnvironmentVariableTarget.User) == null)
            {
                System.Environment.SetEnvironmentVariable("basicToken", "none", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useBasicToken", "no", EnvironmentVariableTarget.User);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("basicToken", "none", EnvironmentVariableTarget.User);
            }
            if (System.Environment.GetEnvironmentVariable("useAPIKey", EnvironmentVariableTarget.User) == null)
            {
                System.Environment.SetEnvironmentVariable("apiKey", "none", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useAPIKey", "no", EnvironmentVariableTarget.User);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("apiKey", "none", EnvironmentVariableTarget.User);
            }
            if (System.Environment.GetEnvironmentVariable("useCustomKey", EnvironmentVariableTarget.User) == null)
            {
                System.Environment.SetEnvironmentVariable("customKey", "none", EnvironmentVariableTarget.User);
                System.Environment.SetEnvironmentVariable("useCustomKey", "no", EnvironmentVariableTarget.User);
            }
            else
            {
                System.Environment.SetEnvironmentVariable("customKey", "none", EnvironmentVariableTarget.User);
            }
            PopulateTreeView();
            AddEventHandlers();
        }


        private void AddEventHandlers()
        {
            this.tvDirectory.AfterSelect += new TreeViewEventHandler(this.tvDirectory_AfterSelect);
            this.btnSend.Click += new EventHandler(this.sendButton_Click);
            this.ctxChangeFolder.Click += new EventHandler(this.changeFolderMenuItem_Click);
            this.tvDirectory.AfterLabelEdit += new NodeLabelEditEventHandler(this.tvDirectory_AfterLabelEdit);
            this.tvDirectory.BeforeLabelEdit += new NodeLabelEditEventHandler(this.tvDirectory_BeforeLabelEdit);
            this.ctxMenuAddJSONFile.Click += new EventHandler(this.addJSONFileMenuItem_Click);
            this.ctxMenuAddXMLFile.Click += new EventHandler(this.addXMLFileMenuItem_Click);
            this.ctxMenuAddFolder.Click += new EventHandler(this.addFolderMenuItem_Click);
            this.ctxMenuDelete.Click += new EventHandler(this.deleteMenuItem_Click);
            this.ctxRefresh.Click += new EventHandler(this.refreshMenuItem_Click);
            this.btnOptions.Click += new EventHandler(this.options_Click);
            this.btnHttpHeader.Click += new EventHandler(this.httpHeader_Click);
            this.dgHttpHeader.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgHttpHeader_CellFormat);
            this.dgHttpHeader.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgHttpHeader_CellPainting);
            this.dgHttpHeader.CellValueChanged += new DataGridViewCellEventHandler(this.dgHttpHeader_OnCellValueChanged);
            this.btnSaveDetail.Click += new EventHandler(this.btnSaveInput_Click);
            this.btnInputFields.Click += new EventHandler(this.btnInputDetails_Click);
            this.exportToolStripMenuItem.Click += new EventHandler(this.ExportResultsMenuItem_Click);
        }

        /* ===============================================
         * TreeView
         * ===============================================*/
        public void PopulateTreeView()
        {
            String tempWorkFolder = System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);

            DirectoryInfo info;
            if (tempWorkFolder != null)
            {
                info = new DirectoryInfo(@tempWorkFolder);
            }
            else
            {
                info = new DirectoryInfo(@"C:\\JSONAPI\\");
            }
            if (info.Exists)
            {
                //rootNode = new TreeNode(info.Name);
                //rootNode.Tag = info;
                //TreeNode tds = tvDirectory.Nodes.Add(rootNode);
                //GetDirectories(info.GetDirectories(), rootNode);
                LoadDirectory(tempWorkFolder);
            }
            btnHttpHeader.Enabled = false;
            btnSend.Enabled = false;
            btnSaveDetail.Enabled = false;
            btnInputFields.Enabled = false;
            btnCopySelected.Enabled = false;
            btnAddExpectedResult.Enabled = false;
        }
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value  
            TreeNode tds = tvDirectory.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            tds.ImageKey = "folder";
            tds.SelectedImageKey = "folder";
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }

        public void LoadFiles(string dir, TreeNode td)
        {
            var Files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".xml") || s.EndsWith(".json") || s.EndsWith(".js"));

            // Loop through them to see files  
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
                if (fi.FullName.Contains(".json") || fi.FullName.Contains(".xml") || fi.FullName.Contains(".js"))
                {
                    if (fi.FullName.Contains(".json"))
                    {
                        tds.ImageKey = "json";
                        tds.SelectedImageKey = "json";
                    }
                    if (fi.FullName.Contains(".xml"))
                    {
                        tds.ImageKey = "xml";
                        tds.SelectedImageKey = "xml";
                    }
                    if (fi.FullName.Contains(".js") & !fi.FullName.Contains(".json"))
                    {
                        tds.ImageKey = "js";
                        tds.SelectedImageKey = "js";
                    }
                }
                else
                {
                    tds.ImageKey = "folder";
                    tds.SelectedImageKey = "folder";
                }
            }
            List<Utilities.TestSuite.Test> GUITestList = Utilities.TestSuite.GetGUITests(true);
            foreach (Utilities.TestSuite.Test test in GUITestList)
            {
                TreeNode tds = td.Nodes.Add(test.TestName);
                tds.Tag = test.URL;
                tds.StateImageIndex = 1;
                tds.ImageKey = "guitest";
                tds.SelectedImageKey = "guitest";
            }
        }

        public void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories  
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories  
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
            }
        }

        private Dictionary<string, object> deserializeToDictionary(string jo)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                if (d.Value is JObject)
                {
                    values2.Add(d.Key, deserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }

        protected void tvDirectory_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //MessageBox.Show("After Select:" + e.Node.Text);
            string a = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
            bool jsFlag = false;
            if (a.Contains(".json") || a.Contains(".xml") || a.Contains(".js"))
            {
                try
                {
                    if (a.Contains("json"))
                    {
                        System.Environment.SetEnvironmentVariable("fileType", "json", EnvironmentVariableTarget.User);
                    }
                    else
                    {
                        if (a.Contains("js") & !a.Contains("json"))
                        {
                            System.Environment.SetEnvironmentVariable("fileType", "js", EnvironmentVariableTarget.User);
                            jsFlag = true;
                        }
                        else System.Environment.SetEnvironmentVariable("fileType", "xml", EnvironmentVariableTarget.User);
                    }
                    System.Environment.SetEnvironmentVariable("editFile", a, EnvironmentVariableTarget.User);
                    if (this.inputDirtyFlag)
                    {
                        DialogResult saveDetail = MessageBox.Show("Contents of the Inputfile has changed.  Would you like to save it?", "Save Input", MessageBoxButtons.YesNo);
                        if (saveDetail == DialogResult.Yes)
                        {
                            btnSaveInput_Click(sender, e);
                        }
                    }
                    if (!jsFlag)
                    {
                        richtext_PrettifyText(txtHttpInput, a);
                        btnHttpHeader.Enabled = true;
                        btnSend.Enabled = true;
                        btnSaveDetail.Enabled = true;
                        btnInputFields.Enabled = true;
                    }
                    else
                    {
                        JSONAPI.Controls.JavaScriptEditorForm jsEditor = new JSONAPI.Controls.JavaScriptEditorForm(a);
                        jsEditor.TopMost = true;
                        jsEditor.WindowState = FormWindowState.Maximized;
                        jsEditor.Show();
                    }

                }
                catch (Exception f)
                {
                    Utilities.Utilities.WriteLogItem("Error on clicking node to display contents:" + f.Message, System.Diagnostics.TraceEventType.Error);
                }
            }
            else
            {
                txtHttpInput.Text = "";
                btnHttpHeader.Enabled = false;
                btnSend.Enabled = false;
                btnSaveDetail.Enabled = false;
                btnInputFields.Enabled = false;
                if (Utilities.TestSuite.TestExist(tvDirectory.SelectedNode.Text))
                {
                    Configure.frmScriptEditor newScriptForm = new Configure.frmScriptEditor(tvDirectory.SelectedNode.Text);
                    newScriptForm.TopMost = true;
                    newScriptForm.WindowState = FormWindowState.Maximized;
                    newScriptForm.Show();
                }
            }
        }

        private void changeFolderMenuItem_Click(object sender, EventArgs e)
        {
            tvDirectory.Nodes.Clear();
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\JSONAPI";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                LoadDirectory(dialog.FileName);
                System.Environment.SetEnvironmentVariable("workFolder", dialog.FileName, EnvironmentVariableTarget.User);
            }
        }

        private void addJSONFileMenuItem_Click(object sender, EventArgs e)
        {
            var newNode = tvDirectory.SelectedNode.Nodes.Add("new_json.json");
            tvDirectory.SelectedNode = newNode;
            var filePath = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
            var myFile = File.Create(filePath);
            myFile.Close();
            newNode.EnsureVisible();
            tvDirectory.LabelEdit = true;
            if (!newNode.IsEditing)
            {
                newNode.BeginEdit();
            }
        }

        private void addXMLFileMenuItem_Click(object sender, EventArgs e)
        {
            var newNode = tvDirectory.SelectedNode.Nodes.Add("new_xml.xml");
            tvDirectory.SelectedNode = newNode;
            var filePath = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
            try
            {
                var myFile = File.Create(filePath);
                myFile.Close();
                newNode.EnsureVisible();
                tvDirectory.LabelEdit = true;
                if (!newNode.IsEditing)
                {
                    newNode.BeginEdit();
                }
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("File Not created." + fe.Message, System.Diagnostics.TraceEventType.Critical);
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            var deletedNode = tvDirectory.SelectedNode;
            string fullpath = "C:\\" + deletedNode.FullPath.ToString();
            tvDirectory.Nodes.Remove(tvDirectory.SelectedNode);

            //MessageBox.Show(fullpath);
            if (fullpath.Contains(".json") | fullpath.Contains(".xml"))
            {
                System.IO.File.Delete(fullpath);
            }
            else
            {
                System.IO.Directory.Delete(fullpath);
            }
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tvDirectory.Nodes.Clear();
            LoadDirectory(System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User));
            Cursor.Current = Cursors.Default;
        }

        private void addFolderMenuItem_Click(object sender, EventArgs e)
        {
            var newNode = tvDirectory.SelectedNode.Nodes.Add("New Folder");
            tvDirectory.SelectedNode = newNode;
            var pathString = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
            System.IO.Directory.CreateDirectory(pathString);
            newNode.EnsureVisible();
            tvDirectory.LabelEdit = true;
            if (!newNode.IsEditing)
            {
                newNode.BeginEdit();
            }
        }

        void treeView1_RightMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxOptions.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void treeView1_DoubleMouseClick(object sender, EventArgs e)
        {
            mySelectedNode = tvDirectory.SelectedNode;
            if (previous_fileName.Length == 0) previous_fileName = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
            if (mySelectedNode != null && mySelectedNode.Parent != null)
            {
                tvDirectory.SelectedNode = mySelectedNode;
                tvDirectory.LabelEdit = true;
                if (!mySelectedNode.IsEditing)
                {
                    mySelectedNode.BeginEdit();
                }
            }
            else
            {
                if (mySelectedNode.Parent != null)
                {
                    MessageBox.Show("No tree node selected or selected node is a root node.\n" + "Editing of root nodes is not allowed.", "Invalid selection");
                }
            }
        }

        private void tvDirectory_BeforeLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            previous_fileName = "C:\\" + tvDirectory.SelectedNode.FullPath.ToString();
        }

        private void tvDirectory_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //MessageBox.Show(e.Label);
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', ',', '!' }) == -1)
                    {
                        if (!e.Label.Contains(".."))
                        {
                            // Stop editing without canceling the label change.
                            e.Node.EndEdit(false);
                            TreeNode CurrentNode = e.Node;
                            CurrentNode.Text = e.Label;
                            string fullpath = "C:\\" + CurrentNode.FullPath.ToString();
                            var tempArray1 = fullpath.Split(".".ToCharArray());
                            string validateFileBase = tempArray1[0];
                            if (File.Exists(validateFileBase + ".json") | File.Exists(validateFileBase + ".xml"))
                            {
                                MessageBox.Show("An existing file with that name already exists.  Please choose another file name.", "Duplicate File Name");
                                e.CancelEdit = true;
                            }
                            else
                            {
                                //fullpath += System.Environment.GetEnvironmentVariable("workfolder", EnvironmentVariableTarget.User);

                                if ((fullpath.Contains(".json") || fullpath.Contains(".xml")) && !System.IO.File.Exists(fullpath))
                                {
                                    //MessageBox.Show(fullpath);
                                    if (fullpath.Contains(".json"))
                                    {
                                        if (previous_fileName.Length == 0)
                                        {
                                            System.IO.File.WriteAllText(fullpath, "{" + Environment.NewLine + "}");
                                            var tempArray = fullpath.Split(".json".ToCharArray());
                                            System.IO.File.WriteAllText(tempArray[0] + ".h", "{" + Environment.NewLine + "}");
                                        }
                                        else
                                        {
                                            System.IO.File.Move(previous_fileName, fullpath);
                                            var tempArray = fullpath.Split(".json".ToCharArray());
                                            var tempArray2 = previous_fileName.Split(".json".ToCharArray());
                                            System.IO.File.Move(tempArray2[0] + ".h", tempArray[0] + ".h");
                                        }
                                    }
                                    if (fullpath.Contains(".xml"))
                                    {
                                        if (previous_fileName.Length == 0)
                                        {
                                            System.IO.File.WriteAllText(fullpath, "<?xml version=\"1.0\"?>" + Environment.NewLine);
                                            var tempArray = fullpath.Split(".xml".ToCharArray());
                                            System.IO.File.WriteAllText(tempArray[0] + ".h", "{" + Environment.NewLine + "}");
                                        }
                                        else
                                        {
                                            try
                                            {
                                                System.IO.File.Move(previous_fileName, fullpath);
                                                var tempArray = fullpath.Split(".xml".ToCharArray());
                                                var tempArray2 = previous_fileName.Split(".xml".ToCharArray());

                                                System.IO.File.Move(tempArray2[0] + ".h", tempArray[0] + ".h");
                                            }
                                            catch (Exception ef)
                                            {
                                                Utilities.Utilities.WriteLogItem("Issue with rename header file, " + ef.Message, System.Diagnostics.TraceEventType.Error);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    bool exists = System.IO.Directory.Exists(fullpath);
                                    if (!exists)
                                    {
                                        System.IO.Directory.Move(previous_fileName, fullpath);
                                    }
                                }
                            }
                        }
                        else
                        {
                            e.CancelEdit = true;
                            MessageBox.Show("Invalid file extension.\n" + "The invalid are .json or .xml", "Node Label Edit");
                            e.Node.BeginEdit();
                        }
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show("Invalid tree node label.\n" + "The invalid characters are: '@', ',', '!'", "Node Label Edit");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    MessageBox.Show("Invalid tree node label.\nThe label cannot be blank", "Folder detail Edit");
                    e.Node.BeginEdit();
                }
            }
        }


        /* ===============================================
         * Options
         * ===============================================*/

        private void options_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Options.OptionForm optionfrm = new Options.OptionForm();
            //optionfrm.TopMost = true;
            optionfrm.ShowDialog();
            //optionfrm.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
            Cursor.Current = Cursors.Default;
        }

        private void userProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin.frmUserProfile userProfileForm = new Admin.frmUserProfile();
            userProfileForm.TopMost = true;
            userProfileForm.Show();
        }


        public void txtHttpInput_Changed(object sender, System.EventArgs e)
        {
            this.inputDirtyFlag = true;
        }

        void richtext_PrettifyText(RichTextBox obj, string filePath)
        {
            string readText = "";
            string readText2 = "";
            if (File.Exists(filePath))
            {
                readText = File.ReadAllText(filePath);
            }
            else
            {
                readText = filePath;
            }
            try
            {
                string fileType = System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User);
                if (fileType.ToLower() == "json")
                {
                    readText = JValue.Parse(readText).ToString(Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    readText2 = PrintXML(readText.ToString());
                    if (readText2.Contains("Data at the root level is invalid"))
                    {
                        readText = JValue.Parse(readText).ToString(Newtonsoft.Json.Formatting.Indented);
                    }
                    else
                    {
                        readText = readText2;
                    }
                }
            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("JSON Response not in valid format: " + e.ToString(), System.Diagnostics.TraceEventType.Error);
            }
            obj.Text = readText;
        }

        public static string PrintXML(string xml)
        {
            string result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(xml);

                writer.Formatting = System.Xml.Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string formattedXml = sReader.ReadToEnd();

                result = formattedXml;
            }
            catch (Exception xmlE)
            {
                Utilities.Utilities.WriteLogItem("XML Parse Exception:" + xmlE.ToString(), System.Diagnostics.TraceEventType.Error);
                result = xmlE.Message;
            }
            try
            {
                mStream.Close();
                writer.Close();
            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("XML Close Exception:" + e.ToString(), System.Diagnostics.TraceEventType.Error);
            }
            return result;
        }

        /* ===============================================
         * Send HTTP Message
         * ===============================================*/

        private void sendButton_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgHttpHeader.Rows.Clear();
            txtHttpResponseBody.Text = "";
            txtHttpResponseHeader.Text = "";
            httpResponseCode.Text = "";
            httpResponseTime.Text = "";
            btnAddExpectedResult.Enabled = true;
            string tempValue = "";
            System.Environment.SetEnvironmentVariable("execMode", "single", EnvironmentVariableTarget.User);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Dictionary<string, string> responseDict = Utilities.Utilities.SendHttpRequest(txtHttpInput.Text, 1);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            httpResponseTime.Text = elapsedMs.ToString() + " ms";
            if (responseDict.TryGetValue("Body", out tempValue))
            {
                richtext_PrettifyText(txtHttpResponseBody, tempValue);
            }
            else
            {
                txtHttpResponseBody.Text = "No response body received.";
            }
            if (responseDict.TryGetValue("HttpResponse", out tempValue))
            {
                httpResponseCode.Text = tempValue;
            }
            else
            {
                httpResponseCode.Text = "No Http response code received.";
            }
            string[] rowArray = new string[4];
            string responseString = "{";
            foreach (string key in responseDict.Keys)
            {
                if (key.ToLower().Contains("header_"))
                {
                    rowArray[1] = "Header";
                    rowArray[2] = key.Substring(7);
                    rowArray[3] = responseDict[key];
                    responseString += "\"" + key.Substring(7) + "\"" + ":" + "\"" + responseDict[key] + "\"" + ",";
                    dgHttpHeader.Rows.Add(rowArray);
                }
            }
            richtext_PrettifyText(txtHttpResponseHeader, responseString + "}");
            Construct_dgHeaderReturnDict(responseDict);

            AddChkBoxHeader_DataGridView();
            this.btnCopySelected.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private string[] Construct_dgHeaderReturnDict(Dictionary<string, string> responseDict)
        {
            string[] rowArray = new string[4];
            if (System.Environment.GetEnvironmentVariable("fileType", EnvironmentVariableTarget.User) == "json")
            {
                foreach (string key in responseDict.Keys)
                {
                    if (key.ToLower().Contains("body"))
                    {
                        string tempBodyString = responseDict["Body"];
                        try
                        {
                            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(tempBodyString);
                            foreach (KeyValuePair<string, object> d in values)
                            {
                                // if (d.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                                if (d.Value is JObject)
                                {
                                    foreach (JProperty x in (JToken)d.Value)
                                    { // if 'obj' is a JObject
                                        if (x.Value is JObject)
                                        {
                                            foreach (JProperty y in (JToken)x.Value)
                                            {
                                                rowArray[1] = "Body";
                                                rowArray[2] = y.Name;
                                                rowArray[3] = y.Value.ToString();
                                                dgHttpHeader.Rows.Add(rowArray);
                                            }
                                        }
                                        else
                                        {
                                            rowArray[1] = "Body";
                                            rowArray[2] = x.Name;
                                            rowArray[3] = x.Value.ToString();
                                            dgHttpHeader.Rows.Add(rowArray);
                                        }
                                    }
                                }
                                else
                                {
                                    rowArray[1] = "Body";
                                    rowArray[2] = d.Key;
                                    rowArray[3] = d.Value.ToString();
                                }
                            }
                        }
                        catch (Exception pe)
                        {
                            rowArray[1] = "Error";
                            rowArray[2] = "Description";
                            rowArray[3] = pe.ToString();
                        }

                    }
                }
            }
            else
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseDict["Body"]);
                XmlNodeList resources = xml.SelectNodes("*");
                //SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
                List<Variables.VariableList> savedVariableList = Variables.GetVariableDocuments();
                foreach (XmlNode node in resources)
                {
                    if (node.HasChildNodes)
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.HasChildNodes)
                            {
                                foreach (XmlNode grandChildNode in childNode.ChildNodes)
                                {
                                    if (grandChildNode.HasChildNodes)
                                    {
                                        foreach (XmlNode greatGrandChildNode in grandChildNode.ChildNodes)
                                        {
                                            if (greatGrandChildNode.InnerText.Length > 0)
                                            {
                                                string key = grandChildNode.Name.ToString();
                                                string value = "";
                                                if (greatGrandChildNode.Value == null) value = greatGrandChildNode.InnerText;
                                                else value = greatGrandChildNode.Value.ToString();
                                                if (key != null & value != null)
                                                {
                                                    rowArray[1] = "Body";
                                                    rowArray[2] = key;
                                                    rowArray[3] = value;
                                                    dgHttpHeader.Rows.Add(rowArray);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (grandChildNode.InnerText.Length > 0)
                                        {
                                            string key = childNode.Name.ToString();
                                            string value = "";
                                            if (grandChildNode.Value == null) value = grandChildNode.InnerText;
                                            else value = grandChildNode.Value.ToString();
                                            if (key != null & value != null)
                                            {
                                                rowArray[1] = "Body";
                                                rowArray[2] = key;
                                                rowArray[3] = value;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (childNode.InnerText.Length > 0)
                                {
                                    string key = node.Name.ToString();
                                    string value = "";
                                    if (childNode.Value == null) value = childNode.InnerText;
                                    else value = childNode.Value.ToString();
                                    if (key != null & value != null)
                                    {
                                        rowArray[1] = "Body";
                                        rowArray[2] = key;
                                        rowArray[3] = value;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (node.InnerText.Length > 0)
                        {
                            string key = node.Name.ToString();
                            string value = "";
                            if (node.Value == null) value = node.InnerText;
                            else value = node.Value.ToString();
                            if (key != null & value != null)
                            {
                                rowArray[1] = "Body";
                                rowArray[2] = key;
                                rowArray[3] = value;
                            }
                        }
                    }
                }
            }
            return rowArray;
        }

        private void btnSaveInput_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String fileName = System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User);
            System.IO.File.WriteAllText(fileName, txtHttpInput.Text);
            var fileArray = fileName.Split("\\".ToCharArray());
            string testName = fileArray[fileArray.Length - 1].Split(".".ToCharArray())[0];
            Dictionary<string, string> testAttrs = Utilities.TestSuite.GetTestHeaderAttributes(testName);
            if (testAttrs.Count > 0)
            {
                Utilities.TestSuite.NewTestWithDetail(testName, Utilities.TestSuite.GetTestHeaderInput(testName), testAttrs, txtHttpInput.Text);
                MessageBox.Show("Save completed successfully.");
            }
            else MessageBox.Show("Please add header attrbutes before saving.");
            Cursor.Current = Cursors.Default;
            
        }

        private void btnInputDetails_Click(object sender, EventArgs e)
        {
            Configure.frmInputFields inputFieldsForm = new Configure.frmInputFields(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User));
            inputFieldsForm.TopMost = true;
            inputFieldsForm.Show();
        }

        private void btnAddExpectedResult_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string inputString = txtHttpResponseBody.Text;
            string testName = tvDirectory.SelectedNode.Text;
            var testArray = testName.Split(".".ToCharArray());
            Utilities.TestSuite.NewExpectedResult(testArray[0], Utilities.Utilities.BuildExpectedResultList(testArray[0], inputString));
            Cursor.Current = Cursors.Default;
        }

        private void copySelected_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, string> valueDict = new Dictionary<string, string>();
            try
            {
                foreach (DataGridViewRow row in dgHttpHeader.Rows)
                {
                    if ((Boolean)((DataGridViewCheckBoxCell)row.Cells[0]).FormattedValue)
                    {
                        string key = (string)((DataGridViewTextBoxCell)row.Cells[2]).FormattedValue;
                        string keyValue = (string)((DataGridViewTextBoxCell)row.Cells[3]).FormattedValue;
                        valueDict.Add(key, keyValue);
                    }
                }
                Options.frmSaveOptions saveOptions = new Options.frmSaveOptions(valueDict);
                saveOptions.TopMost = true;
                saveOptions.ShowDialog();
                saveOptions.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
                foreach (DataGridViewRow row in dgHttpHeader.Rows)
                {
                    row.Cells[0].Selected = false;
                }
            }
            catch (Exception dlg)
            {
                Utilities.Utilities.WriteLogItem("Issue to build Selected Headers" + dlg.ToString(), System.Diagnostics.TraceEventType.Error);
            }

            Cursor.Current = Cursors.Default;
        }

        private void httpHeader_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Configure.frmHttpHeader httpHeaderfrm = new Configure.frmHttpHeader();
            httpHeaderfrm.TopMost = true;
            httpHeaderfrm.ShowDialog();
            httpHeaderfrm.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
            Cursor.Current = Cursors.Default;
        }


        bool IsTheSameCellValue(int column, int row)
        {
            bool returnValue = true;
            try
            {
                DataGridViewCell cell1 = dgHttpHeader[column, row];
                DataGridViewCell cell2 = dgHttpHeader[column, row - 1];
                if (cell1.Value == null || cell2.Value == null)
                {
                    return false;
                }
                if (cell1.Value.ToString() == cell2.Value.ToString())
                    returnValue = true;
                else returnValue = false;
            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("Error on IsTheSameValue:" + e.ToString(), System.Diagnostics.TraceEventType.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void dgHttpHeader_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 1)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dgHttpHeader.AdvancedCellBorderStyle.Top;
                }
                DataGridViewCell cell = this.dgHttpHeader.Rows[e.RowIndex].Cells[e.ColumnIndex];

                cell.ToolTipText = cell.Value.ToString();
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("Error defining TooltipText" + fe.ToString(), System.Diagnostics.TraceEventType.Warning);
            }
        }

        private void dgHttpHeader_CellFormat(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0 || e.ColumnIndex == 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("Error defining CellFormat" + fe.ToString(), System.Diagnostics.TraceEventType.Warning);
            }
        }

        public void ExportResultsMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = System.Environment.GetEnvironmentVariable("workFolder", EnvironmentVariableTarget.User);
            dialog.Title = "Export Excel Results";
            dialog.DefaultExt = "csv";
            dialog.Filter = "CSV Files (*.csv) | *.csv";
            dialog.FileName = "HeaderResults.csv";
            dialog.ShowDialog();
            string fileName = dialog.FileName;
            Cursor.Current = Cursors.WaitCursor;
            bool result = Utilities.Utilities.ExportGrid(dgHttpHeader, fileName);
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

        private void AddChkBoxHeader_DataGridView()
        {
            Rectangle rect = dgHttpHeader.GetCellDisplayRectangle(0, -1, true);
            rect.Y = rect.Location.Y + 5;
            rect.X = rect.Location.X + 45;
            checkboxHeader = new CheckBox();
            checkboxHeader.Size = new Size(15, 15);
            checkboxHeader.Location = rect.Location;
            dgHttpHeader.Controls.Add(checkboxHeader);
            checkboxHeader.MouseClick += new MouseEventHandler(checkboxHeader_CheckedChanged);
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            HeaderCheckBoxClick((System.Windows.Forms.CheckBox)sender);
        }

        private void HeaderCheckBoxClick(System.Windows.Forms.CheckBox headerCheckbox)
        {
            try
            {
                isHeaderCheckBoxClicked = true;
                foreach (DataGridViewRow r in dgHttpHeader.Rows)
                {
                    ((DataGridViewCheckBoxCell)r.Cells[0]).Value = (bool)headerCheckbox.Checked;
                }
                dgHttpHeader.RefreshEdit();
                isHeaderCheckBoxClicked = false;
            }
            catch (Exception e)
            {
                Utilities.Utilities.WriteLogItem("Error on HeaderCheckboxClick: " + e.ToString(), System.Diagnostics.TraceEventType.Error);
            }
        }

        private void dgHttpHeader_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colSelect.Index)
            {
                btnCopySelected.Enabled = true;
            }
        }

        private void dgHttpHeader_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // If the data source raises an exception when a cell value is
            // commited, display an error message.
            if (e.Exception != null &&
                e.Context == DataGridViewDataErrorContexts.Commit)
            {
                Utilities.Utilities.WriteLogItem("Data Error on the dgHttpHeader component.", System.Diagnostics.TraceEventType.Warning);
            }
        }

        private void ctxMenuAddGUITest_Click(object sender, EventArgs e)
        {
            Configure.frmScriptEditor newScriptForm = new Configure.frmScriptEditor();
            newScriptForm.TopMost = true;
            newScriptForm.WindowState = FormWindowState.Maximized;
            newScriptForm.Show();
        }
    }
}
