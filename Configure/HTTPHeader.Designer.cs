
namespace JSONAPI.Configure
{
    partial class frmHttpHeader
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHttpHeader));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAuthorization = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCustomAttr = new System.Windows.Forms.TextBox();
            this.btnAttributeAdd = new JSONAPI.Controls.JSONAPIButton();
            this.txtAttrValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHttpAttrs = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbHttpType = new System.Windows.Forms.ComboBox();
            this.btnSaveAttributes = new JSONAPI.Controls.JSONAPIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgHeaderElements = new System.Windows.Forms.DataGridView();
            this.colAttrType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxGridOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRemoveRow = new System.Windows.Forms.ToolStripMenuItem();
            this.colAttrValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsHttpHeader = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHeaderElements)).BeginInit();
            this.ctxGridOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsHttpHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgHeaderElements);
            this.splitContainer1.Size = new System.Drawing.Size(990, 600);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAuthorization);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmbHttpType);
            this.groupBox1.Controls.Add(this.btnSaveAttributes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtURL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(990, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTTP Header Options";
            // 
            // cmbAuthorization
            // 
            this.cmbAuthorization.FormattingEnabled = true;
            this.cmbAuthorization.Items.AddRange(new object[] {
            "Basic Auth",
            "Bearer Token",
            "API Key",
            "Digest Auth",
            "OAuth 2.0",
            "Hawk Authentication",
            "AWS Signature",
            "None"});
            this.cmbAuthorization.Location = new System.Drawing.Point(712, 75);
            this.cmbAuthorization.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAuthorization.Name = "cmbAuthorization";
            this.cmbAuthorization.Size = new System.Drawing.Size(266, 28);
            this.cmbAuthorization.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(615, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Authorization";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCustomAttr);
            this.groupBox2.Controls.Add(this.btnAttributeAdd);
            this.groupBox2.Controls.Add(this.txtAttrValue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbHttpAttrs);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(5, 115);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(881, 113);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other HTTP Attributes";
            // 
            // txtCustomAttr
            // 
            this.txtCustomAttr.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCustomAttr.Location = new System.Drawing.Point(79, 29);
            this.txtCustomAttr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomAttr.Name = "txtCustomAttr";
            this.txtCustomAttr.Size = new System.Drawing.Size(691, 32);
            this.txtCustomAttr.TabIndex = 6;
            this.txtCustomAttr.Visible = false;
            // 
            // btnAttributeAdd
            // 
            this.btnAttributeAdd.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAttributeAdd.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAttributeAdd.Location = new System.Drawing.Point(777, 68);
            this.btnAttributeAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAttributeAdd.Name = "btnAttributeAdd";
            this.btnAttributeAdd.Size = new System.Drawing.Size(86, 43);
            this.btnAttributeAdd.TabIndex = 4;
            this.btnAttributeAdd.Text = "Add";
            this.btnAttributeAdd.UseVisualStyleBackColor = false;
            // 
            // txtAttrValue
            // 
            this.txtAttrValue.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAttrValue.Location = new System.Drawing.Point(79, 68);
            this.txtAttrValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAttrValue.Name = "txtAttrValue";
            this.txtAttrValue.Size = new System.Drawing.Size(691, 32);
            this.txtAttrValue.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Value";
            // 
            // cmbHttpAttrs
            // 
            this.cmbHttpAttrs.FormattingEnabled = true;
            this.cmbHttpAttrs.Items.AddRange(new object[] {
            "A-IM",
            "Accept",
            "Accept-Charset",
            "Accept-Encoding",
            "Accept-Language",
            "Accept-Datetime",
            "Access-Control-Request-Method",
            "Access-Control-Request-Headers",
            "Cache-Control",
            "Connection",
            "Content-Length",
            "Content-Type",
            "Cookie",
            "Date",
            "Expect",
            "Forwarded",
            "From",
            "Host",
            "If-Match",
            "If-Modified-Since",
            "If-None-Match",
            "If-Range",
            "If-Unmodified-Since",
            "Max-Forwards",
            "Origin",
            "Pragma",
            "Proxy-Authorization",
            "Range",
            "Referer",
            "TE",
            "User-Agent",
            "Upgrade",
            "Via",
            "Warning",
            "Other"});
            this.cmbHttpAttrs.Location = new System.Drawing.Point(79, 29);
            this.cmbHttpAttrs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbHttpAttrs.Name = "cmbHttpAttrs";
            this.cmbHttpAttrs.Size = new System.Drawing.Size(518, 28);
            this.cmbHttpAttrs.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(9, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Attribute";
            // 
            // cmbHttpType
            // 
            this.cmbHttpType.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbHttpType.FormattingEnabled = true;
            this.cmbHttpType.Items.AddRange(new object[] {
            "POST",
            "GET",
            "PUT",
            "PATCH",
            "DELETE"});
            this.cmbHttpType.Location = new System.Drawing.Point(53, 75);
            this.cmbHttpType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbHttpType.Name = "cmbHttpType";
            this.cmbHttpType.Size = new System.Drawing.Size(262, 28);
            this.cmbHttpType.TabIndex = 3;
            // 
            // btnSaveAttributes
            // 
            this.btnSaveAttributes.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSaveAttributes.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSaveAttributes.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveAttributes.Location = new System.Drawing.Point(893, 133);
            this.btnSaveAttributes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveAttributes.Name = "btnSaveAttributes";
            this.btnSaveAttributes.Size = new System.Drawing.Size(86, 43);
            this.btnSaveAttributes.TabIndex = 5;
            this.btnSaveAttributes.Text = "Save";
            this.btnSaveAttributes.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type";
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtURL.Location = new System.Drawing.Point(53, 36);
            this.txtURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(925, 32);
            this.txtURL.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // dgHeaderElements
            // 
            this.dgHeaderElements.AllowUserToAddRows = false;
            this.dgHeaderElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHeaderElements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAttrType,
            this.colAttrValue});
            this.dgHeaderElements.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgHeaderElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHeaderElements.Location = new System.Drawing.Point(0, 0);
            this.dgHeaderElements.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgHeaderElements.Name = "dgHeaderElements";
            this.dgHeaderElements.RowHeadersVisible = false;
            this.dgHeaderElements.RowHeadersWidth = 51;
            this.dgHeaderElements.RowTemplate.Height = 25;
            this.dgHeaderElements.Size = new System.Drawing.Size(990, 362);
            this.dgHeaderElements.TabIndex = 0;
            // 
            // colAttrType
            // 
            this.colAttrType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAttrType.ContextMenuStrip = this.ctxGridOptions;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.colAttrType.DefaultCellStyle = dataGridViewCellStyle1;
            this.colAttrType.HeaderText = "Attribute Type";
            this.colAttrType.MinimumWidth = 6;
            this.colAttrType.Name = "colAttrType";
            // 
            // ctxGridOptions
            // 
            this.ctxGridOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxGridOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRemoveRow});
            this.ctxGridOptions.Name = "ctxGridOptions";
            this.ctxGridOptions.Size = new System.Drawing.Size(166, 28);
            this.ctxGridOptions.Text = "Grid Options";
            // 
            // ctxRemoveRow
            // 
            this.ctxRemoveRow.Name = "ctxRemoveRow";
            this.ctxRemoveRow.Size = new System.Drawing.Size(165, 24);
            this.ctxRemoveRow.Text = "Remove Row";
            // 
            // colAttrValue
            // 
            this.colAttrValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAttrValue.ContextMenuStrip = this.ctxGridOptions;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Aquamarine;
            this.colAttrValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.colAttrValue.HeaderText = "AttributeValue";
            this.colAttrValue.MinimumWidth = 6;
            this.colAttrValue.Name = "colAttrValue";
            // 
            // frmHttpHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 600);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmHttpHeader";
            this.Text = "HTTP Header";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHttpHeader_Close);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHeaderElements)).EndInit();
            this.ctxGridOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsHttpHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCustomAttr;
        private JSONAPI.Controls.JSONAPIButton btnSaveAttributes;
        private JSONAPI.Controls.JSONAPIButton btnAttributeAdd;
        private System.Windows.Forms.TextBox txtAttrValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbHttpAttrs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbHttpType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgHeaderElements;
        private System.Windows.Forms.ContextMenuStrip ctxGridOptions;
        private System.Windows.Forms.ToolStripMenuItem ctxRemoveRow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAuthorization;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttrType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttrValue;
        private System.Windows.Forms.BindingSource bsHttpHeader;
    }
}