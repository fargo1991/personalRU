namespace PersonalRU
{
    partial class NewPostForm
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
            this.JobTitle_textBox = new System.Windows.Forms.TextBox();
            this.Department_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButt = new System.Windows.Forms.Button();
            this.OKbutt = new System.Windows.Forms.Button();
            this.isCreateVacancy_checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // JobTitle_textBox
            // 
            this.JobTitle_textBox.Location = new System.Drawing.Point(83, 12);
            this.JobTitle_textBox.Name = "JobTitle_textBox";
            this.JobTitle_textBox.Size = new System.Drawing.Size(129, 20);
            this.JobTitle_textBox.TabIndex = 0;
            // 
            // Department_textbox
            // 
            this.Department_textbox.Location = new System.Drawing.Point(83, 38);
            this.Department_textbox.Name = "Department_textbox";
            this.Department_textbox.Size = new System.Drawing.Size(129, 20);
            this.Department_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Должность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Отдел";
            // 
            // CancelButt
            // 
            this.CancelButt.Location = new System.Drawing.Point(137, 91);
            this.CancelButt.Name = "CancelButt";
            this.CancelButt.Size = new System.Drawing.Size(75, 23);
            this.CancelButt.TabIndex = 4;
            this.CancelButt.Text = "Отмена";
            this.CancelButt.UseVisualStyleBackColor = true;
            this.CancelButt.Click += new System.EventHandler(this.CancelButt_Click);
            // 
            // OKbutt
            // 
            this.OKbutt.Location = new System.Drawing.Point(56, 91);
            this.OKbutt.Name = "OKbutt";
            this.OKbutt.Size = new System.Drawing.Size(75, 23);
            this.OKbutt.TabIndex = 5;
            this.OKbutt.Text = "ОК";
            this.OKbutt.UseVisualStyleBackColor = true;
            this.OKbutt.Click += new System.EventHandler(this.OKbutt_Click);
            // 
            // isCreateVacancy_checkBox
            // 
            this.isCreateVacancy_checkBox.AutoSize = true;
            this.isCreateVacancy_checkBox.Location = new System.Drawing.Point(15, 68);
            this.isCreateVacancy_checkBox.Name = "isCreateVacancy_checkBox";
            this.isCreateVacancy_checkBox.Size = new System.Drawing.Size(121, 17);
            this.isCreateVacancy_checkBox.TabIndex = 6;
            this.isCreateVacancy_checkBox.Text = "Создать вакансию";
            this.isCreateVacancy_checkBox.UseVisualStyleBackColor = true;
            // 
            // NewPostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 126);
            this.Controls.Add(this.isCreateVacancy_checkBox);
            this.Controls.Add(this.OKbutt);
            this.Controls.Add(this.CancelButt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Department_textbox);
            this.Controls.Add(this.JobTitle_textBox);
            this.Name = "NewPostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая должность";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox JobTitle_textBox;
        private System.Windows.Forms.TextBox Department_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelButt;
        private System.Windows.Forms.Button OKbutt;
        private System.Windows.Forms.CheckBox isCreateVacancy_checkBox;
    }
}