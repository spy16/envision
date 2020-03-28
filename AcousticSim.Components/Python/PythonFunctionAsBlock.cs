using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Python
{

    [Serializable]
    public class PythonFunctionAsBlock : Blocks.BlockBase
    {
        List<string> _inputs = new List<string>();
        string pf = null;
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Converts a function into a single output block"; }
        }

        public override string ProcessingType
        {
            get { return "Python"; }
        }

        public override void Init()
        {
            PythonFunctionAsBlockConfigWindow win = new PythonFunctionAsBlockConfigWindow();
            if (win.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var item in win.lstInputs.Items)
                {
                    _inputs.Add(item.ToString());
                }
                pf = win.cmbFunction.SelectedItem.ToString();
            }
            else
            {
                throw new Envision.InitFailedException();
            }
            base.Init();
        }

        public override void Execute(Blocks.EventDescription e)
        {
            IronPython.Runtime.PythonFunction fun = (IronPython.Runtime.PythonFunction)AppGlobals.PyVarGetFunc(pf);
            if (fun != null)
            {
                object[] args = new object[InputNodes.Count];
                for (int i = 0 ; i < InputNodes.Count ; i++)
                {
                    args[i] = InputNodes[i].Object;
                }
                object result = AppGlobals.InvokeFunction(fun, args);
                OutputNodes[0].Object = result;
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>();
            foreach (var item in _inputs)
            {
                InputNodes.Add(new Blocks.BlockInputNode(ref root, item, item));
            }
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output", "out"));
        }
    }
}
