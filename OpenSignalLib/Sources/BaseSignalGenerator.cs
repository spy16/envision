using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenSignalLib.Operations;

namespace OpenSignalLib.Sources
{

    [Serializable]
        public class BaseSignalGenerator
        {
            #region [ Properties ... ]

            private SignalType signalType = SignalType.Sine;
            /// <summary>
            /// Signal Type.
            /// </summary>
            public SignalType SignalType
            {
                get { return signalType; }
                set { signalType = value; }
            }

            private float frequency = 1f;
            /// <summary>
            /// Signal Frequency.
            /// </summary>
            public float Frequency
            {
                get { return frequency; }
                set { frequency = value; }
            }

            private float phase = 0f;
            /// <summary>
            /// Signal Phase.
            /// </summary>
            public float Phase
            {
                get { return phase; }
                set { phase = value; }
            }

            private float amplitude = 1f;
            /// <summary>
            /// Signal Amplitude.
            /// </summary>
            public float Amplitude
            {
                get { return amplitude; }
                set { amplitude = value; }

            }

            private float offset = 0f;
            /// <summary>
            /// Signal Offset.
            /// </summary>
            public float Offset
            {
                get { return offset; }
                set { offset = value; }
            }

            private float invert = 1; // Yes=-1, No=1
            /// <summary>
            /// Signal Inverted?
            /// </summary>
            public bool Invert
            {
                get { return invert == -1; }
                set { invert = value ? -1 : 1; }
            }

            private GetValueDelegate getValueCallback = null;
            /// <summary>
            /// GetValue Callback?
            /// </summary>
            public GetValueDelegate GetValueCallback
            {
                get { return getValueCallback; }
                set { getValueCallback = value; }
            }

            #endregion  [ Properties ]

            #region [ Private ... ]

            /// <summary>
            /// Random provider for noise generator
            /// </summary>
            private Random random = new Random();

            /// <summary>
            /// Time the signal generator was started
            /// </summary>
            protected long startTime = Stopwatch.GetTimestamp();

            /// <summary>
            /// Ticks per second on this CPU
            /// </summary>
            protected long ticksPerSecond = Stopwatch.Frequency;

            #endregion  [ Private ]

            #region [ Public ... ]

            public delegate float GetValueDelegate(float time);

            public BaseSignalGenerator(SignalType initialSignalType)
            {
                signalType = initialSignalType;
            }

            public BaseSignalGenerator() { }

#if DEBUG
            public float GetValue(float time)
#else
			public float GetValue(float time)
#endif
            {
                float value = 0f;
                float t = frequency * time + phase;
                if (signalType == Sources.SignalType.Sine || signalType == Sources.SignalType.Square)
                {
                    t = frequency * time;
                }
                switch (signalType)
                { // http://en.wikipedia.org/wiki/Waveform
                    case SignalType.Sine: // sin( 2 * pi * t )
                        value = (float)Math.Sin(2f * Math.PI * t + phase);
                        break;
                    case SignalType.Square: // sign( sin( 2 * pi * t ) )
                        value = Math.Sign(Math.Sin(2f * Math.PI * t + phase));
                        value = (value == -1) ? 0 : 1;
                        break;
                    case SignalType.Triangle: // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                        value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                        break;
                    case SignalType.Sawtooth: // 2 * ( t/a - floor( t/a + 1/2 ) )
                        value = 2f * (t - (float)Math.Floor(t + 0.5f));
                        break;


                    case SignalType.Pulse: // http://en.wikipedia.org/wiki/Pulse_wave
                        value = (Math.Abs(Math.Sin(2 * Math.PI * t)) < 1.0 - 10E-3) ? (0) : (1);
                        break;
                    case SignalType.WhiteNoise: // http://en.wikipedia.org/wiki/White_noise
                        value = 2f * (float)random.Next(int.MaxValue) / int.MaxValue - 1f;
                        break;
                    case SignalType.GaussNoise: // http://en.wikipedia.org/wiki/Gaussian_noise
                        value = (float)StatisticFunction.NORMINV((float)random.Next(int.MaxValue) / int.MaxValue, 0.0, 0.4);
                        break;
                    case SignalType.DigitalNoise: //Binary Bit Generators
                        value = random.Next(2);
                        break;

                    case SignalType.UserDefined:
                        value = (getValueCallback == null) ? (0f) : getValueCallback(t);
                        break;
                }

                return (invert * amplitude * value + offset);
            }

            public float GetValue()
            {
                float time = (float)(Stopwatch.GetTimestamp() - startTime) / ticksPerSecond;
                return GetValue(time);
            }

            public void Reset()
            {
                startTime = Stopwatch.GetTimestamp();
            }

            public void Synchronize(BaseSignalGenerator instance)
            {
                startTime = instance.startTime;
                ticksPerSecond = instance.ticksPerSecond;
            }

            #endregion [ Public ]
        }

        #region [ Enums ... ]

        public enum SignalType
        {
            Sine,
            Square,
            Triangle,
            Sawtooth,

            Pulse,
            WhiteNoise,    // random between -1 and 1
            GaussNoise,	   // random between -1 and 1 with normal distribution
            DigitalNoise,

            UserDefined    // user defined between -1 and 1	}
        }

        #endregion [ Enums ]



    
}
