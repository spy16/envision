using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Envision.Components.Sinks
{
    [Serializable]
    public class SpectrumViewer : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Plots the spectrum of the input vector/signal"; }
        }

        public override string ProcessingType
        {
            get { return "Visualization"; }
        }

        private Envision.Components.Utils.PlotControl plotControl;


        [Parameter]
        public bool ShowPointValues { get; set; }
        [Parameter]
        public bool UseDockableWindow { get; set; }

        private bool _hold = true;
        [Parameter]
        public bool Hold
        {
            get { return _hold; }
            set {
                if (_hold && plotControl.Window != null && !plotControl.Window.IsDisposed)
                {
                    plotControl.Window.Close();
                }
                _hold = value; 
            }
        }


        private void _createPlot(double[] x, double[] y)
        {
            plotControl = Utils.Plot("Spectrum (" + this.ID + ")", x, y,true, this.UseDockableWindow);
            plotControl.Curve.GetXAxis(plotControl.Control.GraphPane).Title.Text = "Frequency(Hz)";
            plotControl.Curve.GetYAxis(plotControl.Control.GraphPane).Title.Text = "Power(Linear-Scale)";
            if (ShowPointValues)
            {
                var myPane = plotControl.Control.GraphPane;
                myPane.AxisChange(plotControl.Control.CreateGraphics());

                foreach (PointPair pt in plotControl.Points)
                {
                    string label = pt.X.ToString();
                    TextObj text = new TextObj(label, (float)pt.X , (float)pt.Y ,
                    CoordType.AxisXYScale, AlignH.Left, AlignV.Bottom);
                    text.FontSpec.Border.IsVisible = false;
                    myPane.GraphObjList.Add(text);
                }
            }

        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                double[] x, y;
                var obj = InputNodes[0].Object;
                if (Utils.IsSignal(obj))
                {
                    OpenSignalLib.Sources.Signal s = Utils.AsSignal(obj);
                    Logger.D(s.SamplingRate.ToString());
                    int n = int.Parse(AppGlobals.PyExecuteExpr(N).ToString());
                    var spectrum = OpenSignalLib.Transforms.Fourier.fspectrum(s, n);
                    x = spectrum.Keys.ToArray();
                    y = spectrum.Values.ToArray();
                }
                else
                {
                    throw new Exception("invalid input type : " + obj.GetType().Name);
                }
                if (!Hold || (plotControl.Window == null || plotControl.Window.IsDisposed))
                {
                    _createPlot(x, y);
                }
                if (Hold)
                {
                    plotControl.Curve.Clear();
                    for (int i = 0 ; i < x.Length ; i++)
                    {
                        if (Scale == Scales.dB_Scale)
                        {
                            plotControl.Curve.AddPoint(x[i], 10*Math.Log10( y[i]));
                        }
                        else
                        {
                            plotControl.Curve.AddPoint(x[i], y[i]);
                        }

                    }
                    plotControl.Control.AxisChange();
                    plotControl.Control.Invalidate();
                }
            }
        }



        [Parameter]
        public string N { get; set; }


        public enum Scales{
            Linear_Scale, dB_Scale
        }
        [Parameter]
        public Scales Scale { get; set; }


        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            N = "512"; Scale = Scales.Linear_Scale;
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "inputVector", "xt") };
        }
    }
}
