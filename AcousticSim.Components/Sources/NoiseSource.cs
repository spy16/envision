using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{
    [Serializable]
    public class NoiseSource : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Generates different Noise"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        public enum NoiseTypes
        {
            White = OpenSignalLib.Sources.SignalType.WhiteNoise,
            Gaussian = OpenSignalLib.Sources.SignalType.GaussNoise,
            Digital = OpenSignalLib.Sources.SignalType.DigitalNoise
        }

        public override void Execute(Blocks.EventDescription e)
        {
            float fs = float.Parse(Utils.Parse(SamplingRate, "SamplingRate").ToString());
            OpenSignalLib.Sources.Signal s = new OpenSignalLib.Sources.SignalGenerator(
                (OpenSignalLib.Sources.SignalType)this.NoiseType, 100, 0, fs,1,(int)fs);
            OutputNodes[0].Object = s;
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            SamplingRate = "22100";
            NoiseType = NoiseTypes.White;
            OutputNodes = new List<Blocks.BlockOutputNode>() { new Blocks.BlockOutputNode(ref root, "output", "out") };
        }

        [Parameter]
        public NoiseTypes NoiseType { get; set; }

        [Parameter]
        public string SamplingRate { get; set; }
    }
}
