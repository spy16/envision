using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{

    [Serializable]
    public class MUX21 : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Acts as a multiplexer"; }
        }

        public override string ProcessingType
        {
            get { return "SignalRouting"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            var sel = InputNodes[2].Object;
            var in1 = InputNodes[0].Object;
            var in2 = InputNodes[1].Object;
            if (Utils.IsSignal(sel) && Utils.IsSignal(in1) && Utils.IsSignal(in2))
            {
                OpenSignalLib.Sources.Signal s1 = Utils.AsSignal(in1);
                OpenSignalLib.Sources.Signal s2 = Utils.AsSignal(in2);
                OpenSignalLib.Sources.Signal ctrl = Utils.AsSignal(sel);
                if (s1.Length == s2.Length && s1.Length == ctrl.Length)
                {

                }
            }
            else
            {
                if (sel == null || false == (bool)sel)
                {
                    OutputNodes[0].Object = InputNodes[1].Object;
                }
                else
                {
                    OutputNodes[0].Object = InputNodes[0].Object;
                }
            }
        }

        [Parameter]
        public bool Interpolate { get; set; }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Interpolate = true;
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "input1", "in1"));
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "input2", "in2"));
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "select", "sel"));
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output", "out"));
        }
    }
}
