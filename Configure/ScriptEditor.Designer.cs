namespace JSONAPI.Configure
{
    partial class frmScriptEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStepName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbStepDesc = new System.Windows.Forms.TabPage();
            this.numSequence = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbInputRV = new System.Windows.Forms.ComboBox();
            this.cmbURL = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOpenObjectMap = new JSONAPI.Controls.JSONAPIButton();
            this.cmbObjectMapItem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInputValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbInputData = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbOperationDesc = new System.Windows.Forms.ComboBox();
            this.tbExpectedResult = new System.Windows.Forms.TabPage();
            this.cmbAttribute = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExpectedValue = new System.Windows.Forms.TextBox();
            this.cmbVerificationType = new System.Windows.Forms.ComboBox();
            this.txtElementName = new System.Windows.Forms.TextBox();
            this.btnCancel = new JSONAPI.Controls.JSONAPIButton();
            this.btnAddGUITestStep = new JSONAPI.Controls.JSONAPIButton();
            this.dgGUISteps = new System.Windows.Forms.DataGridView();
            this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeaderInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldExpectedResultList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxObjectMapGridOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuDeleteGUITest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuAddGUITest = new System.Windows.Forms.ToolStripMenuItem();
            this.bsGUISteps = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbStepDesc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSequence)).BeginInit();
            this.tbExpectedResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGUISteps)).BeginInit();
            this.ctxObjectMapGridOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsGUISteps)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgGUISteps);
            this.splitContainer1.Size = new System.Drawing.Size(800, 643);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStepName);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnAddGUITestStep);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 364);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Navigation Steps";
            // 
            // txtStepName
            // 
            this.txtStepName.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtStepName.Location = new System.Drawing.Point(93, 30);
            this.txtStepName.Name = "txtStepName";
            this.txtStepName.Size = new System.Drawing.Size(659, 23);
            this.txtStepName.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "Test Name";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbStepDesc);
            this.tabControl1.Controls.Add(this.tbExpectedResult);
            this.tabControl1.Location = new System.Drawing.Point(6, 74);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(779, 252);
            this.tabControl1.TabIndex = 16;
            // 
            // tbStepDesc
            // 
            this.tbStepDesc.Controls.Add(this.numSequence);
            this.tbStepDesc.Controls.Add(this.label1);
            this.tbStepDesc.Controls.Add(this.cmbInputRV);
            this.tbStepDesc.Controls.Add(this.cmbURL);
            this.tbStepDesc.Controls.Add(this.label7);
            this.tbStepDesc.Controls.Add(this.btnOpenObjectMap);
            this.tbStepDesc.Controls.Add(this.cmbObjectMapItem);
            this.tbStepDesc.Controls.Add(this.label2);
            this.tbStepDesc.Controls.Add(this.txtInputValue);
            this.tbStepDesc.Controls.Add(this.label6);
            this.tbStepDesc.Controls.Add(this.cmbInputData);
            this.tbStepDesc.Controls.Add(this.label3);
            this.tbStepDesc.Controls.Add(this.label5);
            this.tbStepDesc.Controls.Add(this.cmbOperationDesc);
            this.tbStepDesc.Location = new System.Drawing.Point(4, 24);
            this.tbStepDesc.Name = "tbStepDesc";
            this.tbStepDesc.Padding = new System.Windows.Forms.Padding(3);
            this.tbStepDesc.Size = new System.Drawing.Size(771, 224);
            this.tbStepDesc.TabIndex = 0;
            this.tbStepDesc.Text = "Step Desc";
            this.tbStepDesc.UseVisualStyleBackColor = true;
            // 
            // numSequence
            // 
            this.numSequence.Cursor = System.Windows.Forms.Cursors.Default;
            this.numSequence.Location = new System.Drawing.Point(106, 13);
            this.numSequence.Name = "numSequence";
            this.numSequence.Size = new System.Drawing.Size(39, 23);
            this.numSequence.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Sequence";
            // 
            // cmbInputRV
            // 
            this.cmbInputRV.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbInputRV.FormattingEnabled = true;
            this.cmbInputRV.Items.AddRange(new object[] {
            "Click",
            "Type",
            "Validate",
            "Hover"});
            this.cmbInputRV.Location = new System.Drawing.Point(384, 175);
            this.cmbInputRV.Name = "cmbInputRV";
            this.cmbInputRV.Size = new System.Drawing.Size(280, 23);
            this.cmbInputRV.TabIndex = 19;
            this.cmbInputRV.Visible = false;
            // 
            // cmbURL
            // 
            this.cmbURL.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbURL.FormattingEnabled = true;
            this.cmbURL.Location = new System.Drawing.Point(106, 50);
            this.cmbURL.Name = "cmbURL";
            this.cmbURL.Size = new System.Drawing.Size(636, 23);
            this.cmbURL.TabIndex = 18;
            this.cmbURL.TextChanged += new System.EventHandler(this.cmbURL_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "URL";
            // 
            // btnOpenObjectMap
            // 
            this.btnOpenObjectMap.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOpenObjectMap.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOpenObjectMap.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenObjectMap.Location = new System.Drawing.Point(611, 127);
            this.btnOpenObjectMap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenObjectMap.Name = "btnOpenObjectMap";
            this.btnOpenObjectMap.Size = new System.Drawing.Size(132, 32);
            this.btnOpenObjectMap.TabIndex = 16;
            this.btnOpenObjectMap.Text = "Add Object..";
            this.btnOpenObjectMap.UseVisualStyleBackColor = false;
            this.btnOpenObjectMap.Click += new System.EventHandler(this.btnOpenObjectMap_Click);
            // 
            // cmbObjectMapItem
            // 
            this.cmbObjectMapItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbObjectMapItem.FormattingEnabled = true;
            this.cmbObjectMapItem.Location = new System.Drawing.Point(106, 89);
            this.cmbObjectMapItem.Name = "cmbObjectMapItem";
            this.cmbObjectMapItem.Size = new System.Drawing.Size(635, 23);
            this.cmbObjectMapItem.TabIndex = 15;
            this.cmbObjectMapItem.TextChanged += new System.EventHandler(this.cmbObjectMapItem_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Element XPath";
            // 
            // txtInputValue
            // 
            this.txtInputValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtInputValue.Location = new System.Drawing.Point(384, 175);
            this.txtInputValue.Name = "txtInputValue";
            this.txtInputValue.Size = new System.Drawing.Size(358, 23);
            this.txtInputValue.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Value";
            // 
            // cmbInputData
            // 
            this.cmbInputData.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbInputData.FormattingEnabled = true;
            this.cmbInputData.Items.AddRange(new object[] {
            "Range",
            "Variable",
            "Static"});
            this.cmbInputData.Location = new System.Drawing.Point(106, 175);
            this.cmbInputData.Name = "cmbInputData";
            this.cmbInputData.Size = new System.Drawing.Size(214, 23);
            this.cmbInputData.TabIndex = 12;
            this.cmbInputData.TextChanged += new System.EventHandler(this.cmbInputData_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Operation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input Data";
            // 
            // cmbOperationDesc
            // 
            this.cmbOperationDesc.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbOperationDesc.FormattingEnabled = true;
            this.cmbOperationDesc.Items.AddRange(new object[] {
            "Click",
            "Type",
            "Validate",
            "Hover",
            "Select"});
            this.cmbOperationDesc.Location = new System.Drawing.Point(106, 127);
            this.cmbOperationDesc.Name = "cmbOperationDesc";
            this.cmbOperationDesc.Size = new System.Drawing.Size(280, 23);
            this.cmbOperationDesc.TabIndex = 8;
            this.cmbOperationDesc.TextChanged += new System.EventHandler(this.cmbOperationDesc_TextChanged);
            // 
            // tbExpectedResult
            // 
            this.tbExpectedResult.Controls.Add(this.cmbAttribute);
            this.tbExpectedResult.Controls.Add(this.label11);
            this.tbExpectedResult.Controls.Add(this.label10);
            this.tbExpectedResult.Controls.Add(this.label9);
            this.tbExpectedResult.Controls.Add(this.label8);
            this.tbExpectedResult.Controls.Add(this.txtExpectedValue);
            this.tbExpectedResult.Controls.Add(this.cmbVerificationType);
            this.tbExpectedResult.Controls.Add(this.txtElementName);
            this.tbExpectedResult.Location = new System.Drawing.Point(4, 24);
            this.tbExpectedResult.Name = "tbExpectedResult";
            this.tbExpectedResult.Padding = new System.Windows.Forms.Padding(3);
            this.tbExpectedResult.Size = new System.Drawing.Size(771, 224);
            this.tbExpectedResult.TabIndex = 2;
            this.tbExpectedResult.Text = "Expected Result";
            this.tbExpectedResult.UseVisualStyleBackColor = true;
            // 
            // cmbAttribute
            // 
            this.cmbAttribute.FormattingEnabled = true;
            this.cmbAttribute.Items.AddRange(new object[] {
            "accept",
            "accept-charset",
            "accesskey",
            "action",
            "align",
            "allow",
            "alt",
            "async",
            "autocapitalize",
            "autocomplete",
            "autofocus",
            "autoplay",
            "background",
            "bgcolor",
            "border",
            "buffered",
            "capture",
            "challenge",
            "charset",
            "checked",
            "cite",
            "class",
            "code",
            "codebase",
            "color",
            "cols",
            "colspan",
            "content",
            "contenteditable",
            "contextmenu",
            "controls",
            "coords",
            "crossorigin",
            "csp",
            "data",
            "data-*",
            "datetime",
            "decoding",
            "default",
            "defer",
            "dir",
            "dirname",
            "disabled",
            "download",
            "draggable",
            "enctype",
            "enterkeyhint",
            "for",
            "form",
            "formaction",
            "formenctype",
            "formmethod",
            "formnovalidate",
            "formtarget",
            "headers",
            "height",
            "hidden",
            "high",
            "href",
            "hreflang",
            "http-equiv",
            "icon",
            "id",
            "importance",
            "integrity",
            "intrinsicsize",
            "inputmode",
            "ismap",
            "itemprop",
            "keytype",
            "kind",
            "label",
            "lang",
            "language",
            "loading",
            "list",
            "loop",
            "low",
            "manifest",
            "max",
            "maxlength",
            "minlength",
            "media",
            "method",
            "min",
            "multiple",
            "muted",
            "name",
            "novalidate",
            "open",
            "optimum",
            "pattern",
            "ping",
            "placeholder",
            "poster",
            "preload",
            "radiogroup",
            "readonly",
            "referrerpolicy",
            "rel",
            "required",
            "reversed",
            "rows",
            "rowspan",
            "sandbox",
            "scope",
            "scoped",
            "selected",
            "shape",
            "size",
            "sizes",
            "slot",
            "span",
            "spellcheck",
            "src",
            "srcdoc",
            "srclang",
            "srcset",
            "start",
            "step",
            "style",
            "summary",
            "tabindex",
            "target",
            "title",
            "translate",
            "type",
            "usemap",
            "value",
            "width",
            "wrap"});
            this.cmbAttribute.Location = new System.Drawing.Point(120, 47);
            this.cmbAttribute.Name = "cmbAttribute";
            this.cmbAttribute.Size = new System.Drawing.Size(202, 23);
            this.cmbAttribute.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "Attribute";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 15);
            this.label10.TabIndex = 5;
            this.label10.Text = "Expected Value";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Verification Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Element";
            // 
            // txtExpectedValue
            // 
            this.txtExpectedValue.Location = new System.Drawing.Point(120, 122);
            this.txtExpectedValue.Name = "txtExpectedValue";
            this.txtExpectedValue.Size = new System.Drawing.Size(325, 23);
            this.txtExpectedValue.TabIndex = 2;
            // 
            // cmbVerificationType
            // 
            this.cmbVerificationType.FormattingEnabled = true;
            this.cmbVerificationType.Items.AddRange(new object[] {
            "Exist",
            "DoesNotExist",
            "EqualToVariableValue",
            "EqualToRangeValue",
            "EqualToValue",
            "EqualToRegEx",
            "=",
            "!=",
            ">",
            ">=",
            "<",
            "<="});
            this.cmbVerificationType.Location = new System.Drawing.Point(120, 87);
            this.cmbVerificationType.Name = "cmbVerificationType";
            this.cmbVerificationType.Size = new System.Drawing.Size(202, 23);
            this.cmbVerificationType.TabIndex = 1;
            this.cmbVerificationType.TextChanged += new System.EventHandler(this.cmbVerificationType_TextChanged);
            // 
            // txtElementName
            // 
            this.txtElementName.Enabled = false;
            this.txtElementName.Location = new System.Drawing.Point(120, 10);
            this.txtElementName.Name = "txtElementName";
            this.txtElementName.Size = new System.Drawing.Size(572, 23);
            this.txtElementName.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(673, 332);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnAddGUITestStep
            // 
            this.btnAddGUITestStep.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddGUITestStep.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnAddGUITestStep.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddGUITestStep.Location = new System.Drawing.Point(537, 332);
            this.btnAddGUITestStep.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddGUITestStep.Name = "btnAddGUITestStep";
            this.btnAddGUITestStep.Size = new System.Drawing.Size(132, 32);
            this.btnAddGUITestStep.TabIndex = 15;
            this.btnAddGUITestStep.Text = "Add";
            this.btnAddGUITestStep.UseVisualStyleBackColor = false;
            this.btnAddGUITestStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // dgGUISteps
            // 
            this.dgGUISteps.AllowUserToAddRows = false;
            this.dgGUISteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGUISteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSequence,
            this.colTestName,
            this.colUserName,
            this.colInput,
            this.colHeaderInput,
            this.colURL,
            this.colXPath,
            this.colOperation,
            this.colFieldExpectedResultList});
            this.dgGUISteps.ContextMenuStrip = this.ctxObjectMapGridOptions;
            this.dgGUISteps.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgGUISteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGUISteps.Location = new System.Drawing.Point(0, 0);
            this.dgGUISteps.Name = "dgGUISteps";
            this.dgGUISteps.RowHeadersVisible = false;
            this.dgGUISteps.RowTemplate.Height = 25;
            this.dgGUISteps.Size = new System.Drawing.Size(800, 259);
            this.dgGUISteps.TabIndex = 0;
            this.dgGUISteps.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgGUISteps_RightMouseClick);
            // 
            // colSequence
            // 
            this.colSequence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSequence.DataPropertyName = "sequence";
            this.colSequence.HeaderText = "Sequence";
            this.colSequence.Name = "colSequence";
            // 
            // colTestName
            // 
            this.colTestName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestName.DataPropertyName = "testName";
            this.colTestName.HeaderText = "Test Name";
            this.colTestName.Name = "colTestName";
            // 
            // colUserName
            // 
            this.colUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserName.DataPropertyName = "userName";
            this.colUserName.HeaderText = "User Name";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = false;
            // 
            // colInput
            // 
            this.colInput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colInput.DataPropertyName = "input";
            this.colInput.HeaderText = "Input";
            this.colInput.Name = "colInput";
            // 
            // colHeaderInput
            // 
            this.colHeaderInput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHeaderInput.DataPropertyName = "headerInput";
            this.colHeaderInput.HeaderText = "Header Input";
            this.colHeaderInput.Name = "colHeaderInput";
            this.colHeaderInput.Visible = false;
            // 
            // colURL
            // 
            this.colURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colURL.DataPropertyName = "url";
            this.colURL.HeaderText = "URL";
            this.colURL.Name = "colURL";
            // 
            // colXPath
            // 
            this.colXPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colXPath.DataPropertyName = "xpath";
            this.colXPath.HeaderText = "XPath";
            this.colXPath.Name = "colXPath";
            // 
            // colOperation
            // 
            this.colOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOperation.DataPropertyName = "operation";
            this.colOperation.HeaderText = "Operation";
            this.colOperation.Name = "colOperation";
            // 
            // colFieldExpectedResultList
            // 
            this.colFieldExpectedResultList.DataPropertyName = "fieldExpectedResultList";
            this.colFieldExpectedResultList.HeaderText = "FieldExpectedResultList";
            this.colFieldExpectedResultList.Name = "colFieldExpectedResultList";
            this.colFieldExpectedResultList.Visible = false;
            // 
            // ctxObjectMapGridOptions
            // 
            this.ctxObjectMapGridOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuDeleteGUITest,
            this.tsMenuAddGUITest});
            this.ctxObjectMapGridOptions.Name = "contextMenuStrip1";
            this.ctxObjectMapGridOptions.Size = new System.Drawing.Size(123, 48);
            // 
            // tsMenuDeleteGUITest
            // 
            this.tsMenuDeleteGUITest.Name = "tsMenuDeleteGUITest";
            this.tsMenuDeleteGUITest.Size = new System.Drawing.Size(122, 22);
            this.tsMenuDeleteGUITest.Text = "Delete";
            this.tsMenuDeleteGUITest.Click += new System.EventHandler(this.tsMenuDeleteGUITest_Click);
            // 
            // tsMenuAddGUITest
            // 
            this.tsMenuAddGUITest.Name = "tsMenuAddGUITest";
            this.tsMenuAddGUITest.Size = new System.Drawing.Size(122, 22);
            this.tsMenuAddGUITest.Text = "Add Step";
            this.tsMenuAddGUITest.Click += new System.EventHandler(this.tsMenuAddGUITest_Click);
            // 
            // frmScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScriptEditor";
            this.Text = "Script Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbStepDesc.ResumeLayout(false);
            this.tbStepDesc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSequence)).EndInit();
            this.tbExpectedResult.ResumeLayout(false);
            this.tbExpectedResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGUISteps)).EndInit();
            this.ctxObjectMapGridOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsGUISteps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgGUISteps;
        private System.Windows.Forms.BindingSource bsGUISteps;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbStepDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInputValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbInputData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOperationDesc;
        private Controls.JSONAPIButton btnCancel;
        private Controls.JSONAPIButton btnAddGUITestStep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tbExpectedResult;
        private Controls.JSONAPIButton btnOpenObjectMap;
        private System.Windows.Forms.ComboBox cmbObjectMapItem;
        private System.Windows.Forms.ComboBox cmbURL;
        private System.Windows.Forms.ComboBox cmbVerificationType;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtExpectedValue;
        private System.Windows.Forms.ComboBox cmbAttribute;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStepName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip ctxObjectMapGridOptions;
        private System.Windows.Forms.ToolStripMenuItem tsMenuDeleteGUITest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSequence;
        private System.Windows.Forms.ComboBox cmbInputRV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeaderInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldExpectedResultList;
        private System.Windows.Forms.ToolStripMenuItem tsMenuAddGUITest;
    }
}