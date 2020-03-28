using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Transforms
{
    [Serializable]
    public class FFT : Blocks.BlockBase
    {
        public FFT()
        {
            N = "512";
        }
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Calculates FFT of the given signal or vector"; }
        }

        public override string ProcessingType
        {
            get { return "Transform"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                double[] values = new double[0];
                if (Utils.IsSignal(InputNodes[0].Object))
                {
                    values = Utils.AsSignal(InputNodes[0].Object).Samples;
                }
                var vals = OpenSignalLib.Transforms.Fourier.FFT(values,
                    int.Parse(AppGlobals.PyExecuteExpr(N).ToString()));
                if (OutputAbsoluteValues)
                {
                    double[] vec = Utils.GetAbsolute(vals);
                    OutputNodes[0].Object = vec;
                }
                else
                {
                    OutputNodes[0].Object = vals;
                }
                
            }
        }

        [Parameter]
        public string N { get; set; }

        [Parameter]
        public bool OutputAbsoluteValues { get; set; }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "inputVector", "xt") };
            OutputNodes = new List<Blocks.BlockOutputNode>() { new Blocks.BlockOutputNode(ref root, "outputVector", "Xf") };
        }
    }
}
