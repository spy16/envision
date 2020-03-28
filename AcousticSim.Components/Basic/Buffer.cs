using Envision.Blocks;
using Envision.Blocks.CustomAttributes;
using Envision.Designer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
    [Serializable]
    public class Buffer : Blocks.BlockBase
    {

        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Acts as a Buffer for feedback loop"; }
        }

        public override string ProcessingType
        {
            get { return "Basic"; }
        }

       

        public override void Execute(EventDescription token)
        {
            OutputNodes[0].Object = (InputNodes[0].ConnectingNode == null) ? null : InputNodes[0].Object;
            Logger.D(this.ID + "::" + this.Name + " executed");
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Picture = Properties.Resources.Buffer;
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "input", "in") };
            OutputNodes = new List<Blocks.BlockOutputNode>() { new Blocks.BlockOutputNode(ref root, "output", "out") };
        }





      
    }
}
