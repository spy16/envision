using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
    [Serializable]
    public class SymbolFormatter : Blocks.BlockBase
    {
        public override string Name
        {
            get { return GetType().Name; }
        }

        public override string Description
        {
            get { return "Generates integer symbols from a bitstream or vice versa"; }
        }

        public override string ProcessingType
        {
            get { return "DataManagement"; }
        }


        public enum InputTypes
        {
            BitStream, Symbols
        }

        [Parameter]
        public InputTypes ConversionFrom { get; set; }

        [Parameter(Description = "Number of bits that make up a symbol")]
        public int BitsPerSymbol { get; set; }


        [Parameter]
        public bool AutoPad { get; set; }

        [Parameter(Description = "If true, Pads the sequence by adding zeros at the starting of the sequence. " +
                               "Otherwise, zeros are added at the end")]
        public bool PrePad { get; set; }


        public double[] ConvertToSymbols(double[] bits)
        {
            List<double> data = new List<double>();
            string DataStream = "";
            for (int i = 0 ; i < bits.Length ; i++)
            {
                DataStream += (bits[i] <= 0) ? '0' : '1';
            }
            if (AutoPad && ((DataStream.Length % BitsPerSymbol) != 0))
            {
                while ((DataStream.Length % BitsPerSymbol) != 0)
                {
                    if (PrePad)
                    {
                        DataStream = "0" + DataStream;
                    }
                    else
                    {
                        DataStream += "0"; // postpad
                    }
                }
            }
            int offset = 0;
            while (offset < DataStream.Length)
            {
                string chunk = DataStream.Substring(offset, BitsPerSymbol);
                data.Add(Convert.ToInt64(chunk, 2));
                offset += BitsPerSymbol;
            }
            return data.ToArray();
        }


        public override void Execute(Blocks.EventDescription e)
        {
            var input = InputNodes[0].Object;
            if (Utils.IsSignal(input))
            {
                input = Utils.AsSignal(input).Samples;
            }
            if (Utils.IsArrayOf<double>(input))
            {
                if (ConversionFrom == InputTypes.Symbols)
                {
                    double[] syms = (double[])InputNodes[0].Object;
                    double[] bits = new double[(int)(syms.Length * BitsPerSymbol)];
                    int j = 0;
                    for (int i = 0 ; i < syms.Length ; i++)
                    {
                        string res = Convert.ToString((int)syms[i], 2);
                        foreach (var item in res)
                        {
                            bits[j++] = (item == '0') ? 0 : 1;
                        }
                    }
                    OutputNodes[0].Object = bits;
                }
                else
                {
                    double[] bits = (double[])input;
                    double[] syms = ConvertToSymbols(bits);
                    OutputNodes[0].Object = syms;
                }
            }

        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.BitsPerSymbol = 1;
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "instream", "bin"));
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "outstream", "syms"));
        }
    }
}
