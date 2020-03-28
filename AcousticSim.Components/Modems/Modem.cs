using Envision.Blocks.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Modems
{
    [Serializable]
    public class Modem : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Modulates/Demodulates input signal or vector"; }
        }

        public override string ProcessingType
        {
            get { return "Modems"; }
        }

        public enum OutFormat
        {
            BinaryStream, Symbols
        }

        [Parameter]
        private OutFormat OutputFormat { get; set; }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (Utils.IsSignal(obj))
                {
                    obj = Utils.AsSignal(obj).Samples;
                }
                if (obj != null)
                {
                    if (Utils.IsArrayOf<double>(obj) && Operation == Operations.Modulate)
                    {
                        double[] vars = ((double[])obj);
                        int[] symbols = new int[vars.Length];
                        for (int i = 0 ; i < symbols.Length ; i++)
                        {
                            symbols[i] = (int)vars[i];
                        }
                        var m = new OpenSignalLib.Modems.Modem(this.Scheme);
                        OutputNodes[0].Object = m.Modulate(symbols);
                    }
                    else if (Utils.IsArrayOf<OpenSignalLib.ComplexTypes.Complex>(obj) || Operation == Operations.Demodulate)
                    {
                        var m = new OpenSignalLib.Modems.Modem(this.Scheme);
                        OpenSignalLib.ComplexTypes.Complex[] vals = (OpenSignalLib.ComplexTypes.Complex[])obj;
                        int[] _tmp = m.Demodulate(vals);
                        double[] syms = new double[_tmp.Length];
                        for (int i = 0 ; i < _tmp.Length ; i++)
                        {
                            syms[i] = (double)_tmp[i];
                        }
                        if (OutputFormat == OutFormat.Symbols)
                        {
                            OutputNodes[0].Object = syms;
                        }
                        else
                        {
                            double[] bits = new double[(int)(syms.Length * Math.Log( m.ConstellationSize) / Math.Log(2))];
                            int j = 0;
                            for (int i = 0 ; i < syms.Length ; i++)
                            {
                                string res = Convert.ToString((int)syms[i], 2);
                                while (res.Length < m.BitsPerSymbol)
                                {
                                    res = "0" + res;
                                }
                                foreach (var item in res)
                                {
                                    bits[j++] = (item == '0') ? 0 : 1;
                                }
                            }
                            OutputNodes[0].Object = bits;

                        }
                    }
                    else
                    {
                        throw new Exception("invalid input type: " + obj.GetType().Name);
                    }
                }
            }
        }

        public enum Operations
        {
            Modulate, Demodulate
        }

        [Parameter]
        public OpenSignalLib.Types.ModulationScheme Scheme { get; set; }



        private Operations _op;
        [Parameter]
        public Operations Operation
        {
            get { return _op; }
            set
            {
                _op = value;
                if (_op == Operations.Demodulate)
                {
                    var d = Utils.ChoiceInput(new string[] { "Bit Stream", "Symbol Stream" }, 0, "Output Format",
                        "Defines the format for the demodulated data");
                    if (d == 0)
                    {
                        OutputFormat = OutFormat.BinaryStream;
                    }
                    else
                    {
                        OutputFormat = OutFormat.Symbols;
                    }
                }
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            Operation = Operations.Modulate;
            Scheme = OpenSignalLib.Types.ModulationScheme.QPSK;
            OutputFormat = OutFormat.BinaryStream;
            InputNodes = new List<Blocks.BlockInputNode>() { 
                new Blocks.BlockInputNode(ref root, "data", "din", "Integer symbols in range (0-(M-1)) (int[])") };
            OutputNodes = new List<Blocks.BlockOutputNode>() { 
                new Blocks.BlockOutputNode(ref root, "mod_out", "mod", "modulated (Complex[]) /demodulated(int[]) output") };
        }
    }
}
