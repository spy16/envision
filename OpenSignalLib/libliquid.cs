using OpenSignalLib.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib
{
    unsafe internal static class libliquid
    {

        #region [   Types   ]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Complex
        {
            public Single real;
            public Single imag;
        };
        #endregion

        public static OpenSignalLib.ComplexTypes.Complex ToOSLComplex(Complex arg)
        {
            return new ComplexTypes.Complex(arg.real, arg.imag);
        }

        public unsafe static T[] Create<T>(void* source, int length)
        {
            var type = typeof(T);
            var sizeInBytes = Marshal.SizeOf(typeof(T));

            T[] output = new T[length];

            if (type.IsPrimitive)
            {
                // Make sure the array won't be moved around by the GC 
                var handle = GCHandle.Alloc(output, GCHandleType.Pinned);

                var destination = (byte*)handle.AddrOfPinnedObject().ToPointer();
                var byteLength = length * sizeInBytes;

                // There are faster ways to do this, particularly by using wider types or by 
                // handling special lengths.
                for (int i = 0 ; i < byteLength ; i++)
                    destination[i] = ((byte*)source)[i];

                handle.Free();
            }
            else if (type.IsValueType)
            {
                if (!type.IsLayoutSequential && !type.IsExplicitLayout)
                {
                    throw new InvalidOperationException(string.Format("{0} does not define a StructLayout attribute", type));
                }

                IntPtr sourcePtr = new IntPtr(source);

                for (int i = 0 ; i < length ; i++)
                {
                    IntPtr p = new IntPtr((byte*)source + i * sizeInBytes);

                    output[i] = (T)System.Runtime.InteropServices.Marshal.PtrToStructure(p, typeof(T));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} is not supported", type));
            }

            return output;
        }

        #region [   Methods: Correlators ]
        [DllImport("libliquid.dll")]
        internal extern static int xcorrelate(Complex[] sync, int sync_length, Complex[] sequence, int seq_length,
     float threshold, float dphi_max, float* _tau_hat, float* _dphi_hat, float* _gamma_hat);
        #endregion


        #region [   Methods: FIR - Filters  ]


        [DllImport("libliquid.dll")]
        internal static extern void firdespm_custom(int _h_len,
                  int _num_bands,
                  float[] _bands,
                  float[] _des,
                  float[] _weights,
                  Filters.FIR_PM_WeightingType[] _wtype,
                  Filters.FIR_PM_BandType _btype,
                  float[] _h);

        [DllImport("libliquid.dll")]
        internal static extern void fir_response(float[] h, int h_len, float[] frequencies, Complex* response, int response_length);


        [DllImport("libliquid.dll")]
        internal static extern void liquid_firdes_kaiser(int _n, float _fc, float _As, float _mu, float* _h);


        [DllImport("libliquid.dll")]
        internal static extern void firfilt_apply(float[] h, int h_len, float[] x, int x_len, float* output);


        [DllImport("libliquid.dll")]
        internal static extern void liquid_firdes_prototype(Filters.FIR_Filter_Type _type, int _k,
                              int _m, float _beta, float _dt, float* _h);


        // estimate required filter length given
        //  _df     :   transition bandwidth (0 < _b < 0.5)
        //  _As     :   stop-band attenuation [dB], _As > 0
        [DllImport("libliquid.dll")]
        internal static extern int estimate_req_filter_len(float _df,
                                             float _As);

        // estimate filter stop-band attenuation given
        //  _df     :   transition bandwidth (0 < _b < 0.5)
        //  _N      :   filter length
        [DllImport("libliquid.dll")]
        internal static extern float estimate_req_filter_As(float _df,
                                     int _N);

        // estimate filter transition bandwidth given
        //  _As     :   stop-band attenuation [dB], _As > 0
        //  _N      :   filter length
        [DllImport("libliquid.dll")]
        internal static extern float estimate_req_filter_df(float _As,
                                     int _N);


        // returns the Kaiser window beta factor give the filter's target
        // stop-band attenuation (As) [Vaidyanathan:1993]
        //  _As     :   target filter's stop-band attenuation [dB], _As > 0
        [DllImport("libliquid.dll")]
        internal static extern float kaiser_beta_As(float _As);
        #endregion


        #region [   Methods: IIR - Filters  ]
        [DllImport("libliquid.dll")]
        internal static extern void liquid_iirdes(Filters.IIR_FilterType _ftype, Filters.IIR_BandType _btype,
            Filters.IIR_Format _format, int _n, float _fc, float _f0, float _Ap, float _As, float* _B, float* _A);


        [DllImport("libliquid.dll")]
        internal static extern void iirfilt_apply(float[] b, float[] a, int b_len, int a_len, float[] x,
                                                int x_len, float[] output);


        [DllImport("libliquid.dll")]
        internal static extern void design_iirfilt(int order, float fc, float f0, float As, float Ap, OpenSignalLib.Filters.IIR_FilterType ftype,
            OpenSignalLib.Filters.IIR_BandType btype, float* b, float* a);
        #endregion


        #region [  Methods: FFT/iFFT   ]

        [DllImport("libliquid.dll")]
        internal static extern void fft(float[] samples, int x_len, int nfft, float* real, float* imag);

        [DllImport("libliquid.dll")]
        internal static extern void ifft(int nfft, float[] real, float[] imag, float* signal_real, float* signal_imag);

        [DllImport("libliquid.dll")]
        internal static extern void fft2(float[] samples, int x_len, int nfft, Complex* outputPtr);

        #endregion


        #region [   Methods: Modulation/Demodulation  ]
        [DllImport("libliquid.dll")]
        internal static extern int digital_get_max_symbol(Types.ModulationScheme ms);

        [DllImport("libliquid.dll")]
        internal static extern void digital_modulate(Types.ModulationScheme ms, int[] symbols, int length, Complex* output);

        [DllImport("libliquid.dll")]
        internal static extern void digital_demodulate(Types.ModulationScheme ms, Complex[] modulated, int length, int* output);

        #endregion

    }


}
