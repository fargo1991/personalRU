using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PersonalRU
{
    class VSBitmap
    {
        public Bitmap bmp;
        public VSBitmap(Bitmap bmp)
        {
            this.bmp = new Bitmap(bmp);
        }
        public Bitmap getBmap() { return this.bmp; }
        public Bitmap overLay(Bitmap bmp2)
        {
            Bitmap bmp1 = new Bitmap(this.bmp);
            for (int y = this.bmp.Height - bmp2.Height; y < this.bmp.Height; y++)
            {
                for (int x = 0; x < bmp2.Width; x++)
                {
                    byte A1 = bmp1.GetPixel(x, y).A; byte A2 = bmp2.GetPixel(x, y - bmp.Height+bmp2.Height).A;
                    byte R1 = bmp1.GetPixel(x, y).R; byte R2 = bmp2.GetPixel(x, y - bmp.Height + bmp2.Height).R;
                    byte G1 = bmp1.GetPixel(x, y).G; byte G2 = bmp2.GetPixel(x, y - bmp.Height + bmp2.Height).G;
                    byte B1 = bmp1.GetPixel(x, y).B; byte B2 = bmp2.GetPixel(x, y - bmp.Height + bmp2.Height).B;

                    int iA = (byte)((A1 + A2) / 2);
                    int iR = (byte)((R1 + R2) / 2);
                    int iG = (byte)((G1 + G2) / 2);
                    int iB = (byte)((B1 + B2) / 2);

                    if ((A2 >= 213) && (R2 >= 213) && (G2 >= 213) && (B2 >= 213))
                    {
                        iA = 255; iR = 255; iG = 255; iB = 255;
                    }

                    byte A, R, G, B;
                    if (iA > 255) A = 255; else if (iA < 0) A = 0; else A = (byte)iA;
                    if (iR > 255) R = 255; else if (iR < 0) R = 0; else R = (byte)iR;
                    if (iG > 255) G = 255; else if (iG < 0) G = 0; else G = (byte)iG;
                    if (iB > 255) B = 255; else if (iB < 0) B = 0; else B = (byte)iB;

                    Color new_color = Color.FromArgb(R, G, B);

                    bmp1.SetPixel(x, y, new_color);
                }
            }
            return bmp1;
        }
    }
}
