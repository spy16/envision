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

namespace Envision.Components.Sources
{
    [Serializable]
    public class PythonCodeBlock : Blocks.BlockBase
    {
        public PythonCodeBlock()
        {
            this.Code = "None # (write only expressions here)";
        }
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Executes IronPython code"; }
        }

        public override Blocks.BlockBase.ProcessingTypeEnum ProcessingType
        {
            get { return ProcessingTypeEnum.Source; }
        }

        [Parameter]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Code { get; set; }

        public override void Execute(EventDescription token)
        {
            if (this.LastEvent != token)
            {
                OutputNodes[0].Object = AppGlobals.PyExecuteExpr(Code);
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            OutputNodes = new List<Blocks.BlockOutputNode>() { new Blocks.BlockOutputNode(ref root, "output", "out")};
        }

    }
}
