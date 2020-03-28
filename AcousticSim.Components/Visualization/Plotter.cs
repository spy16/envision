using Envision.Blocks.CustomAttributes;
using OpenSignalLib.ComplexTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Sinks
{
    [Serializable]
    public class Plotter : Blocks.BlockBase
    {
        public Plotter()
        {
            UseDockableWindow = true;
            Hold = true;
            this.Title = "Plot";
            this.XLabel = "time(s)";
            this.YLabel = "amplitude";
        }


        private int input_count = 1;


        [Parameter]
        [Category("Plot Configurations")]
        public string Title { get; set; }

        [Parameter]
        [Category("Plot Configurations")]
        public string XLabel { get; set; }

        [Parameter]
        [Category("Plot Configurations")]
        public string YLabel { get; set; }

        private bool _hold = true;
        [Parameter]
        public bool Hold
        {
            get { return _hold; }
            set
            {
                if (_hold && plotControl.Window != null && !plotControl.Window.IsDisposed)
                {
                    plotControl.Window.Close();
                }
                _hold = value;
            }
        }
        [Parameter]
        public bool UseDockableWindow { get; set; }


        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Plots the input vector"; }
        }

        public override string ProcessingType
        {
            get { return "Visualization"; }
        }


        private Envision.Components.Utils.PlotControl plotControl;

       


        private Envision.Components.Utils.PlotControl _createPlot(double[] x, double[] y)
        {
            if (!Hold || (plotControl.Window == null || plotControl.Window.IsDisposed))
            {

                plotControl = Utils.Plot(Title + " (" + this.ID + ")", x, y, true, UseDockableWindow);
                plotControl.Curve.GetXAxis(plotControl.Control.GraphPane).Title.Text = XLabel;
                plotControl.Curve.GetYAxis(plotControl.Control.GraphPane).Title.Text = YLabel;

            }
            if (Hold)
            {
                plotControl.Curve.Clear();
                for (int i = 0 ; i < x.Length ; i++)
                {
                    plotControl.Curve.AddPoint(x[i], y[i]);

                }
                plotControl.Control.AxisChange();
                plotControl.Control.Invalidate();
            }
            return plotControl;
        }
        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                double[] x, y;
                if (obj != null)
                {
                    if (Utils.IsArrayOf<double>(obj))
                    {
                        dynamic vals = obj;
                        y = (double[])vals;
                        x = new double[y.Length];
                        for (int i = 0 ; i < y.Length ; i++)
                        {
                            x[i] = (double)i;
                        }
                        var pltctrl = _createPlot(x, y);
                    }
                    else if (Utils.IsSignal(obj))
                    {
                        OpenSignalLib.Sources.Signal sig = Utils.AsSignal(obj);
                        y = sig.Samples;
                        x = new double[y.Length];
                        for (int i = 0 ; i < x.Length ; i++)
                        {
                            x[i] = i * (1.0 / sig.SamplingRate);
                        }
                        var pltctrl = _createPlot(x, y);
                    }
                    else if (obj.GetType() == typeof(Dictionary<double, double>))
                    {
                        Dictionary<double, double> vals = (Dictionary<double, double>)obj;
                        x = vals.Keys.ToArray();
                        y = vals.Values.ToArray();
                        var pltctrl = _createPlot(x, y);
                        Logger.D("Dictionary");
                    }
                    else if (Utils.IsArrayOf<Complex>(obj))
                    {
                        Complex[] vals = (Complex[])obj;
                        x = new double[vals.Length];
                        y = new double[vals.Length];
                        for (int i = 0 ; i < vals.Length ; i++)
                        {
                            x[i] = vals[i].Re;
                            y[i] = vals[i].Im;
                        }
                        var pltctrl = _createPlot(x, y);
                        pltctrl.Curve.Line.IsVisible = false;
                        pltctrl.Curve.Symbol.Type = ZedGraph.SymbolType.Circle;
                    }
                    else
                    {
                        throw new Exception("invalid input type : " + obj.GetType().Name);
                    }
                    
                }
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>() ;
            for (int i = 0 ; i < input_count ; i++)
            {
                string id = (i + 1).ToString();
                InputNodes.Add(new Blocks.BlockInputNode(ref root, "input" + id, "in" + id));
            }
        }
    }
}
