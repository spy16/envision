using Envision.Blocks.CustomAttributes;
using OpenSignalLib.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcousticSimBlockset.Channels
{
    [Serializable]
   public class ChannelAWGN : Envision.Blocks.BlockBase
    {
        public override string Name
        {
            get { return "AWGN"; }
        }

        public override string Description
        {
            get { return "Operates as Additive White Gaussian Noise channel"; }
        }

        public override string ProcessingType
        {
            get { return "Channel"; }
        }

        [Parameter]
        public double SNRdB { get; set; }

        public override void Execute(Envision.Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var Tx = InputNodes[0].Object;
                if (Envision.Components.Utils.IsSignal(Tx))
                {
                    OpenSignalLib.Sources.Signal s = Envision.Components.Utils.AsSignal(Tx);
                    OpenSignalLib.Sources.Signal n = new OpenSignalLib.Sources.WhiteGaussianNoise
                        (s.Samples.Length, SNRdB, (int)s.SamplingRate, s.SignalEnergy());
                    OutputNodes[0].Object = s + n;
                }
                else if (Envision.Components.Utils.IsArrayOf<Complex>(Tx))
                {
                    Complex[] vals = (Complex[])Tx;
                    OpenSignalLib.Sources.Signal s1 = new OpenSignalLib.Sources.Signal(1f, new double[vals.Length]);
                    OpenSignalLib.Sources.Signal s2 = new OpenSignalLib.Sources.Signal(1f, new double[vals.Length]);
                    for (int i = 0 ; i < vals.Length ; i++)
                    {
                        s1.Samples[i] = vals[i].Re;
                        s2.Samples[i] = vals[i].Im;
                    }
                    OpenSignalLib.Sources.Signal n1 = new OpenSignalLib.Sources.WhiteGaussianNoise
                        (s1.Samples.Length, SNRdB, (int)s1.SamplingRate, s1.SignalEnergy());
                    OpenSignalLib.Sources.Signal n2 = new OpenSignalLib.Sources.WhiteGaussianNoise
                        (s2.Samples.Length, SNRdB, (int)s2.SamplingRate, s2.SignalEnergy());
                    s1 = s1 + n1;
                    s2 = s2 + n1;
                    for (int i = 0 ; i < vals.Length ; i++)
                    {
                        vals[i] = new Complex(s1.Samples[i], s2.Samples[i]);
                    }
                    OutputNodes[0].Object = vals;
                }
            }
            else
            {
                throw new InvalidOperationException("input terminal 'Tx' not connected");
            }
        }

        protected override void CreateNodes(ref Envision.Blocks.BlockBase root)
        {
            this.SNRdB = 1.0;
            InputNodes = new List<Envision.Blocks.BlockInputNode>() 
                { new Envision.Blocks.BlockInputNode(ref root, "Transmitted", "Tx") };
            OutputNodes = new List<Envision.Blocks.BlockOutputNode>() 
                { new Envision.Blocks.BlockOutputNode(ref root, "Recieved", "Rx") };
        }
    }
}
