using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Transforms
{
    [Serializable]
    public class iFFT : Blocks.BlockBase
    {
        public iFFT()
        {
            N = "512";
        }
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Calculates Inverse-FFT of the given vector"; }
        }

        public override string ProcessingType
        {
            get { return "Transform"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (Utils.IsArrayOf<OpenSignalLib.ComplexTypes.Complex>(obj))
                {
                    OpenSignalLib.ComplexTypes.Complex[] fft = (OpenSignalLib.ComplexTypes.Complex[])obj;
                    var vals = OpenSignalLib.Transforms.Fourier.iFFT(fft, 
                        int.Parse(AppGlobals.PyExecuteExpr(N).ToString()));
                    OutputNodes[0].Object = Utils.GetAbsolute(vals);
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
