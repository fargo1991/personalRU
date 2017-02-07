using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PersonalRU
{
    public class PhotoTrimmer
    {
        public int X = 0;
        public int Y = 0;
        private int Width = 1;
        private int Height = 1;
        public double ratio;
        public Bitmap img;
        public PhotoTrimmer(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ratio = (double)this.Width / (double)this.Height;
            img = new Bitmap(Width, Height);
        }
        public PhotoTrimmer(int x, int y, int width, int height)
        {
            this.X = x; this.Y = y;
            this.Width = width;
            this.Height = height;
            this.ratio = (double)this.Width / (double)this.Height;
        }
        public PhotoTrimmer(int x, int y, Size size)
        {
            this.X = x; this.Y = y;
            this.Width = size.Width;
            this.Height = size.Height;
            this.ratio = (double)this.Width / (double)this.Height;
        }
        public void setWidth(int width){
            this.Width = width;            
            this.Height =(int)(this.Width / this.ratio);
        }
        public void setHeight(int height)
        {
            this.Height = height;
            this.Width = (int)(this.Height * this.ratio);
        }
        public int getWidth() { return this.Width; }
        public int getHeight() { return this.Height; }
        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(this.X, this.Y, this.Width, this.Height);
            return rect;
        }
    }
}
