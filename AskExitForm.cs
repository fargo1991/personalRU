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
    public partial class AskExitForm : Form
    {
        public bool isCanceled = false;
        public bool isOKed = false;
        public AskExitForm(string ProjectName)
        {
            InitializeComponent();
            label2.Text = "Проект \"" + ProjectName + "\" был изменен";
        }
        private void OKbutton_Click(object sender, EventArgs e)
        {
            this.isCanceled = false;
            this.isOKed = true;
            this.Close();
        }
        private void NObutton_Click(object sender, EventArgs e)
        {
            this.isCanceled = false;
            this.isOKed = false;
            this.Close();
        }
        private void CancelButt_Click(object sender, EventArgs e)
        {
            this.isCanceled = true;
            this.Close();
        }
    }
}
