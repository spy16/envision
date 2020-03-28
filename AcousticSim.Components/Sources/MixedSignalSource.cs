using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{
    [Serializable]
   public class MixedSignalSource : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Generates a mixed signal by adding multiple different signals"; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            OpenSignalLib.Sources.Signal sig = null;
            foreach (var item in this.Frequencies)
            {
                float f = float.Parse(item);
                if (sig == null)
                {
                    sig = new OpenSignalLib.Sources.Sinusoidal(f, 0.0f, (float)this.SampleRate, 1, this.Length);
                }
                else
                {
                    sig += new OpenSignalLib.Sources.Sinusoidal(f, 0.0f, (float)this.SampleRate, 1, this.Length);
                }
            }
            OutputNodes[0].Object = sig;
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Frequencies = new string[] { "10", "20" };
            this.Length = this.SampleRate = 22100;
            OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output", "out"));
        }

        [Parameter]
        public string[] Frequencies { get; set; }

        [Parameter]
        public int Length { get; set; }

        [Parameter]
        public int SampleRate { get; set; }
    }
}
