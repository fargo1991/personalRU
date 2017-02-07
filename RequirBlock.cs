using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PersonalRU
{
    class RequirBlock
    {
        Label TitleLabel = new Label();
        List<Item> iList = new List<Item>();
        AddButton abutt;
        public string Title = "";
        private Point Location = new Point();

        private VacancyForm parent;
        private Panel parentPanel;
        private ReqBlockList parentClass;

        private int margin = 5;
        private Item lastItem;

        public bool isList = false;

        public RequirBlock(VacancyForm parent, Panel parentPanel, ReqBlockList parentClass, Point Location, string Title, bool isList)
        {
            this.isList = isList;
            this.parent = parent;
            this.parentPanel = parentPanel;
            this.parentClass = parentClass;

            this.Location = Location;
            this.Title = Title;

            this.TitleLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.TextAlign = ContentAlignment.MiddleRight;

            if (this.isList) this.abutt = new AddButton(this.parentClass, parent, parentPanel, this, this.Location, false);
            else
                if (this.iList.Count == 0)
                this.abutt = new AddButton(this.parentClass, parent, parentPanel, this, this.Location, true);

            refresh();
            //this.lastItem = new Item("", this.Location, parent, parentPanel, new Size(300, 0));
        }
        public void refresh()
        {
            refreshTitle();
            refreshItemsList();
            refreshAddButt();
        }
        private void refreshItemsList()
        {
            Point itemLocation = new Point(this.Location.X + this.TitleLabel.Width + this.margin, this.Location.Y);
            foreach (Item item in this.iList)
            {
                item.setLocation(itemLocation);
                item.refresh();
                itemLocation = new Point(itemLocation.X, itemLocation.Y + item.getHeight() + this.margin);
                //if (item.getHeight() > item.deletePBox.Height) itemLocation = new Point(itemLocation.X, itemLocation.Y + item.getHeight() + this.margin);
                //else itemLocation = new Point(itemLocation.X, itemLocation.Y + item.deletePBox.Height + this.margin);
                this.lastItem = item;
            }
        }
        private void refreshTitle()
        {
            if (this.parentPanel.Controls.Contains(this.TitleLabel)) this.parentPanel.Controls.Remove(TitleLabel);
            this.TitleLabel.Text = this.Title;
            this.TitleLabel.Location = this.Location;
            this.TitleLabel.MaximumSize = new Size(150, 0);
            this.TitleLabel.AutoSize = true;
            this.parentPanel.Controls.Add(TitleLabel);
        }
        private void refreshAddButt()
        {
            if (this.isList)
            {
                Point newLocation;
                if (this.iList.Count > 0)
                    if (this.lastItem.getHeight() > this.lastItem.deletePBox.Height) newLocation = new Point(this.TitleLabel.Location.X + this.TitleLabel.Width + this.margin, this.lastItem.getLocation().Y + this.lastItem.getHeight() + this.margin);
                    else newLocation = new Point(this.TitleLabel.Location.X + this.TitleLabel.Width + this.margin, this.lastItem.getLocation().Y + this.lastItem.deletePBox.Height + this.margin);
                else
                    newLocation = new Point(this.TitleLabel.Location.X + this.TitleLabel.Width + this.margin, this.TitleLabel.Location.Y);
                this.abutt.setLocation(newLocation);
                this.abutt.refresh();
            }
            else
            {
                if (this.iList.Count > 0)
                {
                    this.abutt.delete();
                }
            }
        }
        public void addItem(string text)
        {
            Item item = new Item(text, new Point(), this.parent, this.parentPanel, this.parentClass,this, new Size(300, 0));
            this.iList.Add(item);
            this.refresh();
            this.parentClass.refresh();
        }
        public void deleteItem(Item item)
        {
            item.delete();
            this.iList.Remove(item);
            refresh();
            this.parentClass.refresh();
        }
        public void setLocation(Point newLocation)
        {
            this.Location = newLocation;
        }
        public int getBlockHeight()
        {
            int w = 0;
            foreach (Item item in this.iList)
                if (item.getHeight() > item.deletePBox.Height)
                    w += item.getHeight() + this.margin;
                else w += item.deletePBox.Height + this.margin;
            if (this.isList)w += this.abutt.getHeight() + this.margin;
            if (TitleLabel.Height > w) return TitleLabel.Height;
            return w;
        }
        public Item getItem(int LID) { return this.iList[LID]; }
        public List<Item> getList() { return this.iList;}
    }
}
