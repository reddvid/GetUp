namespace GetUp
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.cbOptions = new System.Windows.Forms.ComboBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFonts = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Notify every:";
            // 
            // cbOptions
            // 
            this.cbOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOptions.FormattingEnabled = true;
            this.cbOptions.Items.AddRange(new object[] {
            "30 minutes",
            "hour",
            "1.5 hours",
            "2 hours",
            "3 hours",
            "test"});
            this.cbOptions.Location = new System.Drawing.Point(15, 25);
            this.cbOptions.Name = "cbOptions";
            this.cbOptions.Size = new System.Drawing.Size(227, 21);
            this.cbOptions.TabIndex = 1;
            this.cbOptions.SelectedIndexChanged += new System.EventHandler(this.CbOptions_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(115, 269);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(127, 23);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save and Close";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.lblPreview);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 122);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Gotham", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.Location = new System.Drawing.Point(48, 47);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(128, 43);
            this.lblPreview.TabIndex = 4;
            this.lblPreview.Text = "Get Up";
            this.lblPreview.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Font family:";
            // 
            // cbFonts
            // 
            this.cbFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFonts.FormattingEnabled = true;
            this.cbFonts.Items.AddRange(new object[] {
            "Segoe UI",
            "Montserrat",
            "Raleway"});
            this.cbFonts.Location = new System.Drawing.Point(15, 84);
            this.cbFonts.Name = "cbFonts";
            this.cbFonts.Size = new System.Drawing.Size(227, 21);
            this.cbFonts.TabIndex = 5;
            this.cbFonts.SelectedIndexChanged += new System.EventHandler(this.CbFonts_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 304);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbFonts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cbOptions);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOptions;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFonts;
        private System.Windows.Forms.Button button1;
    }
}