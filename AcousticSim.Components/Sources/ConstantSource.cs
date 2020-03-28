using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Components.Sources
{
    [Serializable]
    public class ConstantSource : Blocks.BlockBase
    {

        private bool _inlcludeInput = false;
        public ConstantSource()
        {
            Value = "None";            
        }

        public override void Init()
        {
            DialogResult d = MessageBox.Show("Inlcude input terminal?", "Input?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                _inlcludeInput = true;
            }
            Blocks.BlockBase root = this;
            CreateNodes(ref root);
        }

        public override string Name
        {
            get { return "Constant"; }
        }

        public override string Description
        {
            get { return "Acts as a flexible value source"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            OutputNodes[0].Object = AppGlobals.PyExecuteExpr(Value);
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            if(_inlcludeInput)
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "input","in")};
            OutputNodes = new List<Blocks.BlockOutputNode>(){
                new Blocks.BlockOutputNode(ref root, "output", "out")
            };
        }


        [Parameter(Description = "Expression or a value")]
        [Category("Configuration")]
        public string Value { get; set; }

    }
}
