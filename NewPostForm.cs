using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalRU
{
    public partial class NewPostForm : Form
    {
        public bool isCreateVacancy = false;
        public string JobTitle = "";
        public string Department = "";
        public bool isOKed = false;
        public NewPostForm()
        {
            InitializeComponent();
        }
        private void OKbutt_Click(object sender, EventArgs e)
        {
            if ((JobTitle_textBox.Text.Length == 0) || (Department_textbox.Text.Length == 0))
            { MessageBox.Show("Заполните все поля!"); return; }
            else
            {
                this.JobTitle = this.JobTitle_textBox.Text;
                this.Department = this.Department_textbox.Text;
                this.isCreateVacancy = isCreateVacancy_checkBox.Checked;
                this.isOKed = true;
                this.Close();
            }
        }
        private void CancelButt_Click(object sender, EventArgs e)
        {
            this.isOKed = false;
            this.Close();
        }

    }
}
