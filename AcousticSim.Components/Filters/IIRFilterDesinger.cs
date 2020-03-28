using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Components.Filters
{
    internal partial class IIRFilterDesinger : Form
    {
        private IIRConfig confi;

        public IIRFilterDesinger()
        {
        }

        public IIRFilterDesinger(IIRConfig confi)
        {
            InitializeComponent();
            cmbBandType.DataSource = Enum.GetValues(typeof(OpenSignalLib.Filters.IIR_BandType));
            cmbFilterType.DataSource = Enum.GetValues(typeof(OpenSignalLib.Filters.IIR_FilterType));
            cmbBandType.SelectedIndex = cmbFilterType.SelectedIndex = 0;
            if (confi != null)
            {
                cmbBandType.SelectedIndex = (int)confi.BandType;
                cmbFilterType.SelectedIndex = (int)confi.FilterType;
                txtF0.Text = (confi.f0 * confi.fs).ToString();
                txtFc.Text = (confi.fc * confi.fs).ToString();
                txtAp.Text = confi.Ap.ToString();
                txtAs.Text = confi.As.ToString();
                txtFs.Text = confi.fs.ToString();
            }
        }

        private void IIRFilterDesinger_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
