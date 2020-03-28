using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
    [Serializable]
    public class CalcPower : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Calculates the power of the input signal"; }
        }

        public override string ProcessingType
        {
            get { return "Basic"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (Utils.IsSignal(obj))
                {
                    OpenSignalLib.Sources.Signal sig = Utils.AsSignal(obj);
                    double power = 0.0;
                    for (int i = 0 ; i < sig.Samples.Length ; i++)
                    {
                        power += (sig.Samples[i] * sig.Samples[i]);
                    }
                    power = (1.0 / sig.Samples.Length) * power;
                    OutputNodes[0].Object = power;
                }
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>(){
               new Blocks.BlockInputNode(ref root, "input_signal", "sigin", "Input signal")
           };
            OutputNodes = new List<Blocks.BlockOutputNode>(){
                new     Blocks.BlockOutputNode(ref root, "avg_power" , "avgP", "Average power of the input signal")
            };
        }
    }
}
