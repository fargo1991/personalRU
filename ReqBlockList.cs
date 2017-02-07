using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PersonalRU
{
    class ReqBlockList
    {
        List<RequirBlock> RBList = new List<RequirBlock>();
        VacancyForm parent;
        Panel parentPanel;
        private Point Location;
        private int margin = 10;

        public ReqBlockList(VacancyForm parent, Panel parentPanel, Point Location)
        {
            this.parent = parent;
            this.parentPanel = parentPanel;
        }
        public void addBlock(RequirBlock RBlock)
        {
            this.RBList.Add(RBlock);
        }
        public void createBlock(string blockTitle, bool isList)
        {
            RequirBlock RBlock = new RequirBlock(this.parent, this.parentPanel, this, this.calcNewBlockLocation(), blockTitle, isList);
            this.RBList.Add(RBlock);
            this.refresh();
        }
        public void addItemToBlock(string text, string Title)
        {
            RequirBlock RBlock = this.RBList.Find(RequirBlock => RequirBlock.Title == Title);
            RBlock.addItem(text);
        }
        public RequirBlock getRBlockByTitle(string Title)
        {
            RequirBlock RBlock = this.RBList.Find(RequirBlock => RequirBlock.Title == Title);
            return RBlock;
        }
        private Point calcNewBlockLocation()
        {
            Point newLocation = new Point();
            return newLocation;
        }
        public void refresh()
        {
            int oldScrollValue = this.parentPanel.VerticalScroll.Value;
            this.parentPanel.VerticalScroll.Value = 0;
            Point lastBlockLocation = this.Location;
            foreach (RequirBlock RBlock in this.RBList)
            {
                RBlock.setLocation(lastBlockLocation);
                RBlock.refresh();
                lastBlockLocation = new Point(lastBlockLocation.X, lastBlockLocation.Y + RBlock.getBlockHeight() + this.margin);
            }
            this.parentPanel.VerticalScroll.Value = oldScrollValue;
        }
        public void setLocation(Point newLocation)
        {
            this.Location = newLocation;
            this.refresh();
        }
    }
}
