using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15
{
    public partial class Form1 : Form
    {
        private int s = 0;
        private int n = 0;
        // переменная истиности, для определения победы
        private bool ca = false;
        // изначальный массив 
        private int[] mas1 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0}; // эталонный массив
        // массив для игры 
        private int[] mas2 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
        //позиция цифры, которую выбрали мышкой
        private int mpos = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mix(mas2);
            this.DoubleBuffered = true;
        }

// "типа" aboutBox
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра Пятнашки", "об игре", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            if (timer2.Enabled == false)
            {
                timer2.Enabled = true;
            }
        }

//время игры, счетсчик хода игры
        private void timer2_Tick(object sender, EventArgs e)
        {
            s++;
            if (s >= 60)
            {
                s = 0;
                n++;
            }
            if (s < 10 && n < 10) label1.Text = "0" + n + ":" + "0" + s;
            else if (s < 10 && n >= 10) label1.Text = n + ":" + "0" + s;
            else label1.Text = "0" + n + ":" + s;
            if (timer2.Enabled == true)
            {
                //определение победы
                for (int i = 0; i < mas1.Length; i++)
                {
                    if (mas1[i] == mas2[i]) ca = true;
                    else
                    {
                        ca = false;
                        break;
                    }

                }
                if (ca == true)
                {
                    ca = false;
                    timer2.Enabled = false;
                    n = 0;
                    s = 0;
                    MessageBox.Show("вы победили!!");
                }
            }
        }

// включаем таймер для перемешивания цифр 
        private void button1_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                button1.Text = "ПЕРЕМЕШАТЬ";
            }
            else
            {
                timer1.Enabled = true;
                button1.Text = "СТОП";
            }
        }

        //функция для осуществления перемешивания цифр
        public void Mix(int[] mas2)
        {
            Predic pred = new Predic();
            int nulpos = Array.FindIndex(mas2, pred.IsNul) + 1;
            Random r = new Random();
            switch (r.Next(4))
            {
                case 0:
                    if (nulpos + 1 < 16 && (float) (nulpos)%4 != 0)
                    {
                        mas2[nulpos - 1] = mas2[nulpos];
                        mas2[nulpos] = 0;
                    }
                    DrawPuzzle(mas2);
                    break;
                case 1:
                    if (nulpos > 1 && (float) (nulpos - 1)%4 != 0)
                    {
                        mas2[nulpos - 1] = mas2[nulpos - 2];
                        mas2[nulpos - 2] = 0;
                    }
                    DrawPuzzle(mas2);
                    break;
                case 2:
                    if (nulpos + 4 < 16)
                    {
                        mas2[nulpos - 1] = mas2[nulpos + 3];
                        mas2[nulpos + 3] = 0;
                    }
                    DrawPuzzle(mas2);
                    break;
                case 3:
                    if (nulpos - 4 > 0)
                    {
                        mas2[nulpos - 1] = mas2[nulpos - 5];
                        mas2[nulpos - 5] = 0;
                    }
                    DrawPuzzle(mas2);
                    break;
            }
            ;

        }

        //рисуем цифры
        public void DrawPuzzle(int[] mas2)
        {
            draw15 d = new draw15();
            Graphics g = CreateGraphics();
            Pen pen = new Pen(Color.MediumOrchid, 3);
            Font f = new System.Drawing.Font(FontFamily.GenericSerif, 25, FontStyle.Bold);
            g.Clear(this.BackColor);
            for (int i = 0; i < 16; i++)
            {
                if (mas2[i] != 0) d.dr15(g, pen, mas2[i], i + 1, f);
            }
            g.Dispose();

        }

// работа с мышкой
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
// определение положения указателя
            Point p = PointToClient(Control.MousePosition);
            Predic pred = new Predic();
            int nulpos = Array.FindIndex(mas2, pred.IsNul) + 1;
//определение положение цифры, на которую указали мышкой
            if (p.X <= 320 && p.Y <= 320)
            {
                if (p.Y < 80) mpos = (p.X)/80 + 1;
                if (p.Y > 80 && p.Y < 160) mpos = (p.X)/80 + 5;
                if (p.Y > 160 && p.Y < 240) mpos = (p.X)/80 + 9;
                if (p.Y > 240 && p.Y < 320) mpos = (p.X)/80 + 13;
            }

            //поведение программы при переставление цифры на пустое место
            if ((mpos == (nulpos - 1) && (float) mpos%4 != 0) || (mpos == (nulpos + 1) && (float) nulpos%4 != 0)
                || mpos == (nulpos - 4) || mpos == (nulpos + 4))
            {
                mas2[nulpos - 1] = mas2[mpos - 1];
                mas2[mpos - 1] = 0;
                this.DoubleBuffered = true;
                DrawPuzzle(mas2);
            }

        }

//рисуем цифры на форме
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawPuzzle(mas2);
        }
//кнопка паузы
        private void button4_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled)
            {
                button4.Text = "ВОЗОБНОВИТЬ";
                timer2.Enabled = false;
                
            }
            else
            {

                button4.Text = "ПАУЗА";
                timer2.Enabled = true;

            }
        }
//кнопка сброс
        private void button5_Click(object sender, EventArgs e)
        {
            DrawPuzzle(mas1);
            timer2.Stop();
           label1.Text = "00:00";
            n = 0;
            s = 0;
            timer2.Enabled = false;
            
        }
        
    }

}