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
    public partial class AskCreateExistingNameProject : Form
    {
        public bool IsOKed = false;
        public AskCreateExistingNameProject()
        {
            InitializeComponent();
        }
        private void OK_butt_Click(object sender, EventArgs e)
        {
            IsOKed = true;
            this.Close();
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            IsOKed = false;
            this.Close();
        }
    }
}
