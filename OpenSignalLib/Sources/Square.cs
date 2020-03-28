using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
        [Serializable]
    public class Square :Signal
    {
        public Square(float frequency, float phase = 0, float SampleRate = - 1, float amplitude = 1, int length = -1)
        {
            if (SampleRate == -1) SampleRate = 20 * frequency;
            if (length == -1) length = (int)SampleRate;
            this.SamplingRate = SampleRate;
            BaseSignalGenerator s = new BaseSignalGenerator(SignalType.Square);
            Samples = new double[length];
            s.Frequency = frequency;
            s.Amplitude = amplitude;
            s.Phase = phase;
            float t = 0;
            float tmp = 1 / SampleRate;
            for (int i = 0 ; i < length ; i++)
            {
                Samples[i] = s.GetValue(t);
                t += tmp;
            }

        }

       
    }
}
