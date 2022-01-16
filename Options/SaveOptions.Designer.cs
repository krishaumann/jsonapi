
namespace JSONAPI.Options
{
    partial class frmSaveOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveOptions));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveOptions = new JSONAPI.Controls.JSONAPIButton();
            this.cmbSaveOptions = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveOptions);
            this.groupBox1.Controls.Add(this.cmbSaveOptions);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save Items:";
            // 
            // btnSaveOptions
            // 
            this.btnSaveOptions.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSaveOptions.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveOptions.Location = new System.Drawing.Point(54, 53);
            this.btnSaveOptions.Name = "btnSaveOptions";
            this.btnSaveOptions.Size = new System.Drawing.Size(75, 28);
            this.btnSaveOptions.TabIndex = 1;
            this.btnSaveOptions.Text = "Save Options";
            this.btnSaveOptions.UseVisualStyleBackColor = false;
            this.btnSaveOptions.Click += new System.EventHandler(this.btnSaveOptions_Click);
            // 
            // cmbSaveOptions
            // 
            this.cmbSaveOptions.Font = new System.Drawing.Font("Yu Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbSaveOptions.FormattingEnabled = true;
            this.cmbSaveOptions.Items.AddRange(new object[] {
            "Once-off",
            "Variable",
            "Both"});
            this.cmbSaveOptions.Location = new System.Drawing.Point(7, 23);
            this.cmbSaveOptions.Name = "cmbSaveOptions";
            this.cmbSaveOptions.Size = new System.Drawing.Size(187, 24);
            this.cmbSaveOptions.TabIndex = 0;
            // 
            // frmSaveOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(206, 95);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSaveOptions";
            this.Text = "Save Options";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private JSONAPI.Controls.JSONAPIButton btnSaveOptions;
        private System.Windows.Forms.ComboBox cmbSaveOptions;
    }
}