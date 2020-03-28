using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenSignalLib.Filters;
using OpenSignalLib.Operations;

namespace OpenSignalLib.Filters
{
    public static class FilterDesign
    {

        #region [   FIR Filters   ]

        /// <summary>
        /// Designs a Kaiser FIR filter 
        /// </summary>
        /// <param name="n">filter length</param>
        /// <param name="fc">cut-off frequency</param>
        /// <param name="As">Stopband attenuation</param>
        /// <param name="mu">Fractional time delay</param>
        /// <returns>FIRFilter object containing prototype co-efficients</returns>
        public static Filters.FIRFilter DesignFIRKaiser(int n, float fc, float As, float mu, float _scale = 2)
        {
            float[] h = new float[n];
            unsafe
            {
                float* _h = stackalloc float[n];
                libliquid.liquid_firdes_kaiser(n, fc / _scale, As, mu, _h);
                for (int i = 0 ; i < n ; i++)
                {
                    h[i] = _h[i];
                }
            }
            Filters.FIRFilter f = new Filters.FIRFilter(h);
            return f;
        }

        /// <summary>
        /// Design a Nyquist FIR filter
        /// </summary>
        /// <param name="int">filter type</param>
        /// <param name="_k">samples/symbol</param>
        /// <param name="_m">symbol delay</param>
        /// <param name="_beta">rolloff factor (0 &lt; beta &lt; 1)</param>
        /// <param name="_dt">fractional sample delay</param>
        /// <returns>returns FIR filter having co-efficient vector of length (2*_k*_m+1)</returns>
        public static Filters.FIRFilter DesignFIR(int type, int _k, int _m, float _beta, float _dt)
        {
            FIR_Filter_Type f = (FIR_Filter_Type)type;
            return DesignFIR(f, _k, _m, _beta, _dt);
        }

        /// <summary>
        /// Design a Nyquist FIR filter
        /// </summary>
        /// <param name="type">filter type (e.g. kaiser, rrc etc.)</param>
        /// <param name="_k">samples/symbol</param>
        /// <param name="_m">symbol delay</param>
        /// <param name="_beta">rolloff factor (0 &lt; beta &lt; 1)</param>
        /// <param name="_dt">fractional sample delay</param>
        /// <returns>returns FIR filter having co-efficient vector of length (2*_k*_m+1)</returns>
        public static Filters.FIRFilter DesignFIR(FIR_Filter_Type type, int _k, int _m, float _beta, float _dt)
        {
            float[] h = new float[(2 * _k * _m + 1)];
            unsafe
            {
                float* _h = stackalloc float[h.Length];
                libliquid.liquid_firdes_prototype(type, _k, _m, _beta, _dt, _h);
                for (int i = 0 ; i < h.Length ; i++)
                {
                    h[i] = _h[i];
                }
            }
            Filters.FIRFilter f = new Filters.FIRFilter(h);
            return f;
        }



        public static Filters.FIRFilter DesignFIR1(int h_len, float[] bands, float[] responses, float[] weights
            , FIR_PM_WeightingType[] wTypes, FIR_PM_BandType bType)
        {
            //firdespm_run(_h_len, _num_bands, * _bands, * _des, * _weights,  
            //Filters.FIR_PM_WeightingType* _wtype, Filters.FIR_PM_BandType _btype, float* _h);
            float[] h = new float[h_len];
            if (bands.Length % 2 != 0)
            {
                throw new Exception("argument `bands` must be of even length");
            }
            libliquid.firdespm_custom(h_len, bands.Length / 2, bands, responses, weights, wTypes, bType, h);
            FIRFilter f = new FIRFilter(h);
            return f;
        }

        /// <summary>
        /// Apply FIR filter on signal
        /// </summary>
        /// <param name="filter">FIR filter object</param>
        /// <param name="input">input signal</param>
        /// <returns>array of floating-point samples of filtered signal</returns>
        public static float[] ApplyFilter(this Filters.FIRFilter filter, float[] input)
        {
            float[] output = new float[input.Length];
            unsafe
            {
                float* y = stackalloc float[input.Length];
                libliquid.firfilt_apply(filter.Coefficients, filter.Coefficients.Length, input, input.Length, y);
                for (int i = 0 ; i < output.Length ; i++)
                {
                    output[i] = y[i];
                }
            }
            return output;
        }

        #endregion


        #region [   Window-Based FIR Filters   ]
        private static float sinc(float x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return (float)(Math.Sin(Math.PI * x) / (Math.PI *x));
            }
        }

        public static Filters.FIRFilter FIRSinc(int length, float fc) {
            float[] sincf = Operations.Misc.linspace(-length / 2, length / 2, length);
            float[] filter = new float[length];
            for (int i = 0 ; i < length ; i++)
            {
                float b = fc * sinc(fc * sincf[i]);
                filter[i] = (float)(b);
            }
            return new FIRFilter(filter);
        }

        public static Filters.FIRFilter FIRCosine(int length, float fc)
        {
            float[] window = Misc.ToArray(Operations.Windows.Cosine(length));
            float[] sincf = Operations.Misc.linspace(-length / 2, length / 2, length);
            float[] filter = new float[length];
            for (int i = 0 ; i < window.Length ; i++)
            {
                float b = fc * sinc(fc * sincf[i]);
                filter[i] = (float)(window[i] * b );
            }
            return new FIRFilter(filter);
        }

        public static Filters.FIRFilter FIRHamming(int length, float fc)
        {
            float[] window = Misc.ToArray(Operations.Windows.Hamming(length));
            float[] sincf = Operations.Misc.linspace(-length / 2, length / 2, length);
            float[] filter = new float[length];
            for (int i = 0 ; i < window.Length ; i++)
            {
                float b = fc * sinc(fc * sincf[i]);
                filter[i] = (float)(window[i] * b);
            }
            return new FIRFilter(filter);
        }
        #endregion

        #region [   IIR Filters   ]

        /// <summary>
        /// Designs an IIR filter with given specifications
        /// </summary>
        /// <param name="filter">filter type</param>
        /// <param name="band">filter passband type(Lowpass, Highpass etc.)</param>
        /// <param name="format">output format (SOS/TF)</param>
        /// <param name="order">order of the filter</param>
        /// <param name="fc">cut-off frequency</param>
        /// <param name="f0">center frequency</param>
        /// <param name="Ap">passband ripple(dB)</param>
        /// <param name="As">stopband ripple(dB)</param>
        /// <returns>IIRFilter object containing filter co-efficients</returns>
        public static Filters.IIRFilter DesignIIR(int filter, int band, int order,
            float fc, float f0, float Ap, float As)
        {
            IIR_FilterType fType = (IIR_FilterType)filter;
            IIR_BandType bType = (IIR_BandType)band;
            return DesignIIR(fType, bType, order, fc, f0, Ap, As);
        }

        /// <summary>
        /// Designs an IIR filter with given specifications
        /// </summary>
        /// <param name="filter">filter type (butter, cheby etc.)</param>
        /// <param name="band">filter passband type(Lowpass, Highpass etc.)</param>
        /// <param name="format">output format (SOS/TF)</param>
        /// <param name="order">order of the filter</param>
        /// <param name="fc">cut-off frequency</param>
        /// <param name="f0">center frequency</param>
        /// <param name="Ap">passband ripple(dB)</param>
        /// <param name="As">stopband ripple(dB)</param>
        /// <returns>IIRFilter object containing filter co-efficients</returns>
        public static Filters.IIRFilter DesignIIR(Filters.IIR_FilterType filter, Filters.IIR_BandType band, int order,
            float fc, float f0, float Ap, float As)
        {
            // derived values : compute filter length
            int N = order; // effective order
            // filter order effectively doubles for band-pass, band-stop
            // filters due to doubling the number of poles and zeros as
            // a result of filter transformation
            if (band == Filters.IIR_BandType.BANDPASS ||
                band == Filters.IIR_BandType.BANDSTOP)
            {
                N *= 2;
            }
            int r = N % 2;     // odd/even order
            int L = (N - r) / 2;   // filter semi-length
            float[] num = { };
            float[] den = { };
            // validate input
            if (fc <= 0 || fc >= 0.5)
            {
                throw new Exception("cutoff frequency out of range");
            }
            else if (f0 < 0 || f0 > 0.5)
            {
                throw new Exception("center frequency out of range");
            }
            else if (Ap <= 0)
            {
                throw new Exception("pass-band ripple out of range");
            }
            else if (As <= 0)
            {
                throw new Exception("stop-band ripple out of range");
            }

            unsafe
            {
                int h_len = N + 1;
                float* _B = stackalloc float[h_len];
                float* _A = stackalloc float[h_len];
                libliquid.liquid_iirdes(filter, band, IIR_Format.TF, order, fc, f0, Ap, As
                    , _B, _A);
                num = new float[h_len];
                den = new float[h_len];
                num = libliquid.Create<float>(_B, h_len);
                den = libliquid.Create<float>(_A, h_len);
            }
            IIRFilter f = new IIRFilter(num, den);
            return f;
        }

        /// <summary>
        /// Apply IIR filter on a signal
        /// </summary>
        /// <param name="filter">IIR filter object</param>
        /// <param name="input">input signal</param>
        /// <returns>array of floating-point samples of filtered signal</returns>
        public static float[] ApplyFilter(this Filters.IIRFilter filter, float[] input)
        {
            float[] output = new float[input.Length];
            int n = filter.Numerator.Length;
            int m = filter.Denominator.Length;
            unsafe
            {
                float[] y = new float[output.Length];
                libliquid.iirfilt_apply(filter.Numerator, filter.Denominator, n, m, input, input.Length, y);
                for (int i = 0 ; i < output.Length ; i++)
                {
                    output[i] = y[i];
                }
            }
            return output;
        }

       

        public static Sources.Signal ApplyFilter(this Filters.IIRFilter filter, Sources.Signal input)
        {
            double[] output = new double[input.Samples.Length];
            int n = filter.Numerator.Length;
            int m = filter.Denominator.Length;
            unsafe
            {
                float[] y = new float[output.Length];
                libliquid.iirfilt_apply(filter.Numerator, filter.Denominator, n, m,
                    Misc.ToArray(input.Samples), input.Samples.Length, y);
                for (int i = 0 ; i < output.Length ; i++)
                {
                    if (float.IsInfinity(y[i]) || float.IsNaN(y[i])) break;
                    output[i] = y[i];
                }
            }
            return new Sources.Signal(input.SamplingRate, output);
        }


        public static Dictionary<float, ComplexTypes.Complex> FrequencyResponse(FIRFilter filt, int Length = 512)
        {
            Dictionary<float, ComplexTypes.Complex> retval = new Dictionary<float, ComplexTypes.Complex>();
            float[] f = new float[Length];
            unsafe
            {
                libliquid.Complex* res = stackalloc libliquid.Complex[Length];
                libliquid.fir_response(filt.Coefficients, filt.Coefficients.Length, f, res, Length);
                for (int i = 0 ; i < Length ; i++)
                {
                    retval.Add(f[i], libliquid.ToOSLComplex(res[i]));
                }
            }
            return retval;
        }

        #endregion


    }
}
