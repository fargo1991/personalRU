namespace PersonalRU
{
    partial class InitMenu
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
            this.OK_butt = new System.Windows.Forms.Button();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.openProj_rbutt = new System.Windows.Forms.RadioButton();
            this.newProj_rbutt = new System.Windows.Forms.RadioButton();
            this.NewProj_picbox = new System.Windows.Forms.PictureBox();
            this.OpenProj_picbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NewProj_picbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenProj_picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // OK_butt
            // 
            this.OK_butt.Location = new System.Drawing.Point(41, 127);
            this.OK_butt.Name = "OK_butt";
            this.OK_butt.Size = new System.Drawing.Size(72, 26);
            this.OK_butt.TabIndex = 1;
            this.OK_butt.Text = "OK";
            this.OK_butt.UseVisualStyleBackColor = true;
            this.OK_butt.Click += new System.EventHandler(this.OK_butt_Click);
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(119, 127);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(72, 26);
            this.Cancel_butt.TabIndex = 2;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // openProj_rbutt
            // 
            this.openProj_rbutt.AutoSize = true;
            this.openProj_rbutt.Location = new System.Drawing.Point(69, 85);
            this.openProj_rbutt.Name = "openProj_rbutt";
            this.openProj_rbutt.Size = new System.Drawing.Size(147, 17);
            this.openProj_rbutt.TabIndex = 4;
            this.openProj_rbutt.Text = "Открыть старый проект";
            this.openProj_rbutt.UseVisualStyleBackColor = true;
            // 
            // newProj_rbutt
            // 
            this.newProj_rbutt.AutoSize = true;
            this.newProj_rbutt.Checked = true;
            this.newProj_rbutt.Location = new System.Drawing.Point(69, 28);
            this.newProj_rbutt.Name = "newProj_rbutt";
            this.newProj_rbutt.Size = new System.Drawing.Size(140, 17);
            this.newProj_rbutt.TabIndex = 3;
            this.newProj_rbutt.TabStop = true;
            this.newProj_rbutt.Text = "Создать новый проект";
            this.newProj_rbutt.UseVisualStyleBackColor = true;
            // 
            // NewProj_picbox
            // 
            this.NewProj_picbox.Image = global::PersonalRU.Properties.Resources.new_project_icon;
            this.NewProj_picbox.Location = new System.Drawing.Point(13, 9);
            this.NewProj_picbox.Name = "NewProj_picbox";
            this.NewProj_picbox.Size = new System.Drawing.Size(50, 50);
            this.NewProj_picbox.TabIndex = 5;
            this.NewProj_picbox.TabStop = false;
            // 
            // OpenProj_picbox
            // 
            this.OpenProj_picbox.Image = global::PersonalRU.Properties.Resources.openProjectIcon;
            this.OpenProj_picbox.Location = new System.Drawing.Point(13, 65);
            this.OpenProj_picbox.Name = "OpenProj_picbox";
            this.OpenProj_picbox.Size = new System.Drawing.Size(50, 50);
            this.OpenProj_picbox.TabIndex = 6;
            this.OpenProj_picbox.TabStop = false;
            // 
            // InitMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(224, 164);
            this.Controls.Add(this.OpenProj_picbox);
            this.Controls.Add(this.NewProj_picbox);
            this.Controls.Add(this.openProj_rbutt);
            this.Controls.Add(this.newProj_rbutt);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.OK_butt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InitMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Начало работы";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InitMenu_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.NewProj_picbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenProj_picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK_butt;
        private System.Windows.Forms.Button Cancel_butt;
        private System.Windows.Forms.RadioButton openProj_rbutt;
        private System.Windows.Forms.RadioButton newProj_rbutt;
        private System.Windows.Forms.PictureBox NewProj_picbox;
        private System.Windows.Forms.PictureBox OpenProj_picbox;
    }
}