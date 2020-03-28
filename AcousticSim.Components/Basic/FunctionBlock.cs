using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
    [Serializable]
    public class FunctionBlock : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Performs various functions such as log, sin, cos, tan etc."; }
        }

        public override string ProcessingType
        {
            get { return "Operation"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (obj != null)
                {
                    double[] vals = new double[1];
                    if (Utils.IsSignal(obj))
                    {
                        OpenSignalLib.Sources.Signal sig = Utils.AsSignal(obj);
                        vals = sig.Samples;
                    }
                    else if (Utils.IsArrayOf<double>(obj))
                    {
                        vals = (double[])obj;
                    }
                    else if (Utils.IsArrayOf<float>(obj))
                    {
                        vals = OpenSignalLib.Operations.Misc.ToArray((float[])obj);
                    }
                    else if (obj.ToString().IsNumeric())
                    {
                        vals[0] = double.Parse( obj.ToString());
                    }
                    else
                    {
                        throw new InvalidCastException("invalid input type : " + obj.GetType().Name);
                    }
                    OutputNodes[0].Object = process(vals);
                }
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Picture = Properties.Resources.FunctionBlock;
            InputNodes = new List<Blocks.BlockInputNode>(){
                new Blocks.BlockInputNode(ref root, "input", "x", "x in f(x) as in log(x). \n"+
                    " `x` can be array of numbers, a signal or a scalar value")
            };
            OutputNodes = new List<Blocks.BlockOutputNode>() {
                new Blocks.BlockOutputNode(ref root, "output", "y","y = f(x)")
            };

        }

        public enum Functions{
            sin, cos, tan, log, log10
        }

        [Parameter]
        public Functions Function
        {
            get;
            set;
        }

        [Parameter(Description = "Type of measurment for trigonometric functions")]
        public bool UseRadianAsUnit { get; set; }

        private double do_op(double x)
        {
            double ans = 0;
            switch (Function)
            {
                case Functions.sin:
                    if (!UseRadianAsUnit) {
                        x = (Math.PI / 180.0) * x;                        
                    }
                    ans = Math.Sin(x);
                    break;
                case Functions.cos:
                    if (!UseRadianAsUnit) {
                        x = (Math.PI / 180.0) * x;                        
                    }
                    ans = Math.Cos(x);
                    break;
                case Functions.tan:
                    if (!UseRadianAsUnit) {
                        x = (Math.PI / 180.0) * x;                        
                    }
                    ans = Math.Tan(x);
                    break;
                case Functions.log:
                    ans = Math.Log(x);
                    break;
                case Functions.log10:
                    ans = Math.Log10(x);
                    break;
                default:
                    break;
            }
            return ans;
        }

        public double[] process(double[] vals)
        {
            double[] ans = new double[vals.Length];
            for (int i = 0 ; i < ans.Length ; i++)
            {
                ans[i] = do_op(vals[i]);
            }
            return ans;
        }
    }
}
