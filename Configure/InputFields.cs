using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Configure
{
    public partial class frmInputFields : Form
    {
        public static Boolean isDirtyFlag = false;
        private TreeNode mySelectedNode;
        public frmInputFields(String filePath)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            System.Environment.SetEnvironmentVariable("editFile", filePath, EnvironmentVariableTarget.User);
            /*using (var reader = new StreamReader(filePath))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var root = JToken.Load(jsonReader);
                DisplayTreeView(root, Path.GetFileNameWithoutExtension(filePath));
            }*/
            string jsonString = File.ReadAllText(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User), Encoding.UTF8);
            try
            {
                var userObj = JObject.Parse(jsonString);
                TreeViewUtilities.ObjectToTreeView.SetObjectAsJson(tvInputFile, userObj);
            }
            catch (Exception pe)
            {
                Utilities.Utilities.WriteLogItem("Parsing error on loading InputFields form: " + pe.ToString(), System.Diagnostics.TraceEventType.Error);
            }
        }

        private void AddEventHandlers()
        {
            this.tvInputFile.AfterSelect += new TreeViewEventHandler(this.tvInputFile_AfterSelect);
            this.tvInputFile.AfterLabelEdit += new NodeLabelEditEventHandler(this.tvInputFile_AfterLabelEdit);
            this.tsRefreshFile.Click += new EventHandler(this.refreshMenuItem_Click);
            this.tsAddNewNode.Click += new EventHandler(this.addNewNode_Click);
            this.tsAddNewNodeValue.Click += new EventHandler(this.addNewNodeValue_Click);
            this.tsRenameParent.Click += new EventHandler(this.renameParent_Click);
            this.tsDeleteNode.Click += new EventHandler(this.deleteNode_Click);
            this.btnValidate.Click += new EventHandler(this.btnValidate_Click);
            this.btnAddInputValue.Click += new EventHandler(this.btnAddInputValue_Click);
            this.btnSaveInputField.Click += new EventHandler(this.btnSaveInputFile_Click);
            this.btnUpdateItem.Click += new EventHandler(this.btnUpdateItem_Click);
        }

        private void DisplayTreeView(JToken root, string rootName)
        {
            tvInputFile.BeginUpdate();
            try
            {
                tvInputFile.Nodes.Clear();
                var tNode = tvInputFile.Nodes[tvInputFile.Nodes.Add(new TreeNode(rootName))];
                tNode.Tag = root;

                AddNode(root, tNode);

                tvInputFile.ExpandAll();
                tvInputFile.SelectedNode = tvInputFile.Nodes[0];
            }
            finally
            {
                tvInputFile.EndUpdate();
            }
        }

        void tvInputFile_RightMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ctxFileEdit.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void tvInputFile_DoubleMouseClick(object sender, MouseEventArgs e)
        {
            mySelectedNode = tvInputFile.SelectedNode;
            if (mySelectedNode != null && mySelectedNode.Parent != null)
            {
                if (tvInputFile.SelectedNode.Text.ToLower().IndexOf("object") < 0)
                {
                    tvInputFile.SelectedNode = mySelectedNode;
                    tvInputFile.LabelEdit = true;
                    if (!mySelectedNode.IsEditing)
                    {
                        mySelectedNode.BeginEdit();
                    }
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

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tvInputFile.Nodes.Clear();
            string jsonString = File.ReadAllText(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User), Encoding.UTF8);
            var userObj = JObject.Parse(jsonString);
            TreeViewUtilities.ObjectToTreeView.SetObjectAsJson(tvInputFile, userObj);
            Cursor.Current = Cursors.Default;
        }

        private void addNewNode_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var newNode1 = tvInputFile.SelectedNode.Nodes.Add("Parent {Object}");
            tvInputFile.SelectedNode = newNode1;
            var newNode2 = tvInputFile.SelectedNode.Nodes.Add("item : \"value\"");
            tvInputFile.SelectedNode = newNode2;
            newNode1.EnsureVisible();
            newNode2.EnsureVisible();
            isDirtyFlag = true;
            tvInputFile.LabelEdit = true;
            if (!newNode2.IsEditing)
            {
                newNode2.BeginEdit();
            }
            Cursor.Current = Cursors.Default;
        }

        private void renameParent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtInputFieldName.Enabled = true;
            txtInputFieldName.Focus();

            Cursor.Current = Cursors.Default;
        }

        private void deleteNode_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tvInputFile.SelectedNode = mySelectedNode;
            tvInputFile.Nodes.Remove(mySelectedNode);
            Cursor.Current = Cursors.Default;
        }

        private void addNewNodeValue_Click(object sender, EventArgs e)
        {
            if (tvInputFile.SelectedNode.Parent == null)
            {
                var newNode = tvInputFile.SelectedNode.Nodes.Add("new : new");
                tvInputFile.SelectedNode = newNode;
                newNode.EnsureVisible();
                isDirtyFlag = true;
                tvInputFile.LabelEdit = true;
                if (!newNode.IsEditing)
                {
                    newNode.BeginEdit();
                }
            }
            else
            {
                MessageBox.Show("Cannot add a new node here.  Try adding a Node Object");
            }
        }

        private void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token == null)
                return;
            if (token is JValue)
            {
                var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                childNode.Tag = token;
            }
            else if (token is JObject)
            {
                var obj = (JObject)token;
                foreach (var property in obj.Properties())
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name))];
                    childNode.Tag = property;
                    AddNode(property.Value, childNode);
                }
            }
            else if (token is JArray)
            {
                var array = (JArray)token;
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
            else
            {
                Utilities.Utilities.WriteLogItem(string.Format("{0} not implemented", token.Type), System.Diagnostics.TraceEventType.Warning); // JConstructor, JRaw
            }
        }

        protected void tvInputFile_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //MessageBox.Show("After Select:" + e.Node.Text);
            string a = tvInputFile.SelectedNode.FullPath.ToString();
            mySelectedNode = tvInputFile.SelectedNode;
            if (tvInputFile.SelectedNode.Parent == null)
            {
                txtInputValue.Text = a;
                txtInputValue.Enabled = false;
                txtInputFieldName.Text = a;
                txtInputFieldName.Enabled = false;
                btnValidate.Enabled = false;
                btnSaveInputField.Enabled = false;
                btnUpdateItem.Enabled = false;
                lstInputType.Enabled = false;
            }
            else
            {
                string[] nodeArray = a.Split("\\".ToCharArray());
                //Get last description.  If nodeArray[nodeArray.Length - 1] contains {Object} then do not make it editable
                string tempDesc = nodeArray[nodeArray.Length - 1];
                string tempName = tempDesc;
                if (!tempDesc.Contains("<"))
                { 
                    nodeArray = nodeArray[nodeArray.Length - 1].Split(":".ToCharArray());
                    if (nodeArray.Length > 0)
                    {
                        tempDesc = nodeArray[nodeArray.Length - 1];
                        tempName = nodeArray[0];
                    }
                    txtInputValue.Text = tempDesc.Replace("\"", "").Trim();
                    txtInputFieldName.Text = tempName.Replace("\"", "").Trim();
                    txtInputValue.Enabled = true;
                    txtInputFieldName.Enabled = true;
                    btnValidate.Enabled = true;
                    btnSaveInputField.Enabled = true;
                    btnUpdateItem.Enabled = true;
                    lstInputType.Enabled = true;
                }
                else
                {
                    nodeArray = tempDesc.Split("<".ToCharArray());
                    if (nodeArray.Length > 0)
                    {
                        tempName = nodeArray[0];
                        txtInputValue.Text = tempDesc.Replace("\"", "").Trim();
                        txtInputFieldName.Text = tempName.Replace("\"", "").Trim();
                        txtInputValue.Enabled = false;
                        txtInputFieldName.Enabled = true;
                        btnValidate.Enabled = false;
                        btnSaveInputField.Enabled = true;
                        btnUpdateItem.Enabled = true;
                        lstInputType.Enabled = false;
                    }
                    else
                    {
                        txtInputValue.Enabled = true;
                        txtInputFieldName.Enabled = true;
                        btnValidate.Enabled = true;
                        btnSaveInputField.Enabled = true;
                        btnUpdateItem.Enabled = true;
                        lstInputType.Enabled = true;
                    }
                }
            }
            
        }

        private void tvInputFile_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            //MessageBox.Show(e.Label);
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { ':' }) > 0)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);
                        TreeNode CurrentNode = e.Node;
                        CurrentNode.Text = e.Label;
                        string[] splitArray = e.Label.Split(":".ToCharArray());
                        if (splitArray.Length > 0)
                        {
                            CurrentNode.Tag = splitArray[0].Trim();

                        }
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show("Invalid tree node label.\n" + "Include a : to seperate key from value", "Node Label Edit");
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
                    isDirtyFlag = true;
                }
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string beginStr = txtInputValue.Text;
            string endStr = Utilities.Utilities.GenerateDetail(beginStr);
            txtShowSample.Text = endStr;
        }

        private void btnAddInputValue_Click(object sender, EventArgs e)
        {
            if (lstInputType.SelectedIndex == 0)
            {
                txtInputValue.Text = txtInputValue.Text + "[VARIABLE(Variable_Name)]";
            }
            if (lstInputType.SelectedIndex == 1)
            {
                txtInputValue.Text = txtInputValue.Text + "[NUMBER(Number_of_Digits)]";
            }
            if (lstInputType.SelectedIndex == 2)
            {
                txtInputValue.Text = txtInputValue.Text + "[TEXT(Number_of_Letters)]";
            }
            if (lstInputType.SelectedIndex == 3)
            {
                txtInputValue.Text = txtInputValue.Text + "[YESTERDAY(Date_Format)]";
            }
            if (lstInputType.SelectedIndex == 4)
            {
                txtInputValue.Text = txtInputValue.Text + "[TODAY(Date_Format)]";
            }
            if (lstInputType.SelectedIndex == 5)
            {
                txtInputValue.Text = txtInputValue.Text + "[TOMORROW(Date_Format)]";
            }
            if (lstInputType.SelectedIndex == 6)
            {
                txtInputValue.Text = txtInputValue.Text + "[GUID]";
            }
            if (lstInputType.SelectedIndex == 7)
            {
                txtInputValue.Text = txtInputValue.Text + "[RANGE(Range_Name)]";
            }
            lstInputType.ClearSelected();
        }

        static public string ToJson(TreeView treeView)
        {
            Chilkat.JsonObject tvJson = new Chilkat.JsonObject();
            Chilkat.JsonArray tvNodes = tvJson.AppendArray("treeViewNodes");

            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                serializeTree(tvNodes, n);
            }

            tvJson.EmitCompact = false;
            return tvJson.Emit();
        }

        static private void serializeTree(Chilkat.JsonArray tvNodes, TreeNode treeNode)
        {
            tvNodes.AddObjectAt(-1);

            Chilkat.JsonObject json = tvNodes.ObjectAt(tvNodes.Size - 1);
            json.UpdateString("name", treeNode.Name);

            TreeNode parent = treeNode.Parent;
            if (parent != null)
            {
                json.UpdateString("parentName", treeNode.Parent.Name);
            }
            else
            {
                json.UpdateNull("parentName");
            }

            json.UpdateString("tag", (string)treeNode.Tag);
            json.UpdateString("text", treeNode.Text);
            json.UpdateString("toolTipText", treeNode.ToolTipText);
            json.UpdateBool("checked", treeNode.Checked);

            foreach (TreeNode tn in treeNode.Nodes)
            {
                serializeTree(tvNodes, tn);
            }

        }

        private void btnSaveInputFile_Click(object sender, EventArgs e)
        {
            // Convert treeview1 to JSON.
            Chilkat.JsonObject json = new Chilkat.JsonObject();

            TreeNodeCollection nodes = tvInputFile.Nodes;
            foreach (TreeNode n in nodes)
            {
                recurseTree(n, json);
            }
            json.EmitCompact = false;
            string jsonString = json.Emit();
            File.WriteAllText(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User), jsonString);
            //Refresh treeview
            jsonString = File.ReadAllText(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User), Encoding.UTF8);
            try
            {
                var userObj = JObject.Parse(jsonString);
                TreeViewUtilities.ObjectToTreeView.SetObjectAsJson(tvInputFile, userObj);
            }
            catch (Exception pe)
            {
                Utilities.Utilities.WriteLogItem("Parsing error on loading InputFields form: " + pe.ToString(), System.Diagnostics.TraceEventType.Error);
            }
            isDirtyFlag = false;
            MessageBox.Show("Input File updated successfully.");
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(txtInputValue.Text, out number1);
            if (canConvert)
            {
                mySelectedNode.Text = txtInputFieldName.Text + " : " + txtInputValue.Text;
            }
            else
            {
                mySelectedNode.Text = txtInputFieldName.Text + " : " + txtInputValue.Text;
                //mySelectedNode.Text = txtInputFieldName.Text + " : " + "\"" + txtInputValue.Text + "\"";
            }
            tvInputFile.Refresh();
            tvInputFile.SelectedNode = mySelectedNode;
            isDirtyFlag = true;
        }

        // Clears the passed-in treeView and rebuilds from JSON.
        static public void FromJson(string strJson, TreeView treeView)
        {
            treeView.Nodes.Clear();

            Chilkat.JsonObject tvJson = new Chilkat.JsonObject();
            tvJson.Load(strJson);
            Chilkat.JsonArray tvNodes = tvJson.ArrayOf("treeViewNodes");

            int numNodes = tvNodes.Size;
            for (int i = 0; i < numNodes; i++)
            {
                Chilkat.JsonObject json = tvNodes.ObjectAt(i);

                if (json.IsNullOf("parentName"))
                {
                    TreeNode node = treeView.Nodes.Add(json.StringOf("name"), json.StringOf("text"));
                    restoreNode(node, json);
                }
                else
                {
                    // Assumes unique names (i.e. keys)
                    TreeNode[] foundNodes = treeView.Nodes.Find(json.StringOf("parentName"), true);
                    if (foundNodes.Length > 0)
                    {
                        TreeNode node = foundNodes[0].Nodes.Add(json.StringOf("name"), json.StringOf("text"));
                        restoreNode(node, json);
                    }
                }

            }
        }

        private void recurseTree(TreeNode treeNode, Chilkat.JsonObject json)
        {
            string tag;
            if (treeNode.Tag == null)
            {
                tag = treeNode.Name;
            }
            else
            {
                tag = treeNode.Tag.ToString();
                if (tag.Length == 0)
                {
                    tag = treeNode.Name;
                }
            }

            int numChildren = treeNode.Nodes.Count;
            if (numChildren == 0)
            {
                string[] tempTextArray = treeNode.Text.Split(":".ToCharArray());
                String tempString = "";
                if (tempTextArray.Length > 0)
                {
                    tempString = tempTextArray[tempTextArray.Length - 1].Trim();

                }
                if (treeNode.Text.Contains("{Object}"))
                {
                    tempString = tempString.Replace("{Object}", "");
                }
                if (treeNode.Text.Contains("{Array}"))
                {
                    tempString = tempString.Replace("{Array}", "");
                }
                if (tempString.Length == 0)
                {
                    tempString = treeNode.Text;
                }
                tempString = tempString.Replace("\"", "");
                int number1 = 0;
                bool booleanFlag = false;
                bool canConvert = int.TryParse(tempString, out number1);
                if (canConvert) json.AppendInt(tag, (int)number1);
                bool canConvert2 = Boolean.TryParse(tempString, out booleanFlag);
                if (canConvert2) json.AppendBool(tag, booleanFlag);
                if (!canConvert && !canConvert2) json.AppendString(tag, tempString);
                return;
            }

            Chilkat.JsonObject jObj = null;
            if (tag.Length > 0) json.AppendObject(tag);
            else jObj = json;

            foreach (TreeNode tn in treeNode.Nodes)
            {
                recurseTree(tn, jObj);
            }
        }

        // Restore the properties of a TreeNode from JSON.
        static private void restoreNode(TreeNode node, Chilkat.JsonObject json)
        {
            node.Tag = json.StringOf("tag");
            node.Text = json.StringOf("text");
            node.ToolTipText = json.StringOf("toolTipText");
            node.Checked = json.BoolOf("checked");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isDirtyFlag) File.WriteAllText(System.Environment.GetEnvironmentVariable("editFile", EnvironmentVariableTarget.User), ToJson(tvInputFile));
            isDirtyFlag = false;
            this.Close();
        }
    }
}
