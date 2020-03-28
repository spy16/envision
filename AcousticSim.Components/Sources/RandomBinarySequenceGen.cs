using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{

    [Serializable]
    public class RandomBinarySequenceGen : Blocks.BlockBase
    {

        public RandomBinarySequenceGen()
        {
            this.GenerateSymbols = false;
            this.BitsPerSymbol = 1;
        }
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Generates binary data stream with given bit duration"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }


        [Parameter(Description = "Length of the sequence as no.of bits")]
        public int Length { get; set; }

        [Parameter(Description = "Allows generation of integer symbols of length specified by BitsPerSymbol")]
        [Category("Symbol Generation")]
        public bool GenerateSymbols { get; set; }

        [Parameter(Description = "If Symbol generation is allowed, the sequence is padded with zeroes when " +
            "(length(sequence) mod BitsPerSymbol) != 0")]
        [Category("Symbol Generation")]
        public bool AutoPad { get; set; }

        [Parameter(Description = "Number of bits per each symbol")]
        [Category("Symbol Generation")]
        public int BitsPerSymbol { get; set; }

        [Parameter]
        public int SampleRate { get; set; }

        [Parameter(Description="When true, generates the random sequence only once. When false, random sequence is "+
            "generated on every execute request")]
        public bool OneTimeGeneration { get; set; }

        [Parameter]
        public LineCodings LineCoding { get; set; }

        public enum LineCodings
        {
            None, BipolarNRZ
        }

        static Random RandomGen = new Random();
        public static string GetRandomStream(int Length)
        {
            string data = "";
            for (int i = 0 ; i < Length ; i++)
            {
                data += ((int)Math.Round( RandomGen.NextDouble())).ToString(); 
            }
            return data;
        }

        public double[] GetLineCodedStream()
        {
            string DataStream = GetRandomStream(Length) ;
            List<double> data = new List<double>();
            if (!GenerateSymbols)
            {
                double tmp = 0;
                foreach (var item in DataStream)
                {
                    while (tmp < 1)
                    {
                        if (LineCoding == LineCodings.None) data.Add((item == '0' ? 0 : 1));
                        else if (LineCoding == LineCodings.BipolarNRZ) data.Add((item == '0' ? -1 : 1));
                        tmp += (1.0 / SampleRate);
                    }
                    tmp = 0;
                }
            }
            else
            {
                if (AutoPad && ((DataStream.Length % BitsPerSymbol) != 0))
                {
                    while ((DataStream.Length % BitsPerSymbol) != 0)
                    {
                        DataStream += "0";
                    }
                }
                int offset = 0;
                while (offset < DataStream.Length)
                {
                    string chunk = DataStream.Substring(offset, BitsPerSymbol);
                    data.Add(Convert.ToInt64(chunk, 2));
                    offset += BitsPerSymbol;
                }
            }
            return data.ToArray();
        }

        public override void Execute(Blocks.EventDescription e)
        {
            OpenSignalLib.Sources.Signal s;
            if (GenerateSymbols)
            {
                s = new OpenSignalLib.Sources.Signal((float)1, GetLineCodedStream());
            }
            else
            {
                s = new OpenSignalLib.Sources.Signal((float)this.SampleRate, GetLineCodedStream());
            }
            OutputNodes[0].Object = s;
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            SampleRate = 22100; LineCoding = LineCodings.None; OneTimeGeneration = false;
            Length = 10;
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "bin_output", "bin"));
        }
    }
}
