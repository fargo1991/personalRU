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
    public partial class DeleteUnitForm : Form
    {
        public bool isOKed = false;
        public bool isSaveVoidUnit = false;
        public DeleteUnitForm()
        {
            InitializeComponent();
        }
        private void OK_butt_Click(object sender, EventArgs e)
        {
            this.isOKed = true;
            this.isSaveVoidUnit = this.isSaveVoidUnit_checkBox.Checked;
            this.Close();
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            this.isOKed = false;
            this.Close();
        }

   
    }
}
