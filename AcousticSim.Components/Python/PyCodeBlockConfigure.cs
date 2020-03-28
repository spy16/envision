using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Components.Basic
{
    public partial class PyCodeBlockConfigure : Form
    {
        public PyCodeBlockConfigure()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = chkAllowInput.Checked;
            cmbIPNos.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbIPNos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void PyCodeBlockConfigure_Load(object sender, EventArgs e)
        {
            cmbOutputCount.SelectedIndex = 0;
            cmbIPNos.SelectedIndex = 0;
        }

        private void chkAllowOutput_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = chkAllowOutput.Checked;
            cmbOutputCount.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
