using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PersonalRU
{
    class Item
    {
        private Label main_label = new Label();
        public PictureBox deletePBox = new PictureBox();
        TextBox textbox = new TextBox();
        private Point Location = new Point();
        private string text = "";
        private Size MaximumSize = new Size();
        private int Width, Height;
        private ComboBox combo = new ComboBox();
        

        private VacancyForm parentForm;
        private Panel parentPanel;
        private ReqBlockList RBListClass;
        private RequirBlock RBClass;

        private int margin = 5;

        public Item(string text, Point Location, VacancyForm parentForm, Panel parentPanel, ReqBlockList RBListClass, RequirBlock RBClass, Size MaximumSize)
        {
            this.text = text;
            this.Location = Location;
            this.MaximumSize = MaximumSize;
            this.parentForm = parentForm;
            this.parentPanel = parentPanel;
            this.RBListClass = RBListClass;
            this.RBClass = RBClass;

            this.main_label.Font = parentPanel.Font;
            this.textbox.Font = parentPanel.Font;

            this.main_label.Padding = new Padding(4);
            this.main_label.AutoSize = true;
            main_label.MaximumSize = new Size(300, 0);

            if (!RBClass.isList) deletePBox.Height = 0;

            this.textbox.BorderStyle = BorderStyle.FixedSingle;

            this.main_label.Click += new EventHandler(this.main_label_Clicked);
            this.textbox.TextChanged += new EventHandler(this.textbox_textChanged);
            this.textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPressed);
            this.textbox.LostFocus += new EventHandler(this.textbox_lostFocus);
            this.deletePBox.Click += new EventHandler(this.deletePBox_Clicked);
            refresh();
        }
        public void setText(string text)
        {
            this.text = text;
            refresh();
        }
        public void refresh()
        {
            this.Width = calcWidth();
            this.Height = calcHeight();
            refreshMainLabel();
            if (RBClass.isList)refresh_deletePBox();
            if (this.RBClass.Title == "Уровень зарплаты:") refreshComboBox();
        }
        private void refreshComboBox()
        {
            this.combo.Location = new Point(this.main_label.Location.X + this.main_label.Width + this.margin, this.main_label.Location.Y);
            
            this.combo.Items.Clear();
            this.combo.Items.Add("руб.");
            this.combo.Items.Add("USD");
            this.combo.Items.Add("EUR");
            this.combo.Width = 50;

            this.parentPanel.Controls.Add(combo);

            if (this.parentPanel.Controls.Contains(this.deletePBox))
                this.parentPanel.Controls.Remove(this.deletePBox);

            this.deletePBox.Location = new Point(this.combo.Location.X + this.combo.Width + this.margin, this.main_label.Location.Y);
            this.deletePBox.Size = new Size(25, 25);
            this.deletePBox.Image = Properties.Resources.deleteItem;
            this.deletePBox.Visible = true;
            deletePBox.Cursor = Cursors.Hand;


            this.parentPanel.Controls.Add(this.deletePBox);
        }
        private void refreshMainLabel()
        {
            if (this.parentPanel.Controls.Contains(this.main_label)) this.parentPanel.Controls.Remove(main_label);
            this.main_label.Text = this.text;
            this.main_label.Location = this.Location;
            this.main_label.Width = this.Width;
            this.main_label.Height = this.Height;
            this.main_label.BackColor = Color.GhostWhite;
            this.main_label.BorderStyle = BorderStyle.FixedSingle;
            this.main_label.Cursor = Cursors.IBeam;

            if (this.isShowMainLabel) this.parentPanel.Controls.Add(main_label);
        }
        private void refresh_deletePBox()
        {
            if (this.parentPanel.Controls.Contains(this.deletePBox))
                this.parentPanel.Controls.Remove(this.deletePBox);

            this.deletePBox.Location = new Point(this.main_label.Location.X + this.main_label.Width + this.margin, this.main_label.Location.Y);
            this.deletePBox.Size = new Size(25, 25);
            this.deletePBox.Image = Properties.Resources.deleteItem;
            this.deletePBox.Visible = true;
            deletePBox.Cursor = Cursors.Hand;

            
            this.parentPanel.Controls.Add(this.deletePBox);
        }
        bool isShowMainLabel = true;
        private void main_label_Clicked(object sender, EventArgs e)
        {
            this.textbox.Location = new Point(this.main_label.Location.X+1,this.main_label.Location.Y+1);
            this.textbox.Height = this.Height + 7;
            this.textbox.Width = this.Width;
            this.textbox.Text = this.text;
            this.textbox.Multiline = true;
            this.textbox.Visible = true;
            this.parentPanel.Controls.Add(this.textbox);
            this.parentPanel.Controls.Remove(this.main_label);
            this.isShowMainLabel = false;
            this.textbox.SelectAll();
            this.textbox.Focus();
        }
        private void textbox_textChanged(object sender, EventArgs e)
        {
            int oldHeight = ((TextBox)sender).Height;
            int oldWidth = ((TextBox)sender).Width;

            this.parentForm.label1.Font = ((TextBox)sender).Font;
            this.parentForm.label1.Text = ((TextBox)sender).Text;
            this.parentForm.label1.AutoSize = true;
            this.parentForm.label1.MaximumSize = new Size(300, 0);

            if (((TextBox)sender).Height < 100)
            {
                int caretPosition = ((TextBox)sender).SelectionStart;
                ((TextBox)sender).Size = new Size(this.parentForm.label1.Width +7, this.parentForm.label1.Height + 9);
                ((TextBox)sender).SelectionStart = 0;
                ((TextBox)sender).ScrollToCaret();
                ((TextBox)sender).SelectionStart = caretPosition;
            }
            if (oldHeight != ((TextBox)sender).Height)
            {

                this.main_label.Height = this.Height = ((TextBox)sender).Height;
                this.main_label.Width = this.Width = ((TextBox)sender).Width;
                this.text = textbox.Text;
                this.refresh();
                this.RBListClass.refresh();
            }
            if (oldWidth != ((TextBox)sender).Width) {
                this.text = textbox.Text;
                this.main_label.Width = this.Width = ((TextBox)sender).Width;
                this.refresh(); }
        }
        private void textbox_KeyPressed(object sender, KeyPressEventArgs e){

            if (e.KeyChar == 13)
            {
                this.parentPanel.Focus();
                this.isShowMainLabel = true;
            }
        }
        private void textbox_lostFocus(object sender, EventArgs e)
        {
            this.text = this.textbox.Text;
            this.parentPanel.Controls.Remove(this.textbox);
            this.parentPanel.Focus();
            this.isShowMainLabel = true;
            this.refresh();
            this.RBListClass.refresh();
        }
        private void deletePBox_Clicked(object sender, EventArgs e)
        {
            AskForm af = new AskForm("Удалить пункт из списка?");
            af.ShowDialog();
            if (af.IsOKed)
            {
                this.RBClass.deleteItem(this);
            }
        }
        public void delete()
        {
            this.parentPanel.Controls.Remove(this.main_label);
            this.parentPanel.Controls.Remove(this.deletePBox);
        }
        private int calcWidth()
        {
            int w = 0;
            Label label = new Label();
            label.Text = this.text;
            label.AutoSize = true;
            label.MaximumSize = new Size(this.MaximumSize.Width, 0);
            this.parentPanel.Controls.Add(label);
            w = label.Width;
            this.parentPanel.Controls.Remove(label);
            return w;
        }
        private int calcHeight()
        {
            int h = 0;
            Label label = new Label();
            label.Text = this.text;
            label.AutoSize = true;
            label.MaximumSize = new Size(this.MaximumSize.Width, 0);
            this.parentPanel.Controls.Add(label);
            h = label.Height;
            this.parentPanel.Controls.Remove(label);
            return h;
        }
        public string getText() { return this.text; }
        public int getWidth() { return this.Width; }
        public int getHeight() {
            if (this.main_label.Height > this.deletePBox.Height) this.Height = main_label.Height;
            else this.Height = this.deletePBox.Height;
            return this.Height; 
        }
        public void setLocation(Point newLocation) { this.Location = newLocation; }
        public Point getLocation() { return this.Location; }

    }
}
