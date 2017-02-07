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
    public partial class AskForm : Form
    {
        public bool IsOKed = false;
        public AskForm(string question)
        {
            InitializeComponent();
            initControls(question);
            this.pictureBox1.Hide();
        }
        public AskForm(string question, Bitmap bmp)
        {
            InitializeComponent();

            //Point new_loc = new Point(label1.Location.X - 50, label1.Location.Y);
            //Size newFormSize = new Size(this.Size.Width - 50, this.Size.Height);

            //this.label1.Location = new_loc;
            //this.Size = newFormSize;
            this.label1.Text = question;
            
            //initControls(question);
            this.pictureBox1.Image = bmp;
            //this.pictureBox1.Hide();
        }
        private void initControls(string question)
        {
            Point new_loc = new Point(label1.Location.X - 50, label1.Location.Y);
            Size newFormSize = new Size(this.Size.Width - 50, this.Size.Height);

            this.label1.Location = new_loc;
            this.Size = newFormSize;
            this.label1.Text = question;
        }
        private void OKButt_Click(object sender, EventArgs e)
        {
            IsOKed = true;
            this.Close();
        }
        private void CancelButt_Click(object sender, EventArgs e)
        {
            IsOKed = false;
            this.Close();
        }
    }
}