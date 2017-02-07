namespace PersonalRU
{
    partial class NewProjectForm
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProjectName_textBox = new System.Windows.Forms.TextBox();
            this.ProjectPath_textbox = new System.Windows.Forms.TextBox();
            this.Ok_butt = new System.Windows.Forms.Button();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.BrowseButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Выберите папку, где будет храниться ваш проект.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя проекта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Путь к папке проекта";
            // 
            // ProjectName_textBox
            // 
            this.ProjectName_textBox.Location = new System.Drawing.Point(135, 17);
            this.ProjectName_textBox.Name = "ProjectName_textBox";
            this.ProjectName_textBox.Size = new System.Drawing.Size(169, 20);
            this.ProjectName_textBox.TabIndex = 2;
            this.ProjectName_textBox.Text = "ProjectName";
            // 
            // ProjectPath_textbox
            // 
            this.ProjectPath_textbox.Location = new System.Drawing.Point(135, 48);
            this.ProjectPath_textbox.Name = "ProjectPath_textbox";
            this.ProjectPath_textbox.Size = new System.Drawing.Size(169, 20);
            this.ProjectPath_textbox.TabIndex = 3;
            // 
            // Ok_butt
            // 
            this.Ok_butt.Location = new System.Drawing.Point(229, 74);
            this.Ok_butt.Name = "Ok_butt";
            this.Ok_butt.Size = new System.Drawing.Size(75, 23);
            this.Ok_butt.TabIndex = 4;
            this.Ok_butt.Text = "OK";
            this.Ok_butt.UseVisualStyleBackColor = true;
            this.Ok_butt.Click += new System.EventHandler(this.Ok_butt_Click);
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(310, 74);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(75, 23);
            this.Cancel_butt.TabIndex = 5;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // BrowseButt
            // 
            this.BrowseButt.Location = new System.Drawing.Point(310, 46);
            this.BrowseButt.Name = "BrowseButt";
            this.BrowseButt.Size = new System.Drawing.Size(75, 23);
            this.BrowseButt.TabIndex = 6;
            this.BrowseButt.Text = "Обзор...";
            this.BrowseButt.UseVisualStyleBackColor = true;
            this.BrowseButt.Click += new System.EventHandler(this.BrowseButt_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 104);
            this.Controls.Add(this.BrowseButt);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.Ok_butt);
            this.Controls.Add(this.ProjectPath_textbox);
            this.Controls.Add(this.ProjectName_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новый проект";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProjectName_textBox;
        private System.Windows.Forms.TextBox ProjectPath_textbox;
        private System.Windows.Forms.Button Ok_butt;
        private System.Windows.Forms.Button Cancel_butt;
        private System.Windows.Forms.Button BrowseButt;
    }
}