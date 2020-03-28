using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{
    [Serializable]
    public class ChirpSource : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "generates Chirp signal"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        [Parameter(Description = "initial frequency")]
        public double f0 { get; set; }
        [Parameter(Description = "final frequency")]
        public double f1 { get; set; }
        [Parameter(Description = "time at which the signal must be at f1")]
        public double t1 { get; set; }

        [Parameter(Description = "Length of the chirp signal")]
        public int Length { get; set; }
        [Parameter(Description = "Sampling rate of the chirp signal")]
        public int SamplingRate { get; set; }
        [Parameter(Description = "Phase of the chirp signal in radians")]
        public double Phase { get; set; }


        public override void Execute(Blocks.EventDescription e)
        {
            //t0=t(1);
            //T=t1-t0;
            //k=(f1-f0)/T;
            //x=cos(2*pi*(k/2*t+f0).*t+phase);
            double t0 = 0;
            double T = t1;
            double k = (f1 - f0) / T;
            OpenSignalLib.Sources.Signal sig = new OpenSignalLib.Sources.Signal(this.SamplingRate, new double[Length]);
            double t = 0;
            for (int i = 0 ; i < this.Length ; i++)
            {
                t = i * (1.0 / SamplingRate);
                sig.Samples[i] = Math.Cos(2 * Math.PI * ((k / 2 * t) + f0)*t + Phase);
            }
            OutputNodes[0].Object = sig;
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.f0 = 1; this.f1 = 100; Phase = 0;
            this.Length = this.SamplingRate = 22100;
            this.t1 = 1;
            InputNodes = new List<Blocks.BlockInputNode>();
            OutputNodes = new List<Blocks.BlockOutputNode>() {
                new Blocks.BlockOutputNode(ref root, "output", "chirp","Signal object which represents the chirp signal")
            };
        }

    }
}
