namespace PersonalRU
{
    partial class DeleteUnitForm
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
            this.isSaveVoidUnit_checkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OK_butt
            // 
            this.OK_butt.Location = new System.Drawing.Point(34, 74);
            this.OK_butt.Name = "OK_butt";
            this.OK_butt.Size = new System.Drawing.Size(75, 23);
            this.OK_butt.TabIndex = 0;
            this.OK_butt.Text = "OK";
            this.OK_butt.UseVisualStyleBackColor = true;
            this.OK_butt.Click += new System.EventHandler(this.OK_butt_Click);
            // 
            // isSaveVoidUnit_checkBox
            // 
            this.isSaveVoidUnit_checkBox.AutoSize = true;
            this.isSaveVoidUnit_checkBox.Location = new System.Drawing.Point(15, 47);
            this.isSaveVoidUnit_checkBox.Name = "isSaveVoidUnit_checkBox";
            this.isSaveVoidUnit_checkBox.Size = new System.Drawing.Size(225, 17);
            this.isSaveVoidUnit_checkBox.TabIndex = 1;
            this.isSaveVoidUnit_checkBox.Text = "Оставить незанятую должность в базе";
            this.isSaveVoidUnit_checkBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Удалить все данные о сотруднике из базы?";
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(115, 74);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(75, 23);
            this.Cancel_butt.TabIndex = 3;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // DeleteUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 109);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.isSaveVoidUnit_checkBox);
            this.Controls.Add(this.OK_butt);
            this.Name = "DeleteUnitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Удаление сотрудника";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK_butt;
        private System.Windows.Forms.CheckBox isSaveVoidUnit_checkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_butt;
    }
}