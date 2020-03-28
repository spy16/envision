using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
    [Serializable]
    public class Sinusoidal : Signal
    {
        private BaseSignalGenerator p = new BaseSignalGenerator(SignalType.Sine);
        private float _increment = 0.001f;

        public double Frequency { get; private set; }
        public double Phase { get; private set; }
        public double Amplitude { get; private set; }
        public Sinusoidal(float frequency, float phase = 0, float SampleRate = - 1, float amplitude = 1, int length = -1,
            bool useParallelization = true)
        {
            if (SampleRate == -1) SampleRate = 30 * frequency;
            if (length == -1) length = (int)SampleRate;
            this.SamplingRate = SampleRate;
            p.Amplitude = amplitude;
            p.Frequency = frequency;
            this.Frequency = frequency;
            this.Amplitude = amplitude;
            this.Phase = phase;
            p.Phase = phase;
            this._increment = 1.0f / SampleRate;
            this.Samples = new double[length];
            if (length > 5000 && useParallelization)
            {
                System.Threading.Tasks.Parallel.For(0, length, _calc);
            }
            else
            {
                float t = 0;
                for (int i = 0 ; i < length ; i++)
                {
                    this.Samples[i] = p.GetValue(t);
                    t = i * _increment;
                }
            }
        }
        private void _calc(int i)
        {
            this.Samples[i] = p.GetValue(i * _increment);
        }


    }

 



}
