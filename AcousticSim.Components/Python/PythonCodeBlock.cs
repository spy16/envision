using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Envision.Blocks;
using Envision.Designer.Utils;
using System.Windows.Forms;

namespace Envision.Components.Basic
{
    [Serializable]
    public class PythonCodeBlock : Blocks.BlockBase
    {

        public PythonCodeBlock()
        {
            this.Picture = Envision.Designer.Utils.ApplicationUtils.ResizeTo( 
                Properties.Resources.python, 30,30);
        }


        int input_count = 0, output_count = 0;
        private string code = "";

        [NonSerialized]
        PythonEditorWindow ped;
        public override void OpenAdditionalSettingsWindow()
        {
            if (ped == null)
            {
                ped = new PythonEditorWindow();
            }
            ped.SetPyCode(code);
            if (ped.ShowDialog() == DialogResult.OK)
            {
                this.code = ped.GetPyCode();
            }
        }

        public override void Init()
        {
            root = this;
            PyCodeBlockConfigure pyWin = new PyCodeBlockConfigure();
            DialogResult d = pyWin.ShowDialog();
            if (d == DialogResult.OK)
            {
                if (pyWin.chkAllowInput.Checked)
                {
                    input_count = int.Parse(pyWin.cmbIPNos.SelectedItem.ToString());
                }
                if (pyWin.chkAllowOutput.Checked)
                {
                    output_count = int.Parse(pyWin.cmbOutputCount.SelectedItem.ToString());
                }
            }
            CreateNodes(ref root);
            
        }
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Executes IronPython code"; }
        }

        public override string ProcessingType
        {
            get { return "Python"; }
        }

        public static double[] PyToNET(object arg) {
            double[] ans = new double[0];
            if (arg.GetType() == typeof(IronPython.Runtime.List))
            {
                IronPython.Runtime.List lst = (IronPython.Runtime.List)arg;
                ans = new double[lst.Count];
                for (int i = 0 ; i < lst.Count ; i++)
                {
                    ans[i] = double.Parse(lst[i].ToString());
                }
                return ans;
            }
            return null;
        }

     

        public override void Execute(EventDescription token)
        {
            if (code == "") code = "None";
            for (int i = 0 ; i < input_count ; i++)
            {
                AppGlobals.PySetVar("in" + (i + 1).ToString(), InputNodes[i].Object);
            }
            object result = null;
            AppGlobals.PyExecute(code, Microsoft.Scripting.SourceCodeKind.Statements);
            for (int i = 0 ; i < output_count ; i++)
            {
                var name = "out" + (i + 1).ToString();
                if (AppGlobals.PyVarExists(name))
                {
                    result = AppGlobals.PyGetVar(name);
                    if (result.GetType() == typeof(IronPython.Runtime.List))
                    {
                        IronPython.Runtime.List lst = (IronPython.Runtime.List)result;
                        //var tmp = new List<object>();
                        //foreach (var item in lst)
                        //{
                        //    tmp.Add(item);
                        //}
                        result = lst;// tmp.ToArray();
                    }
                    var tmp = PyToNET( result);
                    if (tmp != null) OutputNodes[i].Object = (double[])tmp;
                    else OutputNodes[i].Object = result;
                }
                AppGlobals.PyRemVar(name);
            }
            for (int i = 0 ; i < input_count ; i++)
            {
                AppGlobals.PyRemVar("in" + (i + 1).ToString());
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<BlockInputNode>();
            for (int i = 0 ; i < input_count ; i++)
            {
                string id = (i+1).ToString();
                InputNodes.Add(new BlockInputNode(ref root, "input" + id, "in" + id));
            }
            OutputNodes = new List<Blocks.BlockOutputNode>() {  };
            for (int i = 0 ; i < output_count ; i++)
            {
                string id = (i + 1).ToString();
                OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output" + id, "out" + id));
            }
        }

    }
}
