using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PartyPreparation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog(){Filter = "файл заявки|*.partyml"};
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var fileName = sfd.FileName;
                PartyData pd = new PartyData();
                if (radioButton1.Checked)
                    pd.drinkType = DrinkType.Tea;
                else
                    pd.drinkType = DrinkType.Coffee;

                pd.Snacks = new List<SnackData>();
                foreach (SnackData sd in listBox1.Items)
                {
                    pd.Snacks.Add(sd);
                }

                XmlSerializer xs = new XmlSerializer(typeof(PartyData));
                var fileStream = File.Create(fileName);
                xs.Serialize(fileStream, pd);
                fileStream.Close();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            radioButton2.Visible = e.X < radioButton2.Location.X ||
                                   e.X > (radioButton2.Location.X + radioButton2.Size.Width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(){Filter = "заявка|*.partyml"};
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (listBox1.Items.Count > 2)
                {
                    var res = MessageBox.Show("У вас введены бутерброды, вы уверены что хотите выкинуть их?", "Предупреждение", MessageBoxButtons.YesNo);
                    if(res!=DialogResult.Yes)
                        return;
                }
                var xs = new XmlSerializer(typeof (PartyData));
                var file = File.Open(ofd.FileName,FileMode.Open);
                var pd = (PartyData)xs.Deserialize(file);
                file.Close();

                radioButton1.Checked = pd.drinkType == DrinkType.Tea;
                listBox1.Items.Clear();
                foreach (var snackData in pd.Snacks)
                {
                    listBox1.Items.Add(snackData);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sd = new SnackData
                {
                    Salo = checkBox1.Checked,
                    Jam = checkBox3.Checked,
                    Толщина = trackBar1.Value,
                };
            switch (checkBox2.CheckState)
            {
                case CheckState.Unchecked:
                    sd.Caviar = false;
                    break;
                case CheckState.Checked:
                    sd.Caviar = true;
                    break;
                case CheckState.Indeterminate:
                    sd.Caviar = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            listBox1.Items.Add(sd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Толщина бутерброда " + trackBar1.Value + "мм";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.CheckState = CheckState.Indeterminate;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = listBox1.SelectedItem != null;
        }
    }

    public class PartyData
    {
        public DrinkType drinkType { get; set; }
        public List<SnackData> Snacks { get; set; }
    }

    public class SnackData
    {
        public bool Salo { get; set; }
        public bool? Caviar { get; set; }
        public bool Jam { get; set; }
        public int Толщина { get; set; }

        public override string ToString()
        {
            var s = "Бутерброд толщиной " + Толщина+ "мм ";
            if (Salo)
                s += " с салом";
            if (Caviar.HasValue && Caviar.Value)
                s += " с икрой";
            if (Jam)
                s += " с вареньем";
            return s;
        }
    }

    public enum DrinkType
    {
        Tea,
        Coffee
    }
}
