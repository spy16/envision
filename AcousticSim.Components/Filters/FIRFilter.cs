
using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Components.Filters
{

    [Serializable]
    public class FIRFilter : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Acts as a finite impulse response filter"; }
        }

        public override string ProcessingType
        {
            get { return "Filters"; }
        }

        private float[] ReadCoefficientFile(string filename)
        {
            var dataFile = System.IO.File.ReadAllLines(filename);
            float[] h = new float[dataFile.Length];
            for (int i = 0 ; i < h.Length ; i++)
            {
                string line = dataFile[i];
                float val = float.Parse(line.Split(',').First());
                h[i] = val;
            }
            return h;
        }


        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                if (!System.IO.File.Exists(FileName))
                {
                    throw new Exception(this.ID + " <" + this.Name + ">  ::  File '" + FileName+"' does not exist or is invalid");
                }
                if (Utils.IsSignal(InputNodes[0].Object))
                {
                    OpenSignalLib.Filters.FIRFilter fir = new OpenSignalLib.Filters.FIRFilter(ReadCoefficientFile(FileName));
                    OpenSignalLib.Sources.Signal sig = Utils.AsSignal(InputNodes[0].Object);
                    float[] filtered = OpenSignalLib.Filters.FilterDesign.ApplyFilter(fir,
                        OpenSignalLib.Operations.Misc.ToArray(sig.Samples));
                    OutputNodes[0].Object = new OpenSignalLib.Sources.Signal(sig.SamplingRate,
                        OpenSignalLib.Operations.Misc.ToArray(filtered));
                }

            }

        }

        [Parameter]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FileName
        {
            get;
            set;
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Picture = Properties.Resources.FIRFilter;
            InputNodes = new List<Blocks.BlockInputNode>(){
                new Blocks.BlockInputNode(ref root, "input","sig")
            };
            OutputNodes = new List<Blocks.BlockOutputNode>(){
                new Blocks.BlockOutputNode(ref root, "output", "filt")
            };
        }
    }


    
}
