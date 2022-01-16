
namespace JSONAPI.Configure
{
    partial class frmInputFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInputFields));
            this.bsInputFields = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateItem = new JSONAPI.Controls.JSONAPIButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddInputValue = new JSONAPI.Controls.JSONAPIButton();
            this.lstInputType = new System.Windows.Forms.ListBox();
            this.txtShowSample = new System.Windows.Forms.RichTextBox();
            this.txtInputValue = new System.Windows.Forms.RichTextBox();
            this.btnCancel = new JSONAPI.Controls.JSONAPIButton();
            this.btnSaveInputField = new JSONAPI.Controls.JSONAPIButton();
            this.btnValidate = new JSONAPI.Controls.JSONAPIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInputFieldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tvInputFile = new System.Windows.Forms.TreeView();
            this.ctxFileEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAddNewNodeValue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddNewNode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRefreshFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRenameParent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDeleteNode = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bsInputFields)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.ctxFileEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdateItem);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAddInputValue);
            this.groupBox1.Controls.Add(this.lstInputType);
            this.groupBox1.Controls.Add(this.txtShowSample);
            this.groupBox1.Controls.Add(this.txtInputValue);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSaveInputField);
            this.groupBox1.Controls.Add(this.btnValidate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtInputFieldName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(330, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 367);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Field Details";
            // 
            // btnUpdateItem
            // 
            this.btnUpdateItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnUpdateItem.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateItem.Location = new System.Drawing.Point(435, 176);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(110, 30);
            this.btnUpdateItem.TabIndex = 15;
            this.btnUpdateItem.Text = "Update Element";
            this.btnUpdateItem.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Preview";
            // 
            // btnAddInputValue
            // 
            this.btnAddInputValue.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddInputValue.Location = new System.Drawing.Point(154, 98);
            this.btnAddInputValue.Name = "btnAddInputValue";
            this.btnAddInputValue.Size = new System.Drawing.Size(26, 23);
            this.btnAddInputValue.TabIndex = 13;
            this.btnAddInputValue.Text = ">";
            this.btnAddInputValue.UseVisualStyleBackColor = false;
            this.btnAddInputValue.Click += new System.EventHandler(this.btnAddInputValue_Click);
            // 
            // lstInputType
            // 
            this.lstInputType.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstInputType.FormattingEnabled = true;
            this.lstInputType.ItemHeight = 16;
            this.lstInputType.Items.AddRange(new object[] {
            "Variable",
            "Random Number",
            "Random Text",
            "Yesterday",
            "Today",
            "Tomorrow",
            "GUID",
            "Range"});
            this.lstInputType.Location = new System.Drawing.Point(7, 89);
            this.lstInputType.Name = "lstInputType";
            this.lstInputType.Size = new System.Drawing.Size(120, 84);
            this.lstInputType.TabIndex = 12;
            // 
            // txtShowSample
            // 
            this.txtShowSample.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtShowSample.Location = new System.Drawing.Point(208, 234);
            this.txtShowSample.Name = "txtShowSample";
            this.txtShowSample.ReadOnly = true;
            this.txtShowSample.Size = new System.Drawing.Size(337, 69);
            this.txtShowSample.TabIndex = 11;
            this.txtShowSample.Text = "";
            // 
            // txtInputValue
            // 
            this.txtInputValue.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInputValue.Location = new System.Drawing.Point(208, 89);
            this.txtInputValue.Name = "txtInputValue";
            this.txtInputValue.Size = new System.Drawing.Size(337, 69);
            this.txtInputValue.TabIndex = 9;
            this.txtInputValue.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancel.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(470, 329);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveInputField
            // 
            this.btnSaveInputField.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSaveInputField.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveInputField.Location = new System.Drawing.Point(389, 329);
            this.btnSaveInputField.Name = "btnSaveInputField";
            this.btnSaveInputField.Size = new System.Drawing.Size(75, 32);
            this.btnSaveInputField.TabIndex = 7;
            this.btnSaveInputField.Text = "Save";
            this.btnSaveInputField.UseVisualStyleBackColor = false;
            // 
            // btnValidate
            // 
            this.btnValidate.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnValidate.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValidate.Location = new System.Drawing.Point(208, 176);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 30);
            this.btnValidate.TabIndex = 6;
            this.btnValidate.Text = "Preview";
            this.btnValidate.UseVisualStyleBackColor = false;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value";
            // 
            // txtInputFieldName
            // 
            this.txtInputFieldName.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInputFieldName.Location = new System.Drawing.Point(112, 23);
            this.txtInputFieldName.Name = "txtInputFieldName";
            this.txtInputFieldName.Size = new System.Drawing.Size(433, 27);
            this.txtInputFieldName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tvInputFile
            // 
            this.tvInputFile.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tvInputFile.Location = new System.Drawing.Point(13, 12);
            this.tvInputFile.Name = "tvInputFile";
            this.tvInputFile.Size = new System.Drawing.Size(297, 367);
            this.tvInputFile.TabIndex = 2;
            this.tvInputFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvInputFile_RightMouseClick);
            this.tvInputFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvInputFile_DoubleMouseClick);
            // 
            // ctxFileEdit
            // 
            this.ctxFileEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddNewNodeValue,
            this.tsAddNewNode,
            this.tsRefreshFile,
            this.tsRenameParent,
            this.tsDeleteNode});
            this.ctxFileEdit.Name = "ctxFileEdit";
            this.ctxFileEdit.Size = new System.Drawing.Size(187, 114);
            this.ctxFileEdit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvInputFile_RightMouseClick);
            // 
            // tsAddNewNodeValue
            // 
            this.tsAddNewNodeValue.Name = "tsAddNewNodeValue";
            this.tsAddNewNodeValue.Size = new System.Drawing.Size(186, 22);
            this.tsAddNewNodeValue.Text = "Add New Node";
            // 
            // tsAddNewNode
            // 
            this.tsAddNewNode.Name = "tsAddNewNode";
            this.tsAddNewNode.Size = new System.Drawing.Size(186, 22);
            this.tsAddNewNode.Text = "Add Node Object";
            // 
            // tsRefreshFile
            // 
            this.tsRefreshFile.Name = "tsRefreshFile";
            this.tsRefreshFile.Size = new System.Drawing.Size(186, 22);
            this.tsRefreshFile.Text = "Refresh";
            // 
            // tsRenameParent
            // 
            this.tsRenameParent.Name = "tsRenameParent";
            this.tsRenameParent.Size = new System.Drawing.Size(186, 22);
            this.tsRenameParent.Text = "Rename Parent Node";
            // 
            // tsDeleteNode
            // 
            this.tsDeleteNode.Name = "tsDeleteNode";
            this.tsDeleteNode.Size = new System.Drawing.Size(186, 22);
            this.tsDeleteNode.Text = "Delete Node";
            // 
            // frmInputFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(893, 391);
            this.Controls.Add(this.tvInputFile);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInputFields";
            this.Text = "Edit Input Fields";
            ((System.ComponentModel.ISupportInitialize)(this.bsInputFields)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ctxFileEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bsInputFields;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInputFieldName;
        private System.Windows.Forms.Label label1;
        private JSONAPI.Controls.JSONAPIButton btnCancel;
        private JSONAPI.Controls.JSONAPIButton btnSaveInputField;
        private JSONAPI.Controls.JSONAPIButton btnValidate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvInputFile;
        private System.Windows.Forms.ContextMenuStrip ctxFileEdit;
        private System.Windows.Forms.ToolStripMenuItem tsAddNewNodeValue;
        private System.Windows.Forms.RichTextBox txtInputValue;
        private System.Windows.Forms.RichTextBox txtShowSample;
        private System.Windows.Forms.ListBox lstInputType;
        private JSONAPI.Controls.JSONAPIButton btnAddInputValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem tsRefreshFile;
        private System.Windows.Forms.ToolStripMenuItem tsAddNewNode;
        private JSONAPI.Controls.JSONAPIButton btnUpdateItem;
        private System.Windows.Forms.ToolStripMenuItem tsRenameParent;
        private System.Windows.Forms.ToolStripMenuItem tsDeleteNode;
    }
}