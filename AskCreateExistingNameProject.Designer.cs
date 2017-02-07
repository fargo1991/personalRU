namespace PersonalRU
{
    partial class AskCreateExistingNameProject
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OK_butt = new System.Windows.Forms.Button();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 72);
            this.label1.MaximumSize = new System.Drawing.Size(300, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ВНИМАНИЕ! Данные старого проекта будут утеряны!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 25);
            this.label2.MaximumSize = new System.Drawing.Size(300, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "     Проект с таким именем уже существует. Вы все равно хотите создать проект с э" +
    "тим именем?";
            // 
            // OK_butt
            // 
            this.OK_butt.Location = new System.Drawing.Point(93, 99);
            this.OK_butt.Name = "OK_butt";
            this.OK_butt.Size = new System.Drawing.Size(75, 23);
            this.OK_butt.TabIndex = 2;
            this.OK_butt.Text = "OK";
            this.OK_butt.UseVisualStyleBackColor = true;
            this.OK_butt.Click += new System.EventHandler(this.OK_butt_Click);
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(174, 99);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(75, 23);
            this.Cancel_butt.TabIndex = 3;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PersonalRU.Properties.Resources.Alert;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // AskCreateExistingNameProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 134);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.OK_butt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AskCreateExistingNameProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заменить проект";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OK_butt;
        private System.Windows.Forms.Button Cancel_butt;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}