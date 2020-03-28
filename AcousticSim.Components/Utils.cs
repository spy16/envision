using Envision.Blocks;
using Envision.Components.Visualization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Envision.Components
{
    public static class Utils
    {

        public static int ChoiceInput(string[] choices,int default_opt, string title, string description = "")
        {
            ChoiceBox chb = new ChoiceBox();
            chb.Text = title;
            chb.lblDesc.Text = description;
            foreach (var item in choices)
            {
                chb.comboBox1.Items.Add(item);
            }
            chb.comboBox1.SelectedIndex = default_opt;
            if (chb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return chb.comboBox1.SelectedIndex;
            }
            return -1;
        }

        public static string StringInput(string title, string Description = "", string _default = "")
        {
            TextInputForm f = new TextInputForm();
            f.Text = title;
            if (Description == "")
            {
                f.Height = 104;
                f.lblDescription.Visible = false;
            }
            else
            {
                f.Height = 158;
                f.lblDescription.Text = Description;
            }
            string num = _default;
            f.textBox1.Text = num.ToString();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                num = f.textBox1.Text;
            }
            return num;
        }


        public static int IntegerInput(string title, string Description = "", int _default = 0)
        {
            TextInputForm f = new TextInputForm();
            f.Text = title;
            if (Description == "")
            {
                f.Height = 104;
                f.lblDescription.Visible = false;
            }
            else
            {
                f.Height = 158;
                f.lblDescription.Text = Description;
            }
            int num = _default;
            f.textBox1.Text = num.ToString();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                num = int.Parse(f.textBox1.Text);
            }
            return num;
        }

        [Serializable]
        public struct PlotControl
        {
            [NonSerialized]
            private ZedGraphControl _control;
            [NonSerialized]
            private System.Windows.Forms.Form _window;
            public LineItem Curve { get; set; }
            public PointPairList Points { get; set; }
            public ZedGraphControl Control
            {
                get { return _control; }
                set { _control = value; }
            }
            public System.Windows.Forms.Form Window
            {
                get { return _window; }
                set { _window = value; }
            }
        }

        public static PlotControl Plot(string title, double[] x, double[] y, bool ShowPlot = true, bool useDockableWindow = true)
        {
            System.Windows.Forms.Form win = new System.Windows.Forms.Form();
            if (useDockableWindow) win = new PlotterWindowDockable();
            win.Text = title;
            ZedGraph.ZedGraphControl zed = new ZedGraph.ZedGraphControl();
            zed.Dock = System.Windows.Forms.DockStyle.Fill;
            win.Controls.Add(zed);
            GraphPane cPane = zed.GraphPane;
            PointPairList lst = new PointPairList(x, y);
            LineItem curve = cPane.AddCurve(title, lst, Color.Blue);
            cPane.Title.Text = title;
            curve.Symbol.Type = SymbolType.None;
            if (ShowPlot)
            {
                if (useDockableWindow)
                {
                    var w = (PlotterWindowDockable)win;
                    AppGlobals.ShowWin(w, WeifenLuo.WinFormsUI.Docking.DockState.Float);
                }
                else
                {
                    win.Show();
                }

            }
            PlotControl p = new PlotControl();
            p.Curve = curve;
            p.Control = zed;
            p.Window = win;
            p.Points = lst;
            return p;
        }

        public static object Parse(string expr, string hint)
        {
            try
            {
                object result = AppGlobals.PyExecuteExpr(expr);
                return result;
            } catch (Exception ex)
            {
                string msg = "Error while evaluating `" + hint + "` (" + ex.Message + ")";
                Exception tmp = (Exception)Activator.CreateInstance(ex.GetType(), msg);
                throw tmp;
            }
        }



        public static bool IsSignal(object sig)
        {
            if (sig == null) return false;
            return (sig.GetType().BaseType == typeof(OpenSignalLib.Sources.Signal)
                     || sig.GetType().BaseType == typeof(OpenSignalLib.Sources.AbstractSignal));
        }

        public static OpenSignalLib.Sources.Signal AsSignal(object a)
        {
            return (OpenSignalLib.Sources.Signal)a;
        }

        public static OpenSignalLib.Sources.Signal SelectLongest(OpenSignalLib.Sources.Signal a, OpenSignalLib.Sources.Signal b)
        {
            if (a.Samples.Length > b.Samples.Length)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public static OpenSignalLib.Sources.Signal SelectOther(OpenSignalLib.Sources.Signal a,
            OpenSignalLib.Sources.Signal b, OpenSignalLib.Sources.Signal selected)
        {
            if (a == selected)
            {
                return b;
            }
            else
            {
                return a;
            }
        }


        public static bool IsArrayOf<T>(object obj)
        {
            if (obj == null) return false;
            return (obj.GetType().IsArray && obj.GetType().GetElementType() == typeof(T));
        }



        internal static double[] GetAbsolute(OpenSignalLib.ComplexTypes.Complex[] vals)
        {
            double[] vec = new double[vals.Length];
            for (int i = 0 ; i < vals.Length ; i++)
            {
                vec[i] = vals[i].GetModulus();
            }
            return vec;
        }

        internal static bool IsInitialCondition(object _input2)
        {
            if (_input2 != null)
            {
                return (_input2.GetType() == typeof(Envision.InitialCondition));
            }
            return false;
        }
    }
}
