namespace PersonalRU
{
    partial class PhotoDesignForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OK_Butt = new System.Windows.Forms.Button();
            this.Cancel_Butt = new System.Windows.Forms.Button();
            this.BrowseButt = new System.Windows.Forms.Button();
            this.FPath_textBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(341, 340);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseCaptureChanged += new System.EventHandler(this.pictureBox1_MouseCaptureChanged);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // OK_Butt
            // 
            this.OK_Butt.Location = new System.Drawing.Point(231, 8);
            this.OK_Butt.Name = "OK_Butt";
            this.OK_Butt.Size = new System.Drawing.Size(58, 24);
            this.OK_Butt.TabIndex = 1;
            this.OK_Butt.Text = "OK";
            this.OK_Butt.UseVisualStyleBackColor = true;
            this.OK_Butt.Click += new System.EventHandler(this.OK_Butt_Click);
            // 
            // Cancel_Butt
            // 
            this.Cancel_Butt.Location = new System.Drawing.Point(295, 8);
            this.Cancel_Butt.Name = "Cancel_Butt";
            this.Cancel_Butt.Size = new System.Drawing.Size(58, 24);
            this.Cancel_Butt.TabIndex = 2;
            this.Cancel_Butt.Text = "Отмена";
            this.Cancel_Butt.UseVisualStyleBackColor = true;
            this.Cancel_Butt.Click += new System.EventHandler(this.Cancel_Butt_Click);
            // 
            // BrowseButt
            // 
            this.BrowseButt.Location = new System.Drawing.Point(12, 11);
            this.BrowseButt.Name = "BrowseButt";
            this.BrowseButt.Size = new System.Drawing.Size(58, 24);
            this.BrowseButt.TabIndex = 3;
            this.BrowseButt.Text = "Обзор...";
            this.BrowseButt.UseVisualStyleBackColor = true;
            this.BrowseButt.Click += new System.EventHandler(this.BrowseButt_Click);
            // 
            // FPath_textBox
            // 
            this.FPath_textBox.Location = new System.Drawing.Point(76, 12);
            this.FPath_textBox.Name = "FPath_textBox";
            this.FPath_textBox.Size = new System.Drawing.Size(149, 20);
            this.FPath_textBox.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PhotoDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 426);
            this.Controls.Add(this.FPath_textBox);
            this.Controls.Add(this.BrowseButt);
            this.Controls.Add(this.Cancel_Butt);
            this.Controls.Add(this.OK_Butt);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PhotoDesignForm";
            this.Text = "Редактировать фото";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button OK_Butt;
        private System.Windows.Forms.Button Cancel_Butt;
        private System.Windows.Forms.Button BrowseButt;
        private System.Windows.Forms.TextBox FPath_textBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}