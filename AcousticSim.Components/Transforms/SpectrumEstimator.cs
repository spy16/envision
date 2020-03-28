using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Transforms
{
 

    [Serializable]
    public class SpectrumEstimator : Blocks.BlockBase
    {
        public SpectrumEstimator()
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
                if (Utils.IsSignal(InputNodes[0].Object))
                {
                    var spectrum = OpenSignalLib.Transforms.Fourier.fspectrum(Utils.AsSignal(InputNodes[0].Object));
                    OutputNodes[0].Object = spectrum;
                }
            }
        }

        [Parameter]
        public string N { get; set; }


        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "inputVector", "xt") };
            OutputNodes = new List<Blocks.BlockOutputNode>() 
            { new Blocks.BlockOutputNode(ref root, "spectrum", "spec")};
        }
    }
}
