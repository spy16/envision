using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Envision.Blocks;
using Envision.Blocks.CustomAttributes;

namespace Envision.Components.Sources
{

    [Serializable]
    public class SinusoidalSource : BlockBase
    {
        public SinusoidalSource()
        {
            Amplitude = "1"; Phase = "0";
            Frequency = "10"; SamplingRate = "22100";
            Length = "-1";
            this.Picture = Properties.Resources.Sinusoidal;
        }
        public override string Name
        {
            get { return "SinusoidalSource"; }
        }

        public override string Description
        {
            get { return "Sinusoidal Signal Generator"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }


        public override void Execute(EventDescription token)
        {
            float frequency = float.Parse(Utils.Parse(Frequency, "Frequency").ToString());
            float phase = (float)((float.Parse(Utils.Parse(Frequency, "Frequency").ToString())) * (Math.PI / 180.0f));
            float fs = float.Parse(Utils.Parse(SamplingRate, "SamplingRate").ToString());
            float A = float.Parse(Utils.Parse(Amplitude, "Amplitude").ToString());
            int length = -1;
            if (Length.EndsWith("s"))
            {
                length = (int)(float.Parse(Utils.Parse(Length.Trim('s'), "NumberOfSamples").ToString()) * fs);
            }
            else
            {
                length = int.Parse(Utils.Parse(Length, "NumberOfSamples").ToString());
            }

            OpenSignalLib.Sources.Signal s = new OpenSignalLib.Sources.Sinusoidal(frequency, phase, fs, A, length);
            OutputNodes[0].Object = s;
        }

        protected override void CreateNodes(ref BlockBase root)
        {
            InputNodes = new List<BlockInputNode>();
            OutputNodes = new List<BlockOutputNode>() {
                new BlockOutputNode(ref root, "output", "out")
            };
        }

        public override BlockBase Clone()
        {
            return MemberwiseClone();
        }

        public override BlockBase CloneWithLinks()
        {
            return MemberwiseCloneWithLinks();
        }

        [Parameter(Description = "Number of samples to be generated. (Value of -1 means auto)")]
        public string Length { get; set; }

        [Parameter(Description = "Frequency of signal to be generated in Hz")]
        public string Frequency { get; set; }

        [Parameter(Description = "Amplitude of the signal")]
        public string Amplitude { get; set; }

        [Parameter(Description = "Phase of the signal in degrees")]
        public string Phase { get; set; }

        [Parameter(Description = "Sampling rate Fs in samples/second (Value of -1 means auto, in that case fs = 30 x frequency)")]
        public string SamplingRate { get; set; }
    }
}
