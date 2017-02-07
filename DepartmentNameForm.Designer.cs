namespace PersonalRU
{
    partial class DepartmentNameForm
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
            this.OK_button = new System.Windows.Forms.Button();
            this.ProsuctionSiteNmae_textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(54, 38);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 0;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // ProsuctionSiteNmae_textBox1
            // 
            this.ProsuctionSiteNmae_textBox1.Location = new System.Drawing.Point(116, 12);
            this.ProsuctionSiteNmae_textBox1.Name = "ProsuctionSiteNmae_textBox1";
            this.ProsuctionSiteNmae_textBox1.Size = new System.Drawing.Size(144, 20);
            this.ProsuctionSiteNmae_textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название отдела:";
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(135, 38);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(75, 23);
            this.Cancel_butt.TabIndex = 4;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // DepartmentNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 72);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProsuctionSiteNmae_textBox1);
            this.Controls.Add(this.OK_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DepartmentNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Иземнить название отдела";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.TextBox ProsuctionSiteNmae_textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_butt;
    }
}