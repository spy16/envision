using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sources
{
    [Serializable]
    public class GenericSignalBlock : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        private int output_nos = 1;
        public override void Init()
        {
            string[] choices = new string[] { "1", "2", "3", "4" };
            output_nos = Utils.ChoiceInput(choices,0,
                "Number of Outputs", "This block can give signals to multiple blocks. Select");
            output_nos = int.Parse(choices[output_nos]);
            base.Init();
        }

        public override string Description
        {
            get { return "Generates a range of signals including noises, sinusoidal etc."; }
        }

        public override string ProcessingType
        {
            get { return "Source"; }
        }

        public override void OpenAdditionalSettingsWindow()
        {
            GenericSourceConfig conf = new GenericSourceConfig();
            if (conf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double power =2*double.Parse(conf.txtPower.Text);
                this.Amplitude = (float)Math.Sqrt(power);
            }
        }

        [Parameter(Description="Type of the signal you want to generate")]
        public OpenSignalLib.Sources.SignalType Type { get; set; }

        [Parameter(Description = "Frequency in Hz")]
        public float Frequency { get; set; }

        [Parameter(Description = "Phase in radians")]
        public float Phase { get; set; }
        [Parameter(Description = "Sampling rate in samples/sec")]
        public int SampleRate { get; set; }
        [Parameter(Description = "Amplitude of the signal")]
        public float Amplitude { get; set; }

        public override void Execute(Blocks.EventDescription e)
        {
            if (Type == OpenSignalLib.Sources.SignalType.UserDefined)
            {
                throw new InvalidOperationException("UserDefined Signal Type is invalid in this context");
            }
            OpenSignalLib.Sources.Signal sig = new OpenSignalLib.Sources.SignalGenerator
                    (Type, Frequency, Phase, SampleRate, Amplitude);
            for (int i = 0; i < OutputNodes.Count; i++)
			{
                OutputNodes[i].Object = sig;	 
			}
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Frequency = 10; this.SampleRate = -1;
            this.Phase = 0;  this.Type = OpenSignalLib.Sources.SignalType.Sine;
            this.Amplitude = 1;
            InputNodes = new List<Blocks.BlockInputNode>();
            OutputNodes = new List<Blocks.BlockOutputNode>();
            for (int i = 0 ; i < output_nos ; i++)
            {
                string id = (i+1).ToString();
                OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output" + id, "out" + id));
            }
        }
    }
}
