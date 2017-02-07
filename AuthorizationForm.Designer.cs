namespace PersonalRU
{
    partial class AuthorizationForm
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
            this.AF_Login_button = new System.Windows.Forms.Button();
            this.AF_cancel_button = new System.Windows.Forms.Button();
            this.login_TextBox = new System.Windows.Forms.TextBox();
            this.pswd_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AF_cut_down_button = new System.Windows.Forms.PictureBox();
            this.AF_ExitButton = new System.Windows.Forms.PictureBox();
            this.AF_TopBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_cut_down_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_ExitButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_TopBar)).BeginInit();
            this.SuspendLayout();
            // 
            // AF_Login_button
            // 
            this.AF_Login_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.AF_Login_button.FlatAppearance.BorderSize = 0;
            this.AF_Login_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AF_Login_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AF_Login_button.ForeColor = System.Drawing.SystemColors.Control;
            this.AF_Login_button.Location = new System.Drawing.Point(114, 115);
            this.AF_Login_button.Name = "AF_Login_button";
            this.AF_Login_button.Size = new System.Drawing.Size(76, 27);
            this.AF_Login_button.TabIndex = 4;
            this.AF_Login_button.Text = "Войти";
            this.AF_Login_button.UseVisualStyleBackColor = false;
            this.AF_Login_button.Click += new System.EventHandler(this.AF_Login_button_Click);
            // 
            // AF_cancel_button
            // 
            this.AF_cancel_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.AF_cancel_button.FlatAppearance.BorderSize = 0;
            this.AF_cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AF_cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AF_cancel_button.ForeColor = System.Drawing.SystemColors.Control;
            this.AF_cancel_button.Location = new System.Drawing.Point(196, 115);
            this.AF_cancel_button.Name = "AF_cancel_button";
            this.AF_cancel_button.Size = new System.Drawing.Size(73, 27);
            this.AF_cancel_button.TabIndex = 5;
            this.AF_cancel_button.Text = "Отмена";
            this.AF_cancel_button.UseVisualStyleBackColor = false;
            this.AF_cancel_button.Click += new System.EventHandler(this.AF_cancel_button_Click);
            // 
            // login_TextBox
            // 
            this.login_TextBox.Location = new System.Drawing.Point(125, 47);
            this.login_TextBox.Name = "login_TextBox";
            this.login_TextBox.Size = new System.Drawing.Size(121, 20);
            this.login_TextBox.TabIndex = 6;
            this.login_TextBox.Text = "login";
            // 
            // pswd_TextBox
            // 
            this.pswd_TextBox.Location = new System.Drawing.Point(125, 73);
            this.pswd_TextBox.Name = "pswd_TextBox";
            this.pswd_TextBox.Size = new System.Drawing.Size(121, 20);
            this.pswd_TextBox.TabIndex = 7;
            this.pswd_TextBox.Text = "pswd";
            this.pswd_TextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Пароль";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(258, 54);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(91, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Забыли пароль?";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(258, 80);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(72, 13);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Регистрация";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PersonalRU.Properties.Resources.ПерсоналRU;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 25);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // AF_cut_down_button
            // 
            this.AF_cut_down_button.BackColor = System.Drawing.Color.SteelBlue;
            this.AF_cut_down_button.Image = global::PersonalRU.Properties.Resources.cut_down_def;
            this.AF_cut_down_button.Location = new System.Drawing.Point(353, 0);
            this.AF_cut_down_button.Name = "AF_cut_down_button";
            this.AF_cut_down_button.Size = new System.Drawing.Size(21, 25);
            this.AF_cut_down_button.TabIndex = 3;
            this.AF_cut_down_button.TabStop = false;
            this.AF_cut_down_button.Click += new System.EventHandler(this.AF_cut_down_button_Click);
            this.AF_cut_down_button.MouseLeave += new System.EventHandler(this.AF_cut_down_button_MouseLeave);
            this.AF_cut_down_button.MouseHover += new System.EventHandler(this.AF_cut_down_button_MouseHover);
            // 
            // AF_ExitButton
            // 
            this.AF_ExitButton.BackColor = System.Drawing.Color.SteelBlue;
            this.AF_ExitButton.Image = global::PersonalRU.Properties.Resources.exit_btn_def;
            this.AF_ExitButton.Location = new System.Drawing.Point(374, 0);
            this.AF_ExitButton.Name = "AF_ExitButton";
            this.AF_ExitButton.Size = new System.Drawing.Size(21, 25);
            this.AF_ExitButton.TabIndex = 2;
            this.AF_ExitButton.TabStop = false;
            this.AF_ExitButton.Click += new System.EventHandler(this.AF_ExitButton_Click);
            this.AF_ExitButton.MouseLeave += new System.EventHandler(this.AF_ExitButton_MouseLeave);
            this.AF_ExitButton.MouseHover += new System.EventHandler(this.AF_ExitButton_MouseHover);
            // 
            // AF_TopBar
            // 
            this.AF_TopBar.BackColor = System.Drawing.Color.RoyalBlue;
            this.AF_TopBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.AF_TopBar.Location = new System.Drawing.Point(0, 0);
            this.AF_TopBar.Name = "AF_TopBar";
            this.AF_TopBar.Size = new System.Drawing.Size(353, 25);
            this.AF_TopBar.TabIndex = 1;
            this.AF_TopBar.TabStop = false;
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(395, 164);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pswd_TextBox);
            this.Controls.Add(this.login_TextBox);
            this.Controls.Add(this.AF_cancel_button);
            this.Controls.Add(this.AF_Login_button);
            this.Controls.Add(this.AF_cut_down_button);
            this.Controls.Add(this.AF_ExitButton);
            this.Controls.Add(this.AF_TopBar);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AuthorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_cut_down_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_ExitButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AF_TopBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AF_TopBar;
        private System.Windows.Forms.PictureBox AF_ExitButton;
        private System.Windows.Forms.PictureBox AF_cut_down_button;
        private System.Windows.Forms.Button AF_Login_button;
        private System.Windows.Forms.Button AF_cancel_button;
        private System.Windows.Forms.TextBox login_TextBox;
        private System.Windows.Forms.TextBox pswd_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

