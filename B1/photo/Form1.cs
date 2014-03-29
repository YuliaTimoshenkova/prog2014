using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace photo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     Bitmap inPicture;
             
      private void button1_Click(object sender, EventArgs e)
        {
            var color = new Color();
            for (int h = 0; h < inPicture.Height; h++)
            {
                for (int w = 0; w < inPicture.Width; w++)
                {
                    color = inPicture.GetPixel(w, h);
                    inPicture.SetPixel(w, h, Color.FromArgb(trackBar1.Value, color.R, color.R));
                }
            }

            for (int h = 0; h < inPicture.Height; h++)
            {
                for (int w = 0; w < inPicture.Width; w++)
                {
                    color = inPicture.GetPixel(w, h);
                    inPicture.SetPixel(w, h, Color.FromArgb(trackBar2.Value, color.B, color.B));
                }
            }

            for (int h = 0; h < inPicture.Height; h++)
            {
                for (int w = 0; w < inPicture.Width; w++)
                {
                    color = inPicture.GetPixel(w, h);
                    inPicture.SetPixel(w, h, Color.FromArgb(trackBar3.Value, color.G, color.G));
                }
            }

            pictureBox1.Refresh();
        }
      private void button2_Click_1(object sender, EventArgs e)
      {
          pictureBox1.Image = inPicture;

          var bmp = new Bitmap(inPicture);
          MakeGray(bmp);
          pictureBox1.Image = bmp;
      }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {
            label2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar3.Value.ToString();
        }
      private void MakeGray(Bitmap bmp)
        {
            PixelFormat pxf = PixelFormat.Format24bppRgb;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);
            IntPtr ptr = bmpData.Scan0;

            int numBytes = bmpData.Stride * bmp.Height;
            int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            Marshal.Copy(ptr, rgbValues, 0, numBytes);

           for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
              int value = rgbValues[counter] + rgbValues[counter + 1] + rgbValues[counter + 2];
              byte color_b = 0;

                color_b = Convert.ToByte(value / 3);

                rgbValues[counter] = color_b;
                rgbValues[counter + 1] = color_b;
                rgbValues[counter + 2] = color_b;
            }
            
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            bmp.UnlockBits(bmpData);
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                inPicture = (Bitmap)Image.FromFile(ofd.FileName);
                pictureBox1.Enabled = true;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = inPicture;
                           
               
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "jpg|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var fileName = sfd.FileName;
                var fileStream = File.Create(sfd.FileName);
                inPicture.Save(fileStream, inPicture.RawFormat);

            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
          Close();
        }
 
    }
}
