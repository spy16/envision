
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
    public class IIRFilter : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Acts as a infinite impulse response (IIR) filter"; }
        }

        public override string ProcessingType
        {
            get { return "Filters"; }
        }

        public override void OpenAdditionalSettingsWindow()
        {
            IIRFilterDesinger fdesigner = new IIRFilterDesinger(confi);
            if (fdesigner.ShowDialog() == DialogResult.OK)
            {
                confi = new IIRConfig();
                confi.fs = float.Parse(fdesigner.txtFs.Text);
                confi.f0 = float.Parse(fdesigner.txtF0.Text);
                confi.fc = float.Parse(fdesigner.txtFc.Text);
                confi.f0 = confi.f0 / confi.fs;
                confi.fc = confi.fc / confi.fs;
                confi.Ap = float.Parse(fdesigner.txtAp.Text);
                confi.As = float.Parse(fdesigner.txtAs.Text);
                confi.n = int.Parse(fdesigner.txtOrder.Text);
                confi.BandType = (OpenSignalLib.Filters.IIR_BandType)fdesigner.cmbBandType.SelectedValue;
                confi.FilterType = (OpenSignalLib.Filters.IIR_FilterType)fdesigner.cmbFilterType.SelectedValue;
            }
        }

        private Dictionary<float, float> ReadCoefficientFile(string filename)
        {
            Dictionary<float, float> coeffs = new Dictionary<float, float>();
            var dataFile = System.IO.File.ReadAllLines(filename);
            for (int i = 0 ; i < dataFile.Length ; i++)
            {
                string[] vars = dataFile[i].Split(',');
                if (vars.Length < 2)
                {
                    throw new Exception("File `" + filename + "` not in correct format. \n" +
                    "Each line should contain two decimal numbers separated by comma");
                }
                else
                {
                    coeffs.Add(float.Parse(vars[0]), float.Parse(vars[1]));
                }
            }
            return coeffs;
        }


        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                if (confi == null && !System.IO.File.Exists(FileName))
                {
                    throw new Exception(this.ID + " <" + this.Name + ">  ::  File '" + FileName + "' does not exist or is invalid");
                }
                if (Utils.IsSignal(InputNodes[0].Object))
                {
                    OpenSignalLib.Filters.IIRFilter iir;
                    if (confi == null) // use co-efficients file
                    {
                        var coeffs = ReadCoefficientFile(FileName);
                        iir = new OpenSignalLib.Filters.IIRFilter(coeffs.Keys.ToArray(),
                            coeffs.Values.ToArray());
                    }
                    else // use desinger specifications
                    {
                        iir = OpenSignalLib.Filters.FilterDesign.DesignIIR(confi.FilterType, confi.BandType, confi.n,
                            confi.fc, confi.f0, confi.Ap, confi.As);
                    }
                    OpenSignalLib.Sources.Signal sig = Utils.AsSignal(InputNodes[0].Object);
                    float[] filtered = OpenSignalLib.Filters.FilterDesign.ApplyFilter(iir,
                        OpenSignalLib.Operations.Misc.ToArray(sig.Samples));
                    OutputNodes[0].Object = new OpenSignalLib.Sources.Signal(sig.SamplingRate,
                        OpenSignalLib.Operations.Misc.ToArray(filtered));
                }

            }

        }

        private string _file = "";
        private IIRConfig confi = null;

        [Parameter]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FileName
        {
            get { return _file; }
            set
            {
                _file = value;
                confi = null;
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Picture = Properties.Resources.IIRFilter;
            InputNodes = new List<Blocks.BlockInputNode>(){
                new Blocks.BlockInputNode(ref root, "input","sig")
            };
            OutputNodes = new List<Blocks.BlockOutputNode>(){
                new Blocks.BlockOutputNode(ref root, "output", "filt")
            };

        }
    }


    [Serializable]
    internal class IIRConfig
    {
        public OpenSignalLib.Filters.IIR_BandType BandType { get; set; }
        public OpenSignalLib.Filters.IIR_FilterType FilterType { get; set; }
        public float fc { get; set; }
        public float f0 { get; set; }
        public float As { get; set; }
        public float Ap { get; set; }



        public float fs { get; set; }

        public int n { get; set; }
    }




}
