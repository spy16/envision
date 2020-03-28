using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Components.Python
{
    public partial class PythonFunctionAsBlockConfigWindow : Form
    {
        public PythonFunctionAsBlockConfigWindow()
        {
            InitializeComponent();
            var names = AppGlobals.PyGetVarNames();
            foreach (var name in names)
            {
                if (AppGlobals.IsFunction(name))
                {
                    cmbFunction.Items.Add(name);
                }
            }
            cmbFunction.SelectedIndex = 0;
        }

        private void PythonFunctionAsBlockConfigWindow_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            IronPython.Runtime.PythonFunction pf = 
                (IronPython.Runtime.PythonFunction)AppGlobals.PyGetVar(cmbFunction.SelectedItem.ToString());
            lstInputs.Items.Clear();
            if (pf.__doc__ != null)
            {
                lblDesc.Text = "Description: \n" + pf.__doc__.ToString();
            }
            else lblDesc.Text = "Description: N/A";
            int var_count = 0;
            foreach (var item in pf.func_code.co_varnames)
            {
                lstInputs.Items.Add(item.ToString());
                var_count++;
                if (var_count >= pf.func_code.co_argcount) break;
            }
        }
    }
}
