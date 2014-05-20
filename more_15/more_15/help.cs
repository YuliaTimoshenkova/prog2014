using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// исходная картинка, как подсказка
namespace more_15
{
    public partial class help : Form
    {
        public help()
        {
            InitializeComponent();
        }

        private void HelpLoad(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageDuplicate;
        }


        public Image ImageDuplicate = null;

    }
}
