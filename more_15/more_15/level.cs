using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// выбор количества частей кртинки
namespace more_15
{
    public partial class level : Form
    {
        public int Level = 3;
        public level()
        {
            InitializeComponent();
        }

        private void level_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Level;
        }

        private void NUD_ValueChanged(object sender, EventArgs e)
        {
            Level = (int)numericUpDown1.Value;
        }
    }
}
