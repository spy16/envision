using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenSignalLib.Modems
{
    public class Modem
    {
        public static int GetConstellationSize(Types.ModulationScheme scheme)
        {
            return libliquid.digital_get_max_symbol(scheme);
        }

        public static int GetBitsPerSymbol(Types.ModulationScheme scheme)
        {
            return (int) (Math.Log(libliquid.digital_get_max_symbol(scheme)) / Math.Log(2));
        }

        private Types.ModulationScheme _scheme;
        private int _constellation_size = 0;

        public int ConstellationSize
        {
            get { return this._constellation_size; }
        }

        public int BitsPerSymbol
        {
            get { return GetBitsPerSymbol(_scheme); }
        }

        public string ModulationScheme
        {
            get { return _scheme.ToString(); }
        }

        public Modem(Types.ModulationScheme modem_scheme)
        {
            this._scheme = modem_scheme;
            this._constellation_size = libliquid.digital_get_max_symbol(this._scheme);
        }

        public Modem(int modem_scheme)
        {
            this._scheme = (Types.ModulationScheme) modem_scheme;
            this._constellation_size = libliquid.digital_get_max_symbol(this._scheme);
        }

        private bool isValid(int val)
        {
            return (val >= this._constellation_size);
        }
        public ComplexTypes.Complex[] Modulate(int[] symbols)
        {
            bool[] _cnt = symbols.Select(isValid).ToArray();
            if (_cnt.Contains(true))
            {
                throw new Exception("symbol value exceeds constellation size");
            }
            ComplexTypes.Complex[] _out = new ComplexTypes.Complex[symbols.Length];
            unsafe
            {
                libliquid.Complex * _out_tmp = stackalloc libliquid.Complex[symbols.Length];
                libliquid.digital_modulate(this._scheme, symbols, symbols.Length, _out_tmp);
                for (int i = 0 ; i < symbols.Length ; i++)
                {
                    _out[i] = libliquid.ToOSLComplex(_out_tmp[i]);
                }
            }
            return _out;
        }

        public int[] Demodulate(ComplexTypes.Complex[] modulated)
        {
            int[] output;// = new int[modulated.Length];
            libliquid.Complex[] _tmp_in = new libliquid.Complex[modulated.Length];
            for (int i = 0 ; i < modulated.Length ; i++)
            {
                _tmp_in[i].real = (float)modulated[i].Re;
                _tmp_in[i].imag = (float)modulated[i].Im;
            }
            unsafe
            {
                int* _tmp_out = stackalloc int[modulated.Length];
                libliquid.digital_demodulate(this._scheme, _tmp_in, _tmp_in.Length, _tmp_out);
                output = libliquid.Create<int>(_tmp_out, modulated.Length);
            }
            return output;
        }

    }
}
