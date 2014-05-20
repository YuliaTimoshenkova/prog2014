using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace more_15
{
    public partial class game : Form
    {
        public game()
        {
            InitializeComponent();
        }
//массив хранения частей картинки
        PictureBox[] PB = null;
//длина стороны прямоугольников.
        int Length = 3;
//объект хранения картинки.
        Bitmap Picture = null;
//создание области рисования картинки, и определения ее размеров, после чего части картинки размещаются на панели
        void CreatePictureRegion()
        {
            if (PB != null)
            {
                for (int i = 0; i < PB.Length; i++)
                {
                    PB[i].Dispose();
                }
                PB = null;
            }
            int num = Length;
            PB = new PictureBox[num * num];
            int w = panel1.Width / num;
            int h = panel1.Height / num;
            int countX = 0; 
            int countY = 0; 
            for (int i = 0; i < PB.Length; i++)
            {
                PB[i] = new PictureBox();
                PB[i].Width = w;
                PB[i].Height = h;
                PB[i].Left = 0 + countX * PB[i].Width;
                PB[i].Top = 0 + countY * PB[i].Height;
 // Запомним начальные координаты частчки кртинки для последующей проверки
                Point pt = new Point();
                pt.X = PB[i].Left;
                pt.Y = PB[i].Top;
                PB[i].Tag = pt; 
                countX++;
                if (countX == num)
                {
                    countX = 0;
                    countY++;
                }
                PB[i].Parent = panel1; 
                PB[i].BorderStyle = BorderStyle.None;
                PB[i].SizeMode = PictureBoxSizeMode.StretchImage; 
                PB[i].Show(); 
                PB[i].Click += new EventHandler(PB_Click);
            }
            DrawPicture();
        }

        void DrawPicture()
        {
            if (Picture == null) return;
            int countX = 0;
            int countY = 0;
            int num = Length;
            for (int i = 0; i < PB.Length; i++)
            {
                int w = Picture.Width / num;
                int h = Picture.Height / num;
                PB[i].Image = Picture.Clone(new RectangleF(countX * w, countY * h, w, h), Picture.PixelFormat);
                countX++;
                if (countX == Length)
                {
                    countX = 0;
                    countY++;
                }
            }
        }

        void PB_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            for (int i = 0; i < PB.Length; i++)
            {
//определиние пустой части и возможности хода
                if (PB[i].Visible == false)
                {
                    if (
                        (pb.Location.X == PB[i].Location.X &&
                         Math.Abs(pb.Location.Y - PB[i].Location.Y) == PB[i].Height)
                        ||
                        (pb.Location.Y == PB[i].Location.Y &&
                         Math.Abs(pb.Location.X - PB[i].Location.X) == PB[i].Width)
                        )
                    {
                        Point pt = PB[i].Location;
                        PB[i].Location = pb.Location;
                        pb.Location = pt;
//проверка положения
                        for (int j = 0; j < PB.Length; j++)
                        {
                            Point point = (Point)PB[j].Tag;
                            if (PB[j].Location != point)
                            {
                                return;
                            }
                        }
//условие проверки победы
                        for (int m = 0; m < PB.Length; m++)
                        {
                            PB[m].Visible = true;
                            PB[m].BorderStyle = BorderStyle.None; 
                        }
                        MessageBox.Show("ПОБЕДА!!!!", "WIN", MessageBoxButtons.OK);
                    }
                    break;
                }
            }
        }

//загрузка картинки
        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDlg = new OpenFileDialog();
            ofDlg.Filter = "файлы картинок (*.bmp;*.jpg;*.jpeg;)|*.bmp;*.jpg;.jpeg|All files (*.*)|*.*";
            ofDlg.FilterIndex = 1;
            ofDlg.RestoreDirectory = true;

            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                Picture = new Bitmap(ofDlg.FileName);
                CreatePictureRegion();
            }
        }

        private void нАЧАТЬToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Picture == null) return;
            Random rand = new Random(Environment.TickCount);
            int r = 0;
            for (int i = 0; i < PB.Length; i++)
            {
                PB[i].Visible = true;
                r = rand.Next(0, PB.Length);
                Point ptR = PB[r].Location;
                Point ptI = PB[i].Location;
                PB[i].Location = ptR;
                PB[r].Location = ptI;
                PB[i].BorderStyle = BorderStyle.FixedSingle;
            }
// случайным образом выбираем часть кртинки, и делаем ее невидимой
            r = rand.Next(0, PB.Length);
            PB[r].Visible = false;
        }
//уровень, определяет количетво частей
        private void уРОВЕНЬToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level level = new level();
            level.Level = Length;
            if (level.ShowDialog() == DialogResult.OK)
            {
                Length = level.Level;
                CreatePictureRegion();
            }
        }
//возвращает исходную картинку
        private void оТМЕНАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PB.Length; i++)
            {
                Point pt = (Point)PB[i].Tag;
                PB[i].Location = pt;
                PB[i].Visible = true;
            }
        }
//окно с исходной картинкой
        private void пОДСКАЗКАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help Help = new help();
            Help.ImageDuplicate = Picture;
            Help.ShowDialog();
        }
//окно about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра 15 с картинками  и разными уровнями сложности", "об игре", MessageBoxButtons.OK);
        }
    }
}
