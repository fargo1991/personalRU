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
    public partial class DepartmentNameForm : Form
    {
        public string ProductionSite = "";
        public bool isOKed = false;
        public DepartmentNameForm(string ProductionSiteName)
        {
            InitializeComponent();
            this.ProductionSite = ProductionSiteName;
            this.ProsuctionSiteNmae_textBox1.Text = ProductionSiteName;
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            this.ProductionSite = this.ProsuctionSiteNmae_textBox1.Text;
            this.isOKed = true;
            this.Close();
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            this.isOKed = false;
            this.Close();
        }
    }
}
