using Envision.Blocks;
using Envision.Blocks.CustomAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{

    [Serializable]
    public class ArithmeticOperator : Blocks.BlockBase
    {
        private Guid previous_token = Guid.NewGuid();

        public ArithmeticOperator()
        {
            this.Picture = Properties.Resources.Arithmetic;
        }
        public override string Name
        {
            get { return "ArithmeticOperator"; }
        }

        public override string Description
        {
            get { return "Operates on two inputs and performs an arithmeti operations as specified"; }
        }

        public override string ProcessingType
        {
            get { return "Basic"; }
        }



        private double DoOp(double a, double b)
        {
            double retval = 0;
            switch (Op)
            {
                case Operation.Addition:
                    retval = a + b;
                    break;
                case Operation.Subtraction:
                    retval = a - b;
                    break;
                case Operation.Multiplication:
                    retval = a * b;
                    break;
                case Operation.Divide:
                    retval = a / b;
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation");
            }
            return retval;
        }
        public override void Execute(EventDescription token)
        {
            if (this.LastEvent != token)
            {
                var _input1 = InputNodes[0].Object;
                var _input2 = InputNodes[1].Object;
                if (_input1 != null && _input2 != null)
                {
                    dynamic a  = 0;
                    dynamic b = 0;
                    if (_input1.ToString().IsNumeric() && _input2.ToString().IsNumeric())
                    {
                         a = double.Parse(_input1.ToString());
                        b = double.Parse(_input2.ToString());
                        OutputNodes[0].Object = DoOp(a, b);
                    }
                    else if (Utils.IsSignal(_input1) && Utils.IsSignal(_input2))
                    {
                       var in1 = (OpenSignalLib.Sources.Signal) (Utils.AsSignal(_input1));
                        var in2 = (OpenSignalLib.Sources.Signal)(Utils.AsSignal(_input2));
                        var longest = (OpenSignalLib.Sources.Signal)Utils.SelectLongest(in1,in2);
                        var shortest = (OpenSignalLib.Sources.Signal)Utils.SelectOther(in1, in2, longest);
                        OpenSignalLib.Sources.Signal s = new OpenSignalLib.Sources.Signal();
                        s.Samples = new double[longest.Samples.Length];
                        s.SamplingRate = Math.Min(longest.SamplingRate, shortest.SamplingRate);
                        for (int i = 0 ; i < shortest.Samples.Length ; i++)
                        {
                            a = longest.Samples[i];
                            b = shortest.Samples[i];
                            s.Samples[i] = DoOp(a, b);
                        }
                        OutputNodes[0].Object = s;
                    }
                    else if ( Utils.IsInitialCondition(_input1) || Utils.IsInitialCondition(_input2))
                    {
                        InitialCondition inC = (InitialCondition) (Utils.IsInitialCondition(_input1) ? _input1 : _input2);
                        object other = (Utils.IsInitialCondition(_input1) ? _input2 : _input1);
                        if (Utils.IsSignal(other))
                        {
                            OutputNodes[0].Object = Utils.AsSignal( other);
                        }
                    }
                    
                }
               
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>() { 
                        new Blocks.BlockInputNode(ref root, "input1", "in1"),
                        new Blocks.BlockInputNode(ref root, "input2", "in2")
            };
            OutputNodes = new List<Blocks.BlockOutputNode>() { new Blocks.BlockOutputNode(ref root, "output", "out") };

        }

        public enum Operation
        {
            Addition, Subtraction, Multiplication, Divide
        }

        private Operation _op = Operation.Addition;
        [Parameter(NameResourceName = "Operation", Description = "Operation to be performed. By Default it is Addition")]

        public Operation Op
        {
            get { return _op; }
            set { 
                _op = value;
            }
        }
        
    }

    public static class Extension
    {
        public static bool IsNumeric(this string s)
        {
            double output;
            return double.TryParse(s, out output);
        }
    }
}
