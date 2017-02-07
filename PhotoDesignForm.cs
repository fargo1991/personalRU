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
    public partial class PhotoDesignForm : Form
    {
        private bool isChange = false;
        private string photoFPath = "";
        private Size min_size = new Size(140,160);
        private Size max_size = new Size(700,800);
        private Bitmap photo;
        private Bitmap origin_photo;
        private Bitmap ready_photo;
        private double ratio;
        private int windent = 40;
        private int hindent = 90;
        public bool isOKed = false;

        private static Size mini_p_size = new Size(140, 160);
        private static Size p_trimmer_init_size = new Size(140, 160);
        private static Size max_p_size = new Size(700, 800);

        PhotoTrimmer PTrimmer = new PhotoTrimmer(10, 10, p_trimmer_init_size);
        private bool IsPTrimmerHover = false;
        private bool IsPTrimmerCaptured = false;
        private int X_Deviation = 0, Y_Deviation = 0;
        private int PTCornerIndent = 10;

        private bool LT_CornerHover = false;
        private bool RT_CornerHover = false;
        private bool LB_CornerHover = false;
        private bool RB_CornerHover = false;

        private bool IsLT_CornerCaptured = false;
        private bool IsRT_CornerCaptured = false;
        private bool IsRB_CornerCaptured = false;
        private bool IsLB_CornerCaptured = false;
         
        #region INITIALIZATION
        public PhotoDesignForm(Bitmap _photo, bool isChange, string photoPath)
        {
            this.isChange = isChange;
            this.photo = _photo;
            this.ratio = (double)this.photo.Width / (double)this.photo.Height;
            this.photoFPath = photoPath;
            InitializeComponent();
            SetWindowSize();
            origin_photo = new Bitmap(photo);
            //SetLowBrightness();            
            DrawPhotoTrimmer();
            SetImage();
            ShowParams();            
        }
        private void Init(Bitmap _photo, bool isChange, string photoPath)
        {
            this.isChange = isChange;
            this.photo = _photo;
            this.ratio = (double)this.photo.Width / (double)this.photo.Height;
            this.photoFPath = photoPath;
            SetWindowSize();
            origin_photo = new Bitmap(photo);
            //SetLowBrightness();            
            DrawPhotoTrimmer();
            SetImage();
            ShowParams();            
        }
        private void ShowParams()
        {
            
        }
        private void SetImage()
        {
            Bitmap img = new Bitmap(this.photo, this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = img;
        }
        private void SetWindowSize()
        {
            if (this.photo.Width < this.min_size.Width || this.photo.Height < this.min_size.Height)
            {
                MessageBox.Show("Размеры фото должны быть не менее чем " + min_size.Width + "X" + min_size.Height + "!");
                this.Enabled = false;
            }
            if (this.photo.Width > this.max_size.Width || this.photo.Height > this.max_size.Height)
            {
                if (this.photo.Width > this.photo.Height) { setImgSizeByWidth(); }
                if (this.photo.Width < this.photo.Height) { setImgSizeByHeight(); }
            }
            if (this.photo.Width > this.max_size.Width) {setImgSizeByWidth();}
            else if (this.photo.Height > this.max_size.Height){setImgSizeByHeight();}            
            else { setControls();}
        }
        private void setImgSizeByWidth()
        {
            int w = this.max_size.Width;
            int h = (int)(w / this.ratio);
            Bitmap old_photo = photo;
            photo = new Bitmap(old_photo, w, h);
            setControls();
        }
        private void setImgSizeByHeight()
        {
            int h = this.max_size.Height;
            int w = (int)(h * this.ratio);
            Bitmap old_photo = photo;
            photo = new Bitmap(old_photo, w, h);
            setControls();
        }
        private void setControls()
        {
            if (!this.isChange)
            {
                Point point = new Point(10, 10);
                this.pictureBox1.Location = point;
                this.pictureBox1.Width = this.photo.Width;
                this.pictureBox1.Height = this.photo.Height;
                this.Width = this.photo.Width + windent;
                this.Height = this.photo.Height + hindent;

                this.BrowseButt.Visible = false;
                this.FPath_textBox.Visible = false;
                this.OK_Butt.Location = new Point(this.OK_Butt.Location.X, this.pictureBox1.Location.Y + this.pictureBox1.Height + 10);
                this.Cancel_Butt.Location = new Point(this.Cancel_Butt.Location.X, this.pictureBox1.Location.Y + this.pictureBox1.Height + 10);
            }
            else
            {                
                this.pictureBox1.Location = new Point(10,45);
                this.pictureBox1.Width = this.photo.Width;
                this.pictureBox1.Height = this.photo.Height;
                
                this.BrowseButt.Visible = true;
                this.BrowseButt.Location = new Point(10, 10); ;

                this.FPath_textBox.Visible = true;
                this.FPath_textBox.Location = new Point(80, 13);
                this.FPath_textBox.Text = this.photoFPath;
                this.Width = this.photo.Width + windent;
                this.Height = this.photo.Height + hindent + 35;

                this.OK_Butt.Location = new Point(this.OK_Butt.Location.X, this.pictureBox1.Location.Y + this.pictureBox1.Height + 10);
                this.Cancel_Butt.Location = new Point(this.Cancel_Butt.Location.X, this.pictureBox1.Location.Y + this.pictureBox1.Height + 10);
            }
        }
        #endregion        
        #region PUBLIC
        public Bitmap getPhoto(){ return this.ready_photo; }
        #endregion
        #region MAIN
        private void SetLowBrightness()
        {
            for (int x = 0; x < origin_photo.Width; x++)
            {
                for (int y = 0; y < origin_photo.Height; y++)
                {
                    byte ColorA = LowBrPixel(this.origin_photo.GetPixel(x, y).A, 0);
                    byte ColorR = LowBrPixel(this.origin_photo.GetPixel(x, y).R, 50);
                    byte ColorG = LowBrPixel(this.origin_photo.GetPixel(x, y).G, 50);
                    byte ColorB = LowBrPixel(this.origin_photo.GetPixel(x, y).B, 50);
                    Color newColor = Color.FromArgb((int)ColorA, (int)ColorR, (int)ColorG, (int)ColorB);                    
                    this.origin_photo.SetPixel(x, y, newColor);
                }
            }
        }
        private byte LowBrPixel(byte o_color, int br_level)
        {
            if ((int)o_color - br_level > 0) return (byte)(o_color - br_level);                        
            else return 0X00;            
        }        
        private void DrawPhotoTrimmer()
        {            
            this.photo = new Bitmap(origin_photo);
            DrawRect(PTrimmer.X - PTCornerIndent / 2,
                     PTrimmer.Y - PTCornerIndent / 2,
                     PTCornerIndent, PTCornerIndent, 150);
            DrawRect(PTrimmer.X + PTrimmer.getWidth() - PTCornerIndent / 2,
                     PTrimmer.Y - PTCornerIndent / 2,
                     PTCornerIndent, PTCornerIndent, 150);
            DrawRect(PTrimmer.X + PTrimmer.getWidth() - PTCornerIndent / 2,
                     PTrimmer.Y + PTrimmer.getHeight() - PTCornerIndent / 2,
                     PTCornerIndent, PTCornerIndent, 150);
            DrawRect(PTrimmer.X - PTCornerIndent / 2,
                     PTrimmer.Y + PTrimmer.getHeight() - PTCornerIndent / 2,
                     PTCornerIndent, PTCornerIndent, 150);
            DrawRect(PTrimmer.X- PTCornerIndent/8, PTrimmer.Y + PTCornerIndent / 2, PTCornerIndent / 2, PTrimmer.getHeight() - PTCornerIndent,50);
            DrawRect(PTrimmer.X + PTrimmer.getWidth() - PTCornerIndent/2, PTrimmer.Y + PTCornerIndent / 2, PTCornerIndent / 2, PTrimmer.getHeight() - PTCornerIndent, 50);
            DrawRect(PTrimmer.X + PTCornerIndent / 2, PTrimmer.Y , PTrimmer.getWidth() - PTCornerIndent, PTCornerIndent / 2, 50);
            DrawRect(PTrimmer.X + PTCornerIndent / 2, PTrimmer.Y +PTrimmer.getHeight() - PTCornerIndent / 2, PTrimmer.getWidth() - PTCornerIndent, PTCornerIndent / 2, 50);
        }
        private void DrawRect(int x, int y, int width, int height, int br_level)
        {
            int rx, ry;
            for (rx = x; rx<x+width;rx++){
                for (ry = y; ry < y + height; ry++)
                {
                    DrawPixel(rx, ry, br_level);
                }
            }
        }
        private void DrawPixel(int x, int y,int br_level) { 
            byte ColorA = GetChangedColor(this.photo.GetPixel(x, y).A, 0);
                    byte ColorR = GetChangedColor(this.photo.GetPixel(x, y).R, br_level);
                    byte ColorG = GetChangedColor(this.photo.GetPixel(x, y).G, br_level);
                    byte ColorB = GetChangedColor(this.photo.GetPixel(x, y).B, br_level);
                    Color newColor = Color.FromArgb((int)ColorA,(int)ColorR,(int)ColorG,(int)ColorB);
            this.photo.SetPixel(x, y, newColor);
        }
        private byte GetChangedColor(byte o_color, int br_level)
        {            
            if ((int)o_color + br_level < 255) return (byte)(o_color + br_level);            
            else return 0XFF;
        }
        private void PTrimmerMove(MouseEventArgs e)
        {
            PTrimmerMoveConditions(e);
            DrawPhotoTrimmer();
            pictureBox1.Image = photo;
            pictureBox1.Refresh();
        }
        private void PTrimmerMoveConditions(MouseEventArgs e)
        {
            // PTrimmer Move
            if ((e.X - X_Deviation - PTCornerIndent / 2 > 0) &&
                (e.Y - Y_Deviation - PTCornerIndent / 2 > 0) &&
                (e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 < photo.Width) &&
                (e.Y - Y_Deviation + PTrimmer.getHeight() + PTCornerIndent / 2 < photo.Height))
            {
                PTrimmer.X = e.X - X_Deviation;
                PTrimmer.Y = e.Y - Y_Deviation;
            }
            // PTrimmer MouseLeavePictureBox Move
            // left_border x , y = norm    
            else if ((e.X - X_Deviation - PTCornerIndent / 2 < 0)
                 && (e.Y - Y_Deviation - PTCornerIndent / 2 > 0)
                 && (e.Y - Y_Deviation + PTrimmer.getHeight() + PTCornerIndent / 2 < photo.Height))
            {
                PTrimmer.X = PTCornerIndent / 2;
                PTrimmer.Y = e.Y - Y_Deviation;
            }
            // right border x, y = norm
            else if ((e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 > photo.Width)
                && (e.Y - Y_Deviation - PTCornerIndent / 2 > 0)
                && (e.Y - Y_Deviation + PTrimmer.getHeight() + PTCornerIndent / 2 < photo.Height))
            {
                PTrimmer.X = photo.Width - PTrimmer.getWidth() - PTCornerIndent / 2;
                PTrimmer.Y = e.Y - Y_Deviation;
            }
            // top border y, x = norm
            else if ((e.Y - Y_Deviation - PTCornerIndent / 2 < 0)
                && (e.X - X_Deviation - PTCornerIndent / 2 > 0)
                && (e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 < photo.Width))
            {
                PTrimmer.X = e.X - X_Deviation;
                PTrimmer.Y = PTCornerIndent / 2;
            }
            // bottom border y, x = norm
            else if ((e.Y - Y_Deviation + PTCornerIndent / 2 > photo.Height - PTrimmer.getHeight())
                  && (e.X - X_Deviation - PTCornerIndent / 2 > 0)
                  && (e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 < photo.Width))
            {
                PTrimmer.X = e.X - X_Deviation;
                PTrimmer.Y = photo.Height - PTrimmer.getHeight() - PTCornerIndent / 2;
            }
            // left top corner 
            else if ((e.X - X_Deviation - PTCornerIndent / 2 < 0) && (e.Y - Y_Deviation - PTCornerIndent / 2 < 0))
            {
                PTrimmer.X = PTCornerIndent / 2;
                PTrimmer.Y = PTCornerIndent / 2;
            }
            // right top corner
            else if ((e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 > photo.Width) &&
                    (e.Y - Y_Deviation - PTCornerIndent / 2 < 0))
            {
                PTrimmer.X = photo.Width - PTrimmer.getWidth() - PTCornerIndent / 2;
                PTrimmer.Y = PTCornerIndent / 2;
            }
            // right bottom corner
            else if ((e.X - X_Deviation + PTrimmer.getWidth() + PTCornerIndent / 2 > photo.Width) &&
                     (e.Y - Y_Deviation - PTCornerIndent / 2 > photo.Height - PTrimmer.getHeight()))
            {
                PTrimmer.X = photo.Width - PTrimmer.getWidth() - PTCornerIndent / 2;
                PTrimmer.Y = photo.Height - PTrimmer.getHeight() - PTCornerIndent / 2;
            }
            // left bottom
            else if ((e.Y - Y_Deviation - PTCornerIndent / 2 > photo.Height - PTrimmer.getHeight()) &&
                     (e.X - X_Deviation - PTCornerIndent / 2 < 0))
            {
                PTrimmer.X = PTCornerIndent / 2;
                PTrimmer.Y = photo.Height - PTrimmer.getHeight() - PTCornerIndent / 2;
            }
        }
        private void PTrimmerMouseHoverCondition(MouseEventArgs e)
        {
            // Common PTrimmer Hover Conditions
            if ((e.X >= PTrimmer.X && e.X <= PTrimmer.X + PTrimmer.getWidth()) &&
                (e.Y >= PTrimmer.Y && e.Y <= PTrimmer.Y + PTrimmer.getHeight()))
            {
                resetAllHovers();
                IsPTrimmerHover = true;
            }            
            // PTrimmer Coners MouseHover
            else if ((e.X > PTrimmer.X - PTCornerIndent) && (e.X < PTrimmer.X + PTCornerIndent) &&
                    (e.Y > PTrimmer.Y - PTCornerIndent) && (e.Y < PTrimmer.Y + PTCornerIndent))
            { resetAllHovers(); LT_CornerHover = false; }// = true; }
            else if ((e.X > PTrimmer.X + PTrimmer.getWidth()- PTCornerIndent - 1) && (e.X < PTrimmer.X + PTrimmer.getWidth() + PTCornerIndent) &&
                (e.Y > PTrimmer.Y - PTCornerIndent - 1) && (e.Y < PTrimmer.Y + PTCornerIndent))
            { resetAllHovers(); RT_CornerHover = false; }// = true; }
            else if ((e.X > PTrimmer.X + PTrimmer.getWidth()- PTCornerIndent - 1) && (e.X < PTrimmer.X + PTrimmer.getWidth() + PTCornerIndent) &&
                (e.Y < PTrimmer.Y + PTrimmer.getHeight() + PTCornerIndent) && (e.Y > PTrimmer.Y + PTrimmer.getHeight() - PTCornerIndent - 1))
            { resetAllHovers(); RB_CornerHover = true; }
            else if ((e.X > PTrimmer.X - PTCornerIndent) && (e.X < PTrimmer.X + PTCornerIndent) &&
                (e.Y < PTrimmer.Y + PTrimmer.getHeight() + PTCornerIndent) && (e.Y > PTrimmer.Y + PTrimmer.getHeight() - PTCornerIndent))
            { resetAllHovers(); LB_CornerHover = false; }// = true; }
            else resetAllHovers();
        }
        private void resetAllHovers()
        {
            IsPTrimmerHover = false;
            LT_CornerHover = false;
            RT_CornerHover = false;
            RB_CornerHover = false;
            LB_CornerHover = false;
        }
        private void resetAllCaptures()
        {
            IsPTrimmerCaptured = false;
            IsLT_CornerCaptured = false;
            IsRT_CornerCaptured = false;
            IsRB_CornerCaptured = false;
            IsLB_CornerCaptured = false;
        }
        private void SetCursor()
        {
            if (IsPTrimmerHover) { Cursor = Cursors.SizeAll; }
            else if (!IsPTrimmerHover && IsPTrimmerCaptured) { Cursor = Cursors.SizeAll; }
            else if (IsPTrimmerHover && !IsPTrimmerCaptured) { Cursor = Cursors.SizeAll; }            

            else if (LT_CornerHover) { Cursor = Cursors.SizeNWSE; }
            else if (RT_CornerHover) { Cursor = Cursors.SizeNESW; }
            else if (RB_CornerHover) { Cursor = Cursors.SizeNWSE; }
            else if (LB_CornerHover) { Cursor = Cursors.SizeNESW; }
            else if (!IsPTrimmerHover && 
                !LT_CornerHover &&
                !RT_CornerHover &&
                !RB_CornerHover &&
                !LB_CornerHover) { Cursor = Cursors.Default; }
        }
        private void PTrimmerResize(string movable_conner, MouseEventArgs e)
        {
            calculateRect(movable_conner, e);            
            DrawPhotoTrimmer();
            pictureBox1.Image = photo;
            pictureBox1.Refresh();
        }
        private void calculateRect(string movable_conner, MouseEventArgs e)
        {
            if (movable_conner == "lt")
            {
                int w_increment = PTrimmer.X - e.X;
                int h_increment = PTrimmer.Y - e.Y;

                if (PTrimmer.getWidth() + w_increment > max_p_size.Width) { w_increment = max_p_size.Width - PTrimmer.getWidth(); }
                else if (PTrimmer.getWidth() + w_increment < p_trimmer_init_size.Width) { w_increment = PTrimmer.getWidth() - p_trimmer_init_size.Width; }
                else { PTrimmer.X = e.X; int w = PTrimmer.getWidth() + w_increment; PTrimmer.setWidth(w); }
                if (PTrimmer.getHeight() + h_increment > max_p_size.Height) { h_increment = max_p_size.Height - PTrimmer.getHeight(); }
                else if (PTrimmer.getHeight() + h_increment < p_trimmer_init_size.Height) { h_increment = p_trimmer_init_size.Height - PTrimmer.getHeight(); }
                else { PTrimmer.Y = e.Y; int h = PTrimmer.getHeight() + h_increment; PTrimmer.setHeight(h); }
            }
            if (movable_conner == "rt")
            {
                int w_increment = e.X - (PTrimmer.X + PTrimmer.getWidth());
                int h_increment = PTrimmer.Y - e.Y;

                if (PTrimmer.getWidth() + w_increment > max_p_size.Width) { w_increment = max_p_size.Width - PTrimmer.getWidth(); }
                else if (PTrimmer.getWidth() + w_increment < p_trimmer_init_size.Width) { w_increment = PTrimmer.getWidth() - p_trimmer_init_size.Width; }
                else { int w = PTrimmer.getWidth() + w_increment; PTrimmer.setWidth(w); PTrimmer.X = e.X - PTrimmer.getWidth(); }
                if (PTrimmer.getHeight() + h_increment > max_p_size.Height) { h_increment = max_p_size.Height - PTrimmer.getHeight(); }
                else if (PTrimmer.getHeight() + h_increment < p_trimmer_init_size.Height) { h_increment = p_trimmer_init_size.Height - PTrimmer.getHeight(); }
                else { PTrimmer.Y = e.Y; int h = PTrimmer.getHeight() + h_increment; PTrimmer.setHeight(h); }
            }
            if (movable_conner == "rb")
            {
                int w_increment = e.X - PTrimmer.getWidth() - PTrimmer.X;
                int h_increment = e.Y - PTrimmer.getHeight() - PTrimmer.Y;                
                //max'n'min size Width conditions
                if (PTrimmer.getWidth() + w_increment > max_p_size.Width) { w_increment = max_p_size.Width - PTrimmer.getWidth(); }
                else if (PTrimmer.getWidth() + w_increment < min_size.Width) { w_increment = PTrimmer.getWidth() - min_size.Width; }
                else { PTrimmer.setWidth(e.X - PTrimmer.X); }
                //max'n'min size Height Conditions
                if (PTrimmer.getHeight() + h_increment > max_p_size.Height) { h_increment = max_p_size.Height - PTrimmer.getHeight(); }
                else if (PTrimmer.getHeight() + h_increment < min_size.Height) { h_increment = PTrimmer.getHeight() - min_size.Height; }
                else { PTrimmer.setHeight(e.Y - PTrimmer.Y); }

                // Conners Consitions
                if ((PTrimmer.X + PTrimmer.getWidth() + PTCornerIndent / 2 + 1 > photo.Width))
                    PTrimmer.setWidth(photo.Width - PTrimmer.X - PTCornerIndent/2 - 1);
                if ((PTrimmer.Y + PTrimmer.getHeight() + PTCornerIndent / 2 + 1 > photo.Height))
                    PTrimmer.setHeight(photo.Height - PTrimmer.Y - PTCornerIndent / 2 - 1);
            }
            if (movable_conner == "lb")
            {
                int w_increment = PTrimmer.X - e.X;
                PTrimmer.X = e.X; int w = PTrimmer.getWidth(); PTrimmer.setWidth(w+ w_increment);
                PTrimmer.setHeight(e.Y - PTrimmer.Y);
            }
        }
        #endregion
        #region EVENTS
        private void OK_Butt_Click(object sender, EventArgs e)
        {
            Rectangle rect = PTrimmer.GetRectangle();
            this.ready_photo = origin_photo.Clone(rect, origin_photo.PixelFormat);            
            this.isOKed = true;
            this.Close();
        }
        private void Cancel_Butt_Click(object sender, EventArgs e)
        {
            this.isOKed = false;
            this.Close();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PTrimmerMouseHoverCondition(e);
            SetCursor();
            if (IsPTrimmerCaptured) PTrimmerMove(e);
            else if (IsLT_CornerCaptured){ PTrimmerResize("lt", e);}
            else if (IsRT_CornerCaptured){ PTrimmerResize("rt", e);}
            else if (IsRB_CornerCaptured){ PTrimmerResize("rb", e);}
            else if (IsLB_CornerCaptured){ PTrimmerResize("lb", e);}
            
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            resetAllCaptures();
            if (IsPTrimmerHover) IsPTrimmerCaptured = true;
            X_Deviation = e.X - PTrimmer.X;
            Y_Deviation = e.Y - PTrimmer.Y;
            if (LT_CornerHover) IsLT_CornerCaptured = true;
            if (RT_CornerHover) IsRT_CornerCaptured = true;
            if (RB_CornerHover) IsRB_CornerCaptured = true;
            if (LB_CornerHover) IsLB_CornerCaptured = true;
        }
        private void pictureBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            resetAllCaptures();
        } 
        #endregion                
        private void BrowseButt_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = this.photoFPath;
            openFileDialog1.FileName = this.photoFPath;
            openFileDialog1.ShowDialog();

            Bitmap _photo = new Bitmap(openFileDialog1.FileName);
            this.Init(_photo, this.isChange, openFileDialog1.FileName);
        }
    }
}