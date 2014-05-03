using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SimpleImageProcessing;
//using System.Runtime.InteropServices;








namespace PhotoProcesser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
   
        private Bitmap origin;
         [DllImport("user32.dll")]

        public static extern int SendMessage(
               int hWnd,      // handle to destination window
               uint Msg,       // message
               long wParam,  // first message parameter
               long lParam   // second message parameter
               );


         PictureBox pic = new PictureBox();
         private int begin_x;
         private int begin_y;
         bool resize = false;
         private int scroller_vert = -1;
         private int scroller_hor = -1;



         private void Form1_Load(object sender, EventArgs e)
         {
             pic.Parent = pictureBox1;
             pic.BackColor = Color.Transparent;
             pic.SizeMode = PictureBoxSizeMode.AutoSize;
             pic.BorderStyle = BorderStyle.FixedSingle;
             pic.Visible = false;
         }

         private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {
                 begin_x = e.X;
                 begin_y = e.Y;
                 pic.Left = e.X;
                 pic.Top = e.Y;
                 pic.Width = 0;
                 pic.Height = 0;
                 pic.Visible = true;
                 timer1.Start();
                 resize = true;
             }
         }

         private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {
                 pic.Width = e.X - begin_x;
                 pic.Height = e.Y - begin_y;

                 //—кроллинг...
                 scroller_hor = -1;
                 scroller_vert = -1;

                 if (e.X > panel2.Width - 5)
                 { scroller_hor = 0; }

                 if (e.Y > panel2.Height - 5)
                 { scroller_vert = 0; }


             }
         }


         static public Image Copy(Image srcBitmap, Rectangle section)
         {
             // ¬ырезаем выбранный кусок картинки
             Bitmap bmp = new Bitmap(section.Width, section.Height);
             using (Graphics g = Graphics.FromImage(bmp))
             {

                 g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
             }
             //¬озвращаем кусок картинки.
             return bmp;
         }


         private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
         {

             pic.Width = 0;
             pic.Height = 0;
             pic.Visible = false;
             timer1.Stop();
             if (resize == true)
             {
                 if ((e.X > begin_x + 10) && (e.Y > begin_y + 10)) //„тобы совсем уж мелочь не вырезал - и по случайным нажати€м не срабатывал! (можно убрать +10)
                 {
                     Rectangle rec = new Rectangle(begin_x, begin_y, e.X - begin_x, e.Y - begin_y);
                     pictureBox1.Image = Copy(pictureBox1.Image, rec);
                 }
             }
             resize = false;
         }

         private void timer1_Tick(object sender, EventArgs e)
         {
             if (scroller_vert > -1)
             {
                 SendMessage(panel2.Handle.ToInt32(), 277, 1, scroller_vert);
             }
             if (scroller_hor > -1)
             {
                 SendMessage(panel2.Handle.ToInt32(), 276, 1, scroller_hor);
             }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog(){Filter = "*.jpg|*.jpg"};
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                origin = (Bitmap) Image.FromFile(ofd.FileName);
                pictureBox1.Image = origin;
            }
        }

        ImagerBitmap bmp;

        ImagerBitmap src;
        private void button2_Click(object sender, EventArgs e)
        {
            src = new ImagerBitmap(origin.Clone() as Bitmap);
            var dst = new ImagerBitmap(origin.Clone() as Bitmap);
            
            for (int x = 1; x < src.Bitmap.Width-1; x++)
                for (int y = 1; y < src.Bitmap.Height-1; y++)
                {
                    if(TestIfCircle(x,y))
                    dst.SetPixel(x,y,Color.Red);
                }

            src.UnlockBitmap();
            dst.UnlockBitmap();

            pictureBox2.Image = dst.Bitmap;
        }

        private bool TestIfCircle(int x, int y)
        {
            //Dictionary<int,double>intensity = new Dictionary<int, double>();

            double lastIntensity = -1;
            for (int r = 8; r < 20; r++)
            {
                if (lastIntensity == -1)
                {
                    lastIntensity = GetCircleIntensity(x, y, r);
                }
                else
                {
                    var currentIntensity = GetCircleIntensity(x, y, r);
                    if (currentIntensity - lastIntensity > 20)
                        return true;
                    lastIntensity = currentIntensity;
                }

                //intensity[r] = GetCircleIntensity( x, y, r);
            }
            /*for (int r = 9; r < 30; r++)
            {
                if (intensity[r] - intensity[r - 1] > 20)
                    return true;
            }*/
            return false;
        }

        private double intensity;
        private int count;

        private double GetCircleIntensity(int x0, int y0, int radius)
        {
            int x = radius, y = 0;
            int radiusError = 1 - x;

            intensity = 0;
            count = 0;

            while (x >= y)
            {
                DumpPixed(x + x0, y + y0);
                DumpPixed(y + x0, x + y0);
                DumpPixed(-x + x0, y + y0);
                DumpPixed(-y + x0, x + y0);
                DumpPixed(-x + x0, -y + y0);
                DumpPixed(-y + x0, -x + y0);
                DumpPixed(x + x0, -y + y0);
                DumpPixed(y + x0, -x + y0);
                y++;
                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
            if (count == 0)
                return 0;
            return intensity/count;
        }

        private void DumpPixed(int x, int y)
        {
            if(x<0||y<0)
                return;
            if(x>=src.Bitmap.Width || y>=src.Bitmap.Height)
                return;
            count++;
            intensity += src.GetPixel(x, y).R;
        }

        public void DrawCircle(ImagerBitmap src, int x0, int y0, int radius)
        {
            int x = radius, y = 0;
            int radiusError = 1 - x;

            while (x >= y)
            {
                src.SetPixel(x + x0, y + y0,Color.Red);
                src.SetPixel(y + x0, x + y0, Color.Red);
                src.SetPixel(-x + x0, y + y0, Color.Red);
                src.SetPixel(-y + x0, x + y0, Color.Red);
                src.SetPixel(-x + x0, -y + y0, Color.Red);
                src.SetPixel(-y + x0, -x + y0, Color.Red);
                src.SetPixel(x + x0, -y + y0, Color.Red);
                src.SetPixel(y + x0, -x + y0, Color.Red);
                y++;
                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
        }

        private int CompareColors(Color c, Color c1)
        {
            double grey = (c.R * c.R + c.G * c.G + c.B * c.B) ;
            double grey1 = (c1.R * c1.R + c1.G * c1.G + c1.B * c1.B) ;
            return (int) (grey - grey1);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var src = new ImagerBitmap(origin.Clone() as Bitmap);
            var dst = new ImagerBitmap(origin.Clone() as Bitmap);
            int treshold = trackBar1.Value;
            for (int x = 0; x < src.Bitmap.Width; x++)
                for (int y = 0; y < src.Bitmap.Height; y++)
                {
                    Color c = src.GetPixel(x, y);
                    double grey = Math.Sqrt(c.R * c.R + c.G * c.G + c.B * c.B) / Math.Sqrt(3);
                    double distance =
                        Math.Sqrt((c.R - grey) * (c.R - grey) + (c.G - grey) * (c.G - grey) + (c.B - grey) * (c.B - grey));
                    
                    if (distance < treshold)
                    {
                        dst.SetPixel(x, y, Color.FromArgb(c.G, c.B, c.R));
                    }
                    else
                    {
                        dst.SetPixel(x, y, c);
                    }
                }
            src.UnlockBitmap();
            dst.UnlockBitmap();

            pictureBox2.Image = dst.Bitmap;
        }
    }
}
