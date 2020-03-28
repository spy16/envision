using Envision.Blocks.CustomAttributes;
using OpenSignalLib.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{
    [Serializable]
    public class ImportSignalFromText : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Reads csv files / new line separated file as a signal"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        private OpenSignalLib.Sources.Signal ReadSignalFile(string filename)
        {
            OpenSignalLib.Sources.Signal temp = new Signal();
            temp.SamplingRate = this.SampleRate;
            var dataFile = System.IO.File.ReadAllLines(filename);
            temp.Samples = new double[dataFile.Length];
            for (int i = 0 ; i < temp.Samples.Length ; i++)
            {
                string line = dataFile[i];
                double val = double.Parse(line.Split(',').First());
                temp.Samples[i] = val;
            }
            return temp;
        }


        public override void Execute(Blocks.EventDescription e)
        {
            Signal sig = ReadSignalFile(FilePath);
            OutputNodes[0].Object = sig;
        }

        [Parameter]
        public string FilePath { get; set; }

        [Parameter]
        public int SampleRate { get; set; }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.FilePath = "./data/eeg_sig.txt";
            this.SampleRate = 44100;
            OutputNodes = new List<Blocks.BlockOutputNode>(){
                new Blocks.BlockOutputNode(ref root, "output", "sig")
            };
        }
    }
}
