using Envision.Blocks;
using Envision.Designer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sinks
{
    [Serializable]
    public class Sink : BlockBase
    {
     
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Generic sink block (can consume anything)"; }
        }

        public override string ProcessingType
        {
            get { return "Sinks"; }
        }

        public override void Execute(EventDescription e)
        {
            var obj = InputNodes[0].Object;
            if ( obj == null)
            {
                obj = "null";
            }
            string outp = this.ID + "<Sink> Received: " + obj.ToString();
            Logger.D(outp);
            AppGlobals.WriteResults(outp);
        }

        protected override void CreateNodes(ref BlockBase root)
        {
            InputNodes = new List<BlockInputNode>() { new BlockInputNode(ref root, "input", "in") };
        }
    }
}
