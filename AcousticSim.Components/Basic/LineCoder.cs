using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{

    [Serializable]
    public class LineCoder : Blocks.BlockBase
    {
        public override string Name
        {
            get { return "LineCoder"; }
        }

        public override string Description
        {
            get { return "Provides Line Coding/Decoding schemes such as NRZ, RZ etc." ; }
        }

        public override string ProcessingType
        {
            get { return  "Coding"; }
        }

        [Parameter]
        public LineCodings LineCoding { get; set; }

        [Parameter]
        public LineCodingOps Operation { get; set; }
        public enum LineCodings
        {
            None, BipolarNRZ
        }

        public enum LineCodingOps
        {
            Encode, Decode
        }

        public static void Coding_BipolarNRZ(double[] bitStream, ref double[] output, bool decode = false)
        {
            int i =0;
            foreach (var item in bitStream)
            {
                if (!decode) output[i++] = (item == 0 ? -1 : 1);
                else output[i++] = (item == -1 ? 0 : 1);
            }
        }



        public override void Execute(Blocks.EventDescription e)
        {
            var streamIn = InputNodes[0].Object;
            if (Utils.IsSignal(streamIn))
            {
                streamIn = Utils.AsSignal(streamIn).Samples;
            }
            if (Utils.IsArrayOf<double>(streamIn))
            {
                double[] DataStream = (double[])streamIn;
                double[] data = new double[DataStream.Length];
                switch (LineCoding)
                {
                    case LineCodings.BipolarNRZ:
                        Coding_BipolarNRZ(DataStream, ref data, (Operation == LineCodingOps.Encode ? false : true));                        
                        break;
                    case LineCodings.None:
                    default:
                        DataStream.CopyTo(data, 0);
                        break;
                }
                OutputNodes[0].Object = data;
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            Operation = LineCodingOps.Encode; LineCoding = LineCodings.None;
            InputNodes.Add(new Blocks.BlockInputNode(ref root, "instream", "in"));
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "outstream", "out"));
        }
    }
}
