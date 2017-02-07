using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PersonalRU
{
    class AddButton
    {
        ReqBlockList RBListClass;
        RequirBlock parentClass;
        VacancyForm parent;
        Panel parentPanel;
        private TextBox textbox;
        private Point Location = new Point();
        private Button buttt = new Button();

        private int Width;
        private int Height;

        private bool isChange;
        private string text;

        public AddButton(ReqBlockList RBList, VacancyForm parent, Panel parentPanel, RequirBlock parentClass, Point Location, bool isChange)
        {
            this.RBListClass = RBList;
            this.parentClass = parentClass;
            this.parent = parent;
            this.parentPanel = parentPanel;
            this.Location = Location;
            this.isChange = isChange;

            this.Width = 100;
            this.Height = 21;

            this.textbox = new TextBox();
            this.textbox.Multiline = true;
            this.textbox.TextChanged += new EventHandler(textbox_text_changed);
            this.buttt.Click += new EventHandler(butt_Click);
        }
        public void refresh()
        {
            
            if (this.parentPanel.Controls.Contains(this.textbox)) this.parentPanel.Controls.Remove(this.textbox);
            this.textbox.Location = new Point(this.Location.X,this.Location.Y -this.parentPanel.VerticalScroll.Value);
            this.textbox.BorderStyle = BorderStyle.FixedSingle;
            this.textbox.Font = parentPanel.Font;
            this.textbox.Width = this.Width;
            this.textbox.Height = this.Height;
            this.parentPanel.Controls.Add(this.textbox);

            if (this.parentPanel.Controls.Contains(this.buttt)) this.parentPanel.Controls.Remove(this.buttt);
            this.buttt.Location = new Point(this.Location.X + this.textbox.Width + 5, this.Location.Y - this.parentPanel.VerticalScroll.Value);
            this.buttt.AutoSize = true;
            this.buttt.Text = "Добавить";
            this.buttt.Cursor = Cursors.Hand;
            this.buttt.FlatStyle = FlatStyle.Flat;
            this.buttt.BackColor = Color.SteelBlue;
            this.buttt.ForeColor = Color.White;
            this.parentPanel.Controls.Add(this.buttt);
        }

        private void textbox_text_changed(object sender, EventArgs e)
        {
            int oldHeight = ((TextBox)sender).Height;
            int oldWidth = ((TextBox)sender).Width;

            this.parent.label1.Font = ((TextBox)sender).Font;
            this.parent.label1.Text = ((TextBox)sender).Text;
            this.parent.label1.AutoSize = true;
            this.parent.label1.MaximumSize = new Size(300, 0);

            if (((TextBox)sender).Height < 100)
            {
                int caretPosition = ((TextBox)sender).SelectionStart;
                ((TextBox)sender).Size = new Size(this.parent.label1.Width +7, this.parent.label1.Height + 9);
                ((TextBox)sender).SelectionStart = 0;
                ((TextBox)sender).ScrollToCaret();
                ((TextBox)sender).SelectionStart = caretPosition;
            }
            if (oldHeight != ((TextBox)sender).Height)
            {

                this.textbox.Height = this.Height = ((TextBox)sender).Height;
                this.textbox.Width = this.Width = ((TextBox)sender).Width;
                this.text = textbox.Text;
                this.refresh();
                this.RBListClass.refresh();
            }
            if (oldWidth != ((TextBox)sender).Width)
            {
                this.text = textbox.Text;
                this.textbox.Width = this.Width = ((TextBox)sender).Width;
                this.refresh();
                //this.RBListClass.refresh();
            }
            this.textbox.Focus();
        }
        private void butt_Click(object sender, EventArgs e)
        {
            this.parentClass.addItem(this.text);
            this.text = "";
            this.textbox.Text = "";
            this.textbox.Width = this.Width = 100;
            this.textbox.Height = this.Height = 21;
            //this.Location = new Point(this.Location.X, this.Location.Y + this.textbox.Height + 5);
            refresh();
        }
        public void setLocation(Point Location) { this.Location = Location; }
        public void delete() { if (this.parentPanel.Controls.Contains(this.textbox)) this.parentPanel.Controls.Remove(this.textbox); }
        public int getHeight() {
            if (parentClass.isList) return this.Height;
            else return 0;
        }
    }
}
