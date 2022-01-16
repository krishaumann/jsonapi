
namespace JSONAPI.Options
{
    partial class frmBuildExpectedResult
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
            this.btnVariableSave = new JSONAPI.Controls.JSONAPIButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.btnAddExpectedValue = new JSONAPI.Controls.JSONAPIButton();
            this.txtExpectedValue = new System.Windows.Forms.RichTextBox();
            this.btnValidateExpectedResult = new JSONAPI.Controls.JSONAPIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lstExpectedType = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVariableSave
            // 
            this.btnVariableSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnVariableSave.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnVariableSave.Location = new System.Drawing.Point(455, 161);
            this.btnVariableSave.Name = "btnVariableSave";
            this.btnVariableSave.Size = new System.Drawing.Size(81, 32);
            this.btnVariableSave.TabIndex = 6;
            this.btnVariableSave.Text = "Save";
            this.btnVariableSave.UseVisualStyleBackColor = false;
            this.btnVariableSave.Click += new System.EventHandler(this.btnVariableSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFieldName);
            this.groupBox1.Controls.Add(this.btnAddExpectedValue);
            this.groupBox1.Controls.Add(this.txtExpectedValue);
            this.groupBox1.Controls.Add(this.btnValidateExpectedResult);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnVariableSave);
            this.groupBox1.Controls.Add(this.lstExpectedType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 205);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Field Name";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Enabled = false;
            this.txtFieldName.Location = new System.Drawing.Point(199, 37);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(337, 23);
            this.txtFieldName.TabIndex = 20;
            // 
            // btnAddExpectedValue
            // 
            this.btnAddExpectedValue.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddExpectedValue.Location = new System.Drawing.Point(145, 46);
            this.btnAddExpectedValue.Name = "btnAddExpectedValue";
            this.btnAddExpectedValue.Size = new System.Drawing.Size(26, 23);
            this.btnAddExpectedValue.TabIndex = 17;
            this.btnAddExpectedValue.Text = ">";
            this.btnAddExpectedValue.UseVisualStyleBackColor = false;
            this.btnAddExpectedValue.Click += new System.EventHandler(this.btnAddExpectedValue_Click);
            // 
            // txtExpectedValue
            // 
            this.txtExpectedValue.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtExpectedValue.Location = new System.Drawing.Point(199, 86);
            this.txtExpectedValue.Name = "txtExpectedValue";
            this.txtExpectedValue.Size = new System.Drawing.Size(337, 69);
            this.txtExpectedValue.TabIndex = 16;
            this.txtExpectedValue.Text = "";
            // 
            // btnValidateExpectedResult
            // 
            this.btnValidateExpectedResult.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnValidateExpectedResult.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValidateExpectedResult.Location = new System.Drawing.Point(199, 161);
            this.btnValidateExpectedResult.Name = "btnValidateExpectedResult";
            this.btnValidateExpectedResult.Size = new System.Drawing.Size(75, 30);
            this.btnValidateExpectedResult.TabIndex = 15;
            this.btnValidateExpectedResult.Text = "Validate";
            this.btnValidateExpectedResult.UseVisualStyleBackColor = false;
            this.btnValidateExpectedResult.Click += new System.EventHandler(this.btnValidateExpectedResult_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Value";
            // 
            // lstExpectedType
            // 
            this.lstExpectedType.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstExpectedType.FormattingEnabled = true;
            this.lstExpectedType.ItemHeight = 16;
            this.lstExpectedType.Items.AddRange(new object[] {
            "Variable",
            "IsNumber",
            "IsDate",
            "IsText",
            "IsGUID",
            "Range",
            "Static Value"});
            this.lstExpectedType.Location = new System.Drawing.Point(6, 37);
            this.lstExpectedType.Name = "lstExpectedType";
            this.lstExpectedType.Size = new System.Drawing.Size(120, 84);
            this.lstExpectedType.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type";
            // 
            // frmBuildExpectedResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 219);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBuildExpectedResult";
            this.Text = "Build Expected Result";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private JSONAPI.Controls.JSONAPIButton btnVariableSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstExpectedType;
        private JSONAPI.Controls.JSONAPIButton btnAddExpectedValue;
        private System.Windows.Forms.RichTextBox txtExpectedValue;
        private JSONAPI.Controls.JSONAPIButton btnValidateExpectedResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFieldName;
    }
}