using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sinks
{
    [Serializable]
    public class ExportToTextFile : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Exports data into a file"; }
        }

        public override string ProcessingType
        {
            get { return "Sinks"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (obj != null)
                {
                    if (Utils.IsSignal(obj))
                    {
                        OpenSignalLib.Sources.Signal sig = Utils.AsSignal(obj);
                        var fout = new System.IO.StreamWriter(FileName);
                        for (int i = 0 ; i < sig.Samples.Length ; i++)
                        {
                            fout.WriteLine(sig.Samples[i].ToString());
                        }
                        fout.Close();
                    }
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
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "datain", "din") };
        }
    }
}
