using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Modems
{

    [Serializable]
    public class BERCalculator : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Calculates the bit error rate"; }
        }

        public override string ProcessingType
        {
            get { return "Modems"; }
        }


        public override void Execute(Blocks.EventDescription e)
        {
            var input1 = InputNodes[0].Object;
            var input2 = InputNodes[1].Object;
            if (Utils.IsSignal(input1)) input1 = Utils.AsSignal(input1).Samples;
            if (Utils.IsSignal(input2)) input2 = Utils.AsSignal(input2).Samples;
            if (Utils.IsArrayOf<double>(input1) && Utils.IsArrayOf<double>(input2))
            {
                double[] actual = (double[])input1;
                double[] received = (double[])input2;
                if ((actual.Length != received.Length))
                {
                    throw new Exception("Transmitted (tx) and Received(rx) sequences are of different length");                     
                }
                double total_errors = 0;
                for (int i = 0 ; i < actual.Length ; i++)
                {
                    if (actual[i] != received[i])
                    {
                        total_errors++;
                    }
                }
                OutputNodes[0].Object = (total_errors) / (double)actual.Length;              
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "actual", "tx", "transmitted bitstream"));
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "actual", "rx", "received bitstream"));
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "ber", "ber", "scalar value representing ber"));
        }


     
    }
}
