using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.Operations;

namespace OpenSignalLib.Basic
{
    public static class SignalOps
    {


        public static double[] xcorr(double[] signal, double[] pattern)
        {
            int L = signal.Length + pattern.Length - 1;
            double[] output = new double[L];
            alglib.corrr1d(signal, signal.Length, pattern, pattern.Length, out output);
            return output;
        }
    }
}
