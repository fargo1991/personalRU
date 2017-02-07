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
    public partial class AskDeleteVacancyForm : Form
    {
        public bool isDel = false;
        public bool isDeleteUnit = false;
        public AskDeleteVacancyForm(string msg)
        {
            InitializeComponent();
            
            msg_label.Text = msg;
        }

        private void AskDeleteVacancyForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.isDel = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.isDel = false;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) isDeleteUnit = true;
            else isDeleteUnit = false;
        }
    }
}
