using OpenSignalLib.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Transforms
{
    public static class LiquidFFT
    {
        public static Complex[] fft(float[] input, int N)
        {
            Complex[] output = new Complex[N];
            unsafe
            {
                float* real = stackalloc float[N];
                float* imag = stackalloc float[N];
                libliquid.fft(input, input.Length, N, real, imag);
                for (int i = 0 ; i < N ; i++)
                {
                    output[i] = new Complex(real[i], imag[i]);
                }
            }
            return output;
        }

        public static Complex[] fft2(float[] input, int N = 1024)
        {
            Complex[] retval = new Complex[N];
            unsafe
            {
                libliquid.Complex* outPtr = stackalloc libliquid.Complex[N];
                libliquid.fft2(input, input.Length, N, outPtr);

                for (int i = 0 ; i < N ; i++)
                {
                    retval[i] = new Complex(outPtr[i].real, outPtr[i].imag);
                }
            }
            return retval;
        }

        public static Complex[] ifft(Complex[] input, int N)
        {
            Complex[] output = new Complex[N];
            float[] real = new float[N];
            float[] imag = new float[N];
            for (int i = 0 ; i < N ; i++)
            {
                real[i] = (float)input[i].Re;
                imag[i] = (float)input[i].Im;
            }

            unsafe
            {
                float* sig_real = stackalloc float[N];
                float* sig_imag = stackalloc float[N];
                float* rmse = stackalloc float[1];
                libliquid.ifft(N, real, imag,  sig_real, sig_imag);
                for (int i = 0 ; i < N ; i++)
                {
                    output[i] = new Complex(sig_real[i], sig_imag[i]);
                }
            }
            return output;
        }
    }
}
