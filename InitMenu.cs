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
    public partial class InitMenu : Form
    {
        public bool IsOKed = false;
        public bool Choose = false; // 0 - newProj, 1 - openProj
        public InitMenu()
        {            
            InitializeComponent();
        }
        private void OK_butt_Click(object sender, EventArgs e)
        {
            if (newProj_rbutt.Checked) Choose = false;
            else Choose = true;
            IsOKed = true;
            this.Close();

        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            IsOKed = false;
            this.Close();
        }
        private void InitMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            //IsOKed = false;
        }
    }
}
