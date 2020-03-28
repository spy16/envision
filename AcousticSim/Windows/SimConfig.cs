using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    public partial class SimConfig : Form
    {
        public SimConfig()
        {
            InitializeComponent();
        }

        private void SimConfig_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 8;

        }
    }
}
