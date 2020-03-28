using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
    public class WhiteGaussianNoise : Signal
    {
        public WhiteGaussianNoise(int length, double SNRdB, int SampleRate, double SymbolEnergy )
        {
            SignalGenerator sig = new SignalGenerator(SignalType.WhiteNoise, 10000, 0, SampleRate, 1, length);
            double snr_linear = Math.Pow(10.0, SNRdB / 10.0);
            double N0 = SymbolEnergy / snr_linear;
            double noiseSigma = Math.Sqrt(N0);
            for (int i = 0 ; i < sig.Samples.Length ; i++)
            {
                sig.Samples[i] *= noiseSigma;
            }
            this.Samples = sig.Samples;
            this.SamplingRate = sig.SamplingRate;
        }
    }
}
