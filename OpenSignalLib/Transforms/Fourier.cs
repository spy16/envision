using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.ComplexTypes;
using OpenSignalLib.Sources;
namespace OpenSignalLib.Transforms
{
    public static class Fourier
    {

        /// <summary>
        /// Arranges the FFT values to have 0 as the center frequency
        /// </summary>
        /// <param name="fftvalues">Array of type ComplexF which contain the FFT values of a Signal</param>
        /// <returns>FFT values with the halves of the argument fftvalues swapped</returns>
        public static Complex[] FFTShift(Complex[] fftvalues)
        {
            int half = fftvalues.Length / 2;
            Complex[] f_half = new ArraySegment<Complex>(fftvalues, 0, half).ToArray();
            Complex[] s_half = new ArraySegment<Complex>(fftvalues, half, fftvalues.Length / 2).ToArray();
            Complex[] retval = s_half.Concat(f_half).ToArray();
            return retval;
        }

        /// <summary>
        /// generates a list of KeyValuePair where Value represents the power at a 
        /// particular frequency which is represented by Key.
        /// </summary>
        /// <param name="sig">Signal object</param>
        /// <param name="N">value of N for N-Point FFT</param>
        /// <returns></returns>
        public static Dictionary<double, double> fspectrum(this Signal sig, int N = 1024)
        {
            Complex[] fft = FFTShift(sig.FFT(N));
            Dictionary<double, double> retval = new Dictionary<double, double>();
            float[] freqs = Operations.Misc.linspace(-N/2, (N/2)-1);
            for (int i = 0 ; i < N ; i++)
            {
                freqs[i] = sig.SamplingRate * freqs[i] / N;
                retval[freqs[i]] = (fft[i] * fft[i].GetConjugate()).GetModulus() / (sig.Samples.Length * sig.Samples.Length);
            }
            return retval;
        }


        /// <summary>
        /// Calculates N-point frequency domain transform (Fast-Fourier Transform) of the given signal
        /// </summary>
        /// <param name="s">signal object</param>
        /// <param name="N">value of N for N-point FFT</param>
        /// <returns>Array of Complex[Re,Im] values</returns>
        public static Complex[] FFT(this OpenSignalLib.Sources.Signal s, int N = 1024)
        {
            float[] sig = new float[s.Samples.Length];
            for (int i = 0 ; i < s.Length ; i++)
            {
                sig[i] = (float)s.Samples[i];
            }
            Complex[] fft = LiquidFFT.fft(sig, N);
            return fft;
        }

        /// <summary>
        /// Calculates N-point frequency domain transform (Fast-Fourier Transform) of the given signal
        /// </summary>
        /// <param name="sig">Samples of the signal</param>
        /// <param name="N">value of N for N-point FFT</param>
        /// <returns>Array of Complex[Re,Im] values</returns>
        public static Complex[] FFT(double[] sig, int N = 1024)
        {
            Complex[] retval = new Complex[sig.Length];
            float[] signal_samples = new float[sig.Length];
            for (int i = 0 ; i < sig.Length ; i++)
            {
                signal_samples[i] = (float)sig[i];
            }
            retval = LiquidFFT.fft(signal_samples, N);
            return retval;
        }

        

        /// <summary>
        /// Calculates N-point inverse frequency domain transform (Fast-Fourier Transform) of the given signal
        /// </summary>
        /// <param name="fft_samples">fft values</param>
        /// <param name="N">value of N used while taking FFT</param>
        /// <returns></returns>
        public static Complex[] iFFT(double[] fft_samples, int N = 1024)
        {
            Complex[] fft_ = new Complex[fft_samples.Length];
            for (int i = 0 ; i < fft_.Length ; i++)
            {
                fft_[i] = new Complex(fft_samples[i], 0);
            }
            return iFFT(fft_, N);
        }


        /// <summary>
        /// Calculates N-point inverse frequency domain transform (Fast-Fourier Transform) of the given signal
        /// </summary>
        /// <param name="fft_samples">fft values</param>
        /// <param name="N">value of N used while taking FFT</param>
        /// <returns></returns>
        public static Complex[] iFFT(Complex[] fft_samples, int N = 1024)
        {
            Complex[] retval = new Complex[N];
            retval = LiquidFFT.ifft(fft_samples, N);
            return retval;
        }
    }
}
