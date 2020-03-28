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
    public class BinaryDataStream : Blocks.BlockBase
    {

        public BinaryDataStream()
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

        #region [ Parameters ]

        #region [ Symbol Generation Control ]
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
        #endregion

        [Parameter]
        public int SampleRate { get; set; }

        [Parameter]
        public string DataStream { get; set; }
#endregion


        public double[] GetLineCodedStream()
        {
            List<double> data = new List<double>();
            if (!GenerateSymbols)
            {
                foreach (var item in DataStream)
                {
                    data.Add((item == '0' ? 0 : 1));
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
                    data.Add(Convert.ToInt64(chunk,2));
                    offset += BitsPerSymbol;
                }
            }
            return data.ToArray();
        }

        public override void Execute(Blocks.EventDescription e)
        {
            OpenSignalLib.Sources.Signal s;
            if (OutputNodes[0].ConnectingNode != null)
            {
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
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            SampleRate = 22100; DataStream = "0101";
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "bin_output", "bin"));
        }
    }
}
