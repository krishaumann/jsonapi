namespace JSONAPI
{
    partial class frmObjectMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectMap));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddObject = new JSONAPI.Controls.JSONAPIButton();
            this.btnCancelObject = new JSONAPI.Controls.JSONAPIButton();
            this.btnValidateXPath = new JSONAPI.Controls.JSONAPIButton();
            this.chkValidated = new System.Windows.Forms.CheckBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtXPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgObjectMap = new System.Windows.Forms.DataGridView();
            this.ctxObjectMapGridOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuDeleteObjectMap = new System.Windows.Forms.ToolStripMenuItem();
            this.bsObjectMap = new System.Windows.Forms.BindingSource(this.components);
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsValidated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgObjectMap)).BeginInit();
            this.ctxObjectMapGridOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsObjectMap)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.dgObjectMap);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddObject);
            this.groupBox1.Controls.Add(this.btnCancelObject);
            this.groupBox1.Controls.Add(this.btnValidateXPath);
            this.groupBox1.Controls.Add(this.chkValidated);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtXPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object Element";
            // 
            // btnAddObject
            // 
            this.btnAddObject.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddObject.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddObject.Location = new System.Drawing.Point(560, 148);
            this.btnAddObject.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(103, 32);
            this.btnAddObject.TabIndex = 7;
            this.btnAddObject.Text = "Add";
            this.btnAddObject.UseVisualStyleBackColor = false;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // btnCancelObject
            // 
            this.btnCancelObject.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancelObject.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancelObject.Location = new System.Drawing.Point(667, 148);
            this.btnCancelObject.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancelObject.Name = "btnCancelObject";
            this.btnCancelObject.Size = new System.Drawing.Size(109, 32);
            this.btnCancelObject.TabIndex = 8;
            this.btnCancelObject.Text = "Cancel";
            this.btnCancelObject.UseVisualStyleBackColor = false;
            this.btnCancelObject.Click += new System.EventHandler(this.btnCancelObject_Click);
            // 
            // btnValidateXPath
            // 
            this.btnValidateXPath.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnValidateXPath.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValidateXPath.Location = new System.Drawing.Point(370, 148);
            this.btnValidateXPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnValidateXPath.Name = "btnValidateXPath";
            this.btnValidateXPath.Size = new System.Drawing.Size(132, 32);
            this.btnValidateXPath.TabIndex = 8;
            this.btnValidateXPath.Text = "Validate XPath";
            this.btnValidateXPath.UseVisualStyleBackColor = false;
            // 
            // chkValidated
            // 
            this.chkValidated.AutoSize = true;
            this.chkValidated.Enabled = false;
            this.chkValidated.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkValidated.Location = new System.Drawing.Point(696, 23);
            this.chkValidated.Name = "chkValidated";
            this.chkValidated.Size = new System.Drawing.Size(80, 20);
            this.chkValidated.TabIndex = 7;
            this.chkValidated.Text = "Validated";
            this.chkValidated.UseVisualStyleBackColor = true;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(117, 108);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(573, 23);
            this.txtUrl.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Url";
            // 
            // txtXPath
            // 
            this.txtXPath.Location = new System.Drawing.Point(117, 69);
            this.txtXPath.Name = "txtXPath";
            this.txtXPath.Size = new System.Drawing.Size(573, 23);
            this.txtXPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Element XPath";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(117, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(308, 23);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Element Name";
            // 
            // dgObjectMap
            // 
            this.dgObjectMap.AllowUserToAddRows = false;
            this.dgObjectMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObjectMap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colObjectName,
            this.colXPath,
            this.colURL,
            this.colIsValidated,
            this.colUserName});
            this.dgObjectMap.ContextMenuStrip = this.ctxObjectMapGridOptions;
            this.dgObjectMap.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgObjectMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgObjectMap.Location = new System.Drawing.Point(0, 0);
            this.dgObjectMap.MultiSelect = false;
            this.dgObjectMap.Name = "dgObjectMap";
            this.dgObjectMap.RowHeadersVisible = false;
            this.dgObjectMap.RowTemplate.Height = 25;
            this.dgObjectMap.Size = new System.Drawing.Size(800, 240);
            this.dgObjectMap.TabIndex = 0;
            this.dgObjectMap.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgObjectMap_CellBeginEdit);
            // 
            // ctxObjectMapGridOptions
            // 
            this.ctxObjectMapGridOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuDeleteObjectMap});
            this.ctxObjectMapGridOptions.Name = "ctxObjectMapGridOptions";
            this.ctxObjectMapGridOptions.Size = new System.Drawing.Size(108, 26);
            // 
            // tsMenuDeleteObjectMap
            // 
            this.tsMenuDeleteObjectMap.Name = "tsMenuDeleteObjectMap";
            this.tsMenuDeleteObjectMap.Size = new System.Drawing.Size(107, 22);
            this.tsMenuDeleteObjectMap.Text = "Delete";
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "Select";
            this.colSelect.Name = "colSelect";
            // 
            // colObjectName
            // 
            this.colObjectName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colObjectName.DataPropertyName = "objectName";
            this.colObjectName.HeaderText = "Object Name";
            this.colObjectName.Name = "colObjectName";
            // 
            // colXPath
            // 
            this.colXPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colXPath.DataPropertyName = "xpath";
            this.colXPath.HeaderText = "XPath";
            this.colXPath.Name = "colXPath";
            // 
            // colURL
            // 
            this.colURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colURL.DataPropertyName = "url";
            this.colURL.HeaderText = "URL";
            this.colURL.Name = "colURL";
            // 
            // colIsValidated
            // 
            this.colIsValidated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIsValidated.DataPropertyName = "isValidated";
            this.colIsValidated.HeaderText = "Is Validated";
            this.colIsValidated.Name = "colIsValidated";
            // 
            // colUserName
            // 
            this.colUserName.DataPropertyName = "username";
            this.colUserName.HeaderText = "User Name";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = false;
            // 
            // frmObjectMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmObjectMap";
            this.Text = "Object Map";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgObjectMap)).EndInit();
            this.ctxObjectMapGridOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsObjectMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkValidated;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtXPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private Controls.JSONAPIButton btnAddObject;
        private Controls.JSONAPIButton btnCancelObject;
        private Controls.JSONAPIButton btnValidateXPath;
        private System.Windows.Forms.DataGridView dgObjectMap;
        private System.Windows.Forms.BindingSource bsObjectMap;
        private System.Windows.Forms.ContextMenuStrip ctxObjectMapGridOptions;
        private System.Windows.Forms.ToolStripMenuItem tsMenuDeleteObjectMap;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colURL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsValidated;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colUserName;
    }
}