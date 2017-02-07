namespace PersonalRU
{
    partial class CreateVacancyMenu
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
            this.newVac_base_Person_rbutt = new System.Windows.Forms.RadioButton();
            this.newVac_n_Person_rbutt = new System.Windows.Forms.RadioButton();
            this.OK_butt = new System.Windows.Forms.Button();
            this.Cancel_butt = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ProductionSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // newVac_base_Person_rbutt
            // 
            this.newVac_base_Person_rbutt.AutoSize = true;
            this.newVac_base_Person_rbutt.Location = new System.Drawing.Point(12, 35);
            this.newVac_base_Person_rbutt.Name = "newVac_base_Person_rbutt";
            this.newVac_base_Person_rbutt.Size = new System.Drawing.Size(330, 17);
            this.newVac_base_Person_rbutt.TabIndex = 0;
            this.newVac_base_Person_rbutt.Text = "Создать новую должность на основе выбранной должности";
            this.newVac_base_Person_rbutt.UseVisualStyleBackColor = true;
            this.newVac_base_Person_rbutt.CheckedChanged += new System.EventHandler(this.newVac_base_Person_rbutt_CheckedChanged);
            // 
            // newVac_n_Person_rbutt
            // 
            this.newVac_n_Person_rbutt.AutoSize = true;
            this.newVac_n_Person_rbutt.Checked = true;
            this.newVac_n_Person_rbutt.Location = new System.Drawing.Point(12, 12);
            this.newVac_n_Person_rbutt.Name = "newVac_n_Person_rbutt";
            this.newVac_n_Person_rbutt.Size = new System.Drawing.Size(162, 17);
            this.newVac_n_Person_rbutt.TabIndex = 1;
            this.newVac_n_Person_rbutt.TabStop = true;
            this.newVac_n_Person_rbutt.Text = "Создать новую должность ";
            this.newVac_n_Person_rbutt.UseVisualStyleBackColor = true;
            // 
            // OK_butt
            // 
            this.OK_butt.Location = new System.Drawing.Point(12, 308);
            this.OK_butt.Name = "OK_butt";
            this.OK_butt.Size = new System.Drawing.Size(75, 23);
            this.OK_butt.TabIndex = 2;
            this.OK_butt.Text = "OK";
            this.OK_butt.UseVisualStyleBackColor = true;
            this.OK_butt.Click += new System.EventHandler(this.OK_butt_Click);
            // 
            // Cancel_butt
            // 
            this.Cancel_butt.Location = new System.Drawing.Point(93, 308);
            this.Cancel_butt.Name = "Cancel_butt";
            this.Cancel_butt.Size = new System.Drawing.Size(75, 23);
            this.Cancel_butt.TabIndex = 3;
            this.Cancel_butt.Text = "Отмена";
            this.Cancel_butt.UseVisualStyleBackColor = true;
            this.Cancel_butt.Click += new System.EventHandler(this.Cancel_butt_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductionSite,
            this.JTitle,
            this.FIO,
            this.ID});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(7, 58);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(503, 244);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // ProductionSite
            // 
            this.ProductionSite.Frozen = true;
            this.ProductionSite.HeaderText = "Участок производства";
            this.ProductionSite.Name = "ProductionSite";
            this.ProductionSite.ReadOnly = true;
            this.ProductionSite.Width = 150;
            // 
            // JTitle
            // 
            this.JTitle.Frozen = true;
            this.JTitle.HeaderText = "Должность";
            this.JTitle.Name = "JTitle";
            this.JTitle.ReadOnly = true;
            this.JTitle.Width = 150;
            // 
            // FIO
            // 
            this.FIO.HeaderText = "ФИО";
            this.FIO.Name = "FIO";
            this.FIO.Width = 150;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // CreateVacancyMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 338);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Cancel_butt);
            this.Controls.Add(this.OK_butt);
            this.Controls.Add(this.newVac_n_Person_rbutt);
            this.Controls.Add(this.newVac_base_Person_rbutt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateVacancyMenu";
            this.Text = "Создание вакансии";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton newVac_base_Person_rbutt;
        private System.Windows.Forms.RadioButton newVac_n_Person_rbutt;
        private System.Windows.Forms.Button OK_butt;
        private System.Windows.Forms.Button Cancel_butt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductionSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn JTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}