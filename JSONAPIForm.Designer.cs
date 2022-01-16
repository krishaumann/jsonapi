
using System.Windows.Forms;

namespace JSONAPI
{
    partial class JSONAPIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ImageList imageList;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddExpectedResult = new JSONAPI.Controls.JSONAPIButton();
            this.btnHttpHeader = new JSONAPI.Controls.JSONAPIButton();
            this.btnCopySelected = new JSONAPI.Controls.JSONAPIButton();
            this.httpResponseTime = new System.Windows.Forms.Label();
            this.tbResponse = new System.Windows.Forms.TabControl();
            this.tbAttributes = new System.Windows.Forms.TabPage();
            this.dgHttpHeader = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponseHeaderAttrKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponseHeaderAttrValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbResponseHdr = new System.Windows.Forms.TabPage();
            this.txtHttpResponseHeader = new System.Windows.Forms.RichTextBox();
            this.tbResponseDet = new System.Windows.Forms.TabPage();
            this.txtHttpResponseBody = new System.Windows.Forms.RichTextBox();
            this.btnInputFields = new JSONAPI.Controls.JSONAPIButton();
            this.httpResponseCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSend = new JSONAPI.Controls.JSONAPIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvDirectory = new System.Windows.Forms.TreeView();
            this.ctxOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddJSONFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddXMLFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAddJSFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxChangeFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.txtHttpInput = new System.Windows.Forms.RichTextBox();
            this.btnSaveDetail = new JSONAPI.Controls.JSONAPIButton();
            this.btnOptions = new JSONAPI.Controls.JSONAPIButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tbResponse.SuspendLayout();
            this.tbAttributes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHttpHeader)).BeginInit();
            this.tbResponseHdr.SuspendLayout();
            this.tbResponseDet.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ctxOptions.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.08511F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.08511F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.32931F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.68882F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.848943F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddExpectedResult, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnHttpHeader, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCopySelected, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.httpResponseTime, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbResponse, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnInputFields, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.httpResponseCode, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSend, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tvDirectory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtHttpInput, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveDetail, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOptions, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2054, 781);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // btnAddExpectedResult
            // 
            this.btnAddExpectedResult.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddExpectedResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddExpectedResult.Enabled = false;
            this.btnAddExpectedResult.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddExpectedResult.Location = new System.Drawing.Point(1852, 742);
            this.btnAddExpectedResult.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddExpectedResult.Name = "btnAddExpectedResult";
            this.btnAddExpectedResult.Size = new System.Drawing.Size(200, 37);
            this.btnAddExpectedResult.TabIndex = 6;
            this.btnAddExpectedResult.Text = "Add as Expected Result";
            this.btnAddExpectedResult.UseVisualStyleBackColor = false;
            // 
            // btnHttpHeader
            // 
            this.btnHttpHeader.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnHttpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHttpHeader.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHttpHeader.Location = new System.Drawing.Point(1852, 42);
            this.btnHttpHeader.Margin = new System.Windows.Forms.Padding(2);
            this.btnHttpHeader.Name = "btnHttpHeader";
            this.btnHttpHeader.Size = new System.Drawing.Size(200, 45);
            this.btnHttpHeader.TabIndex = 2;
            this.btnHttpHeader.Text = "HTTP Header..";
            this.btnHttpHeader.UseVisualStyleBackColor = false;
            // 
            // btnCopySelected
            // 
            this.btnCopySelected.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCopySelected.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopySelected.Location = new System.Drawing.Point(1670, 742);
            this.btnCopySelected.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopySelected.Name = "btnCopySelected";
            this.btnCopySelected.Size = new System.Drawing.Size(169, 37);
            this.btnCopySelected.TabIndex = 1;
            this.btnCopySelected.Text = "Copy Selected";
            this.btnCopySelected.UseVisualStyleBackColor = false;
            // 
            // httpResponseTime
            // 
            this.httpResponseTime.AutoSize = true;
            this.httpResponseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.httpResponseTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.httpResponseTime.Location = new System.Drawing.Point(1852, 367);
            this.httpResponseTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.httpResponseTime.Name = "httpResponseTime";
            this.httpResponseTime.Size = new System.Drawing.Size(200, 46);
            this.httpResponseTime.TabIndex = 5;
            // 
            // tbResponse
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbResponse, 4);
            this.tbResponse.Controls.Add(this.tbAttributes);
            this.tbResponse.Controls.Add(this.tbResponseHdr);
            this.tbResponse.Controls.Add(this.tbResponseDet);
            this.tbResponse.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResponse.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbResponse.Location = new System.Drawing.Point(670, 415);
            this.tbResponse.Margin = new System.Windows.Forms.Padding(2);
            this.tbResponse.Name = "tbResponse";
            this.tbResponse.SelectedIndex = 0;
            this.tbResponse.Size = new System.Drawing.Size(1382, 323);
            this.tbResponse.TabIndex = 1;
            // 
            // tbAttributes
            // 
            this.tbAttributes.Controls.Add(this.dgHttpHeader);
            this.tbAttributes.Location = new System.Drawing.Point(4, 25);
            this.tbAttributes.Margin = new System.Windows.Forms.Padding(2);
            this.tbAttributes.Name = "tbAttributes";
            this.tbAttributes.Padding = new System.Windows.Forms.Padding(2);
            this.tbAttributes.Size = new System.Drawing.Size(1374, 294);
            this.tbAttributes.TabIndex = 0;
            this.tbAttributes.Text = "Attributes";
            this.tbAttributes.UseVisualStyleBackColor = true;
            // 
            // dgHttpHeader
            // 
            this.dgHttpHeader.AllowUserToAddRows = false;
            this.dgHttpHeader.AllowUserToDeleteRows = false;
            this.dgHttpHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHttpHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colType,
            this.colResponseHeaderAttrKey,
            this.colResponseHeaderAttrValue});
            this.dgHttpHeader.ContextMenuStrip = this.contextMenuStrip1;
            this.dgHttpHeader.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgHttpHeader.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgHttpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHttpHeader.Location = new System.Drawing.Point(2, 2);
            this.dgHttpHeader.Margin = new System.Windows.Forms.Padding(2);
            this.dgHttpHeader.Name = "dgHttpHeader";
            this.dgHttpHeader.RowHeadersVisible = false;
            this.dgHttpHeader.RowHeadersWidth = 51;
            this.dgHttpHeader.RowTemplate.Height = 25;
            this.dgHttpHeader.Size = new System.Drawing.Size(1370, 290);
            this.dgHttpHeader.TabIndex = 1;
            // 
            // colSelect
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.NullValue = false;
            this.colSelect.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSelect.FalseValue = "false";
            this.colSelect.HeaderText = "Select";
            this.colSelect.MinimumWidth = 70;
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSelect.TrueValue = "true";
            this.colSelect.Width = 70;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.colType.DefaultCellStyle = dataGridViewCellStyle2;
            this.colType.FillWeight = 204.0816F;
            this.colType.HeaderText = "Type";
            this.colType.MinimumWidth = 100;
            this.colType.Name = "colType";
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colType.Width = 125;
            // 
            // colResponseHeaderAttrKey
            // 
            this.colResponseHeaderAttrKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colResponseHeaderAttrKey.DefaultCellStyle = dataGridViewCellStyle3;
            this.colResponseHeaderAttrKey.FillWeight = 47.95918F;
            this.colResponseHeaderAttrKey.HeaderText = "Attribute Key";
            this.colResponseHeaderAttrKey.MinimumWidth = 6;
            this.colResponseHeaderAttrKey.Name = "colResponseHeaderAttrKey";
            this.colResponseHeaderAttrKey.ReadOnly = true;
            // 
            // colResponseHeaderAttrValue
            // 
            this.colResponseHeaderAttrValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colResponseHeaderAttrValue.DefaultCellStyle = dataGridViewCellStyle4;
            this.colResponseHeaderAttrValue.FillWeight = 47.95918F;
            this.colResponseHeaderAttrValue.HeaderText = "Attribute Value";
            this.colResponseHeaderAttrValue.MinimumWidth = 6;
            this.colResponseHeaderAttrValue.Name = "colResponseHeaderAttrValue";
            this.colResponseHeaderAttrValue.ReadOnly = true;
            // 
            // tbResponseHdr
            // 
            this.tbResponseHdr.Controls.Add(this.txtHttpResponseHeader);
            this.tbResponseHdr.Location = new System.Drawing.Point(4, 25);
            this.tbResponseHdr.Margin = new System.Windows.Forms.Padding(2);
            this.tbResponseHdr.Name = "tbResponseHdr";
            this.tbResponseHdr.Padding = new System.Windows.Forms.Padding(2);
            this.tbResponseHdr.Size = new System.Drawing.Size(1372, 293);
            this.tbResponseHdr.TabIndex = 1;
            this.tbResponseHdr.Text = "Response Header";
            this.tbResponseHdr.UseVisualStyleBackColor = true;
            // 
            // txtHttpResponseHeader
            // 
            this.txtHttpResponseHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHttpResponseHeader.Location = new System.Drawing.Point(2, 2);
            this.txtHttpResponseHeader.Margin = new System.Windows.Forms.Padding(2);
            this.txtHttpResponseHeader.Name = "txtHttpResponseHeader";
            this.txtHttpResponseHeader.Size = new System.Drawing.Size(1368, 289);
            this.txtHttpResponseHeader.TabIndex = 0;
            this.txtHttpResponseHeader.Text = "";
            // 
            // tbResponseDet
            // 
            this.tbResponseDet.Controls.Add(this.txtHttpResponseBody);
            this.tbResponseDet.Location = new System.Drawing.Point(4, 25);
            this.tbResponseDet.Margin = new System.Windows.Forms.Padding(2);
            this.tbResponseDet.Name = "tbResponseDet";
            this.tbResponseDet.Padding = new System.Windows.Forms.Padding(2);
            this.tbResponseDet.Size = new System.Drawing.Size(1372, 293);
            this.tbResponseDet.TabIndex = 2;
            this.tbResponseDet.Text = "Response Detail";
            this.tbResponseDet.UseVisualStyleBackColor = true;
            // 
            // txtHttpResponseBody
            // 
            this.txtHttpResponseBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHttpResponseBody.Location = new System.Drawing.Point(2, 2);
            this.txtHttpResponseBody.Margin = new System.Windows.Forms.Padding(2);
            this.txtHttpResponseBody.Name = "txtHttpResponseBody";
            this.txtHttpResponseBody.Size = new System.Drawing.Size(1368, 289);
            this.txtHttpResponseBody.TabIndex = 0;
            this.txtHttpResponseBody.Text = "";
            // 
            // btnInputFields
            // 
            this.btnInputFields.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnInputFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInputFields.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnInputFields.Location = new System.Drawing.Point(1670, 2);
            this.btnInputFields.Margin = new System.Windows.Forms.Padding(2);
            this.btnInputFields.Name = "btnInputFields";
            this.btnInputFields.Size = new System.Drawing.Size(178, 36);
            this.btnInputFields.TabIndex = 4;
            this.btnInputFields.Text = "Input Fields..";
            this.btnInputFields.UseVisualStyleBackColor = false;
            // 
            // httpResponseCode
            // 
            this.httpResponseCode.AutoSize = true;
            this.httpResponseCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.httpResponseCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.httpResponseCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.httpResponseCode.Location = new System.Drawing.Point(1065, 367);
            this.httpResponseCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.httpResponseCode.Name = "httpResponseCode";
            this.httpResponseCode.Size = new System.Drawing.Size(601, 46);
            this.httpResponseCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Yu Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(1670, 367);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "Response Time:";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Font = new System.Drawing.Font("Yu Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSend.Location = new System.Drawing.Point(1852, 2);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(200, 36);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Yu Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(670, 367);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Response Code:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userProfileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(87, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userProfileToolStripMenuItem
            // 
            this.userProfileToolStripMenuItem.Name = "userProfileToolStripMenuItem";
            this.userProfileToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.userProfileToolStripMenuItem.Text = "User Profile";
            // 
            // tvDirectory
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tvDirectory, 2);
            this.tvDirectory.ContextMenuStrip = this.ctxOptions;
            this.tvDirectory.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDirectory.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tvDirectory.Location = new System.Drawing.Point(2, 42);
            this.tvDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.tvDirectory.Name = "tvDirectory";
            this.tableLayoutPanel1.SetRowSpan(this.tvDirectory, 4);
            this.tvDirectory.SelectedImageKey = "folder";
            this.tvDirectory.Size = new System.Drawing.Size(664, 737);
            this.tvDirectory.TabIndex = 0;
            // 
            // ctxOptions
            // 
            this.ctxOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuNew,
            this.ctxChangeFolder,
            this.ctxMenuDelete,
            this.ctxRefresh});
            this.ctxOptions.Name = "ctxOptions";
            this.ctxOptions.Size = new System.Drawing.Size(158, 92);
            // 
            // ctxMenuNew
            // 
            this.ctxMenuNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuAddFolder,
            this.ctxMenuAddJSONFile,
            this.ctxMenuAddXMLFile,
            this.ctxMenuAddJSFile});
            this.ctxMenuNew.Name = "ctxMenuNew";
            this.ctxMenuNew.Size = new System.Drawing.Size(157, 22);
            this.ctxMenuNew.Text = "New";
            // 
            // ctxMenuAddFolder
            // 
            this.ctxMenuAddFolder.Name = "ctxMenuAddFolder";
            this.ctxMenuAddFolder.Size = new System.Drawing.Size(123, 22);
            this.ctxMenuAddFolder.Text = "Folder";
            // 
            // ctxMenuAddJSONFile
            // 
            this.ctxMenuAddJSONFile.Name = "ctxMenuAddJSONFile";
            this.ctxMenuAddJSONFile.Size = new System.Drawing.Size(123, 22);
            this.ctxMenuAddJSONFile.Text = "JSON File";
            // 
            // ctxMenuAddXMLFile
            // 
            this.ctxMenuAddXMLFile.Name = "ctxMenuAddXMLFile";
            this.ctxMenuAddXMLFile.Size = new System.Drawing.Size(123, 22);
            this.ctxMenuAddXMLFile.Text = "XML File";
            // 
            // ctxMenuAddJSFile
            // 
            this.ctxMenuAddJSFile.Name = "ctxMenuAddJSFile";
            this.ctxMenuAddJSFile.Size = new System.Drawing.Size(123, 22);
            this.ctxMenuAddJSFile.Text = "JS File";
            // 
            // ctxChangeFolder
            // 
            this.ctxChangeFolder.Name = "ctxChangeFolder";
            this.ctxChangeFolder.Size = new System.Drawing.Size(157, 22);
            this.ctxChangeFolder.Text = "Change Folder..";
            // 
            // ctxMenuDelete
            // 
            this.ctxMenuDelete.Name = "ctxMenuDelete";
            this.ctxMenuDelete.Size = new System.Drawing.Size(157, 22);
            this.ctxMenuDelete.Text = "Delete";
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(157, 22);
            this.ctxRefresh.Text = "Refresh";
            // 
            // txtHttpInput
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtHttpInput, 3);
            this.txtHttpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHttpInput.Location = new System.Drawing.Point(670, 42);
            this.txtHttpInput.Margin = new System.Windows.Forms.Padding(2);
            this.txtHttpInput.Name = "txtHttpInput";
            this.txtHttpInput.Size = new System.Drawing.Size(1178, 323);
            this.txtHttpInput.TabIndex = 0;
            this.txtHttpInput.Text = "";
            // 
            // btnSaveDetail
            // 
            this.btnSaveDetail.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSaveDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveDetail.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveDetail.Location = new System.Drawing.Point(1517, 2);
            this.btnSaveDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveDetail.Name = "btnSaveDetail";
            this.btnSaveDetail.Size = new System.Drawing.Size(149, 36);
            this.btnSaveDetail.TabIndex = 3;
            this.btnSaveDetail.Text = "Save Input";
            this.btnSaveDetail.UseVisualStyleBackColor = false;
            // 
            // btnOptions
            // 
            this.btnOptions.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOptions.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOptions.Location = new System.Drawing.Point(478, 2);
            this.btnOptions.Margin = new System.Windows.Forms.Padding(2);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(188, 36);
            this.btnOptions.TabIndex = 1;
            this.btnOptions.Text = "Options..";
            this.btnOptions.UseVisualStyleBackColor = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 26);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // JSONAPIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2062, 786);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "JSONAPIForm";
            this.Text = "JSONAPI";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tbResponse.ResumeLayout(false);
            this.tbAttributes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHttpHeader)).EndInit();
            this.tbResponseHdr.ResumeLayout(false);
            this.tbResponseDet.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ctxOptions.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.JSONAPIButton btnAddExpectedResult;
        private Controls.JSONAPIButton btnHttpHeader;
        private Controls.JSONAPIButton btnCopySelected;
        private System.Windows.Forms.Label httpResponseTime;
        private System.Windows.Forms.TabControl tbResponse;
        private System.Windows.Forms.TabPage tbAttributes;
        private System.Windows.Forms.DataGridView dgHttpHeader;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponseHeaderAttrKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponseHeaderAttrValue;
        private System.Windows.Forms.TabPage tbResponseHdr;
        private System.Windows.Forms.RichTextBox txtHttpResponseHeader;
        private System.Windows.Forms.TabPage tbResponseDet;
        private System.Windows.Forms.RichTextBox txtHttpResponseBody;
        private Controls.JSONAPIButton btnInputFields;
        private System.Windows.Forms.Label httpResponseCode;
        private System.Windows.Forms.Label label2;
        private Controls.JSONAPIButton btnOptions;
        private Controls.JSONAPIButton btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem userProfileToolStripMenuItem;
        private System.Windows.Forms.TreeView tvDirectory;
        private System.Windows.Forms.RichTextBox txtHttpInput;
        private Controls.JSONAPIButton btnSaveDetail;
        private ContextMenuStrip ctxOptions;
        private ToolStripMenuItem ctxMenuNew;
        private ToolStripMenuItem ctxMenuAddFolder;
        private ToolStripMenuItem ctxMenuAddJSONFile;
        private ToolStripMenuItem ctxMenuAddXMLFile;
        private ToolStripMenuItem ctxMenuAddJSFile;
        private ToolStripMenuItem ctxChangeFolder;
        private ToolStripMenuItem ctxMenuDelete;
        private ToolStripMenuItem ctxRefresh;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exportToolStripMenuItem;
    }
}