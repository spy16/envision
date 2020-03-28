using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.ComplexTypes;

namespace OpenSignalLib.Operations
{
    public static class Windows
    {


        #region [  Private Functions  ]
        private static double[] _gencoswin(int N, double a0, double a1, double a2, double a3 = 0, double a4 = 0)
        {
            double[] win = new double[N];
            win[0] = 0;
            for (int n = 1 ; n < N ; n++)
                win[n] = a0 - a1 * Math.Cos((2 * Math.PI * n) / (N - 1)) + a2 * Math.Cos((4 * Math.PI * n) / (N - 1))
                        - a3 * Math.Cos((6 * Math.PI * n) / (N - 1)) + a4 * Math.Cos((8 * Math.PI * n) / (N - 1));
            return win;
        }

        private static double[] _gencoswin2(int N, double alpha)
        {
            double[] retval = new double[N];
            for (int i = 0 ; i < N ; i++)
            {
                var cos = Math.Cos(((Math.PI * i) / (N - 1)) - (Math.PI / 2));
                retval[i] = Math.Pow(cos, alpha);
            }
            return retval;
        }

        /// <summary>
        /// Havercosine function
        /// </summary>
        /// <param name="angle">angle theta</param>
        /// <returns></returns>
        private static double hvc(double angle)
        {
            return (1 + Math.Cos(angle)) / 2;
        }

        private static double[] _gengaussian(int N, int p, double sigma)
        {
            if (p % 2 != 0 || p < 0)
            {
                throw new InvalidOperationException("p must be positive even integer");
            }
            double[] retval = new double[N];
            for (int i = 0 ; i < N ; i++)
            {
                double factor = ((i - (N - 1) / 2) / (sigma * (N - 1) / 2));
                retval[i] = Math.Exp((-0.5) * Math.Pow(factor, p));
            }
            return retval;
        }

        private static double besselizero(double x)
        {
            double temp;
            double sum = 1.0;
            double u = 1.0;
            double halfx = x / 2.0;
            int n = 1;

            do
            {
                temp = halfx / (double)n;
                u *= temp * temp;
                sum += u;
                n++;
            } while (u >= 1E-21 * sum);
            return (sum);
        }

        static void kaiser(int n, ref double[] w, double b)
        {
            double tmp;
            double k1 = 1.0 / besselizero(b);
            int k2 = 1 - (n & 1);
            int end = (n + 1) >> 1;
            int i;

            // Calculate window coefficients
            for (i = 0 ; i < end ; i++)
            {
                tmp = (double)(2 * i + k2) / ((double)n - 1.0);
                w[end - (1 & (k2 == 0 ? 1 : 0)) + i] = w[end - 1 - i] = k1 * besselizero(b * Math.Sqrt(1.0 - tmp * tmp));
            }
        }
        
        #endregion

        #region [ Generalized Hamming Windows ]
        /// <summary>
        /// Hanning window (Same as Hamming when alpha = 0.5)
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Hann(int N)
        {
            return _gencoswin(N, 0.5, 0.5, 0);
        }
        /// <summary>
        /// Hamming window 
        /// </summary>
        /// <param name="N">window length</param>
        /// <param name="alpha"></param>
        /// <returns>Hamming window co-efficients</returns>
        public static double[] Hamming(int N, double alpha = 0.53836)
        {
            double beta = 1 - alpha;
            return _gencoswin(N, alpha, beta, 0);
        }

        /// <summary>
        /// Blackman window
        /// </summary>
        /// <param name="N">window length</param>
        /// <param name="alpha"></param>
        /// <returns>window co-efficients</returns>
        public static double[] Blackman(int N, double alpha = 0.16)
        {
            double a0 = (1 - alpha) / 2;
            double a1 = 0.5;
            double a2 = alpha / 2;
            return _gencoswin(N, a0, a1, a2);
        }

        /// <summary>
        /// Nuttall window 
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Nuttall(int N)
        {
            return _gencoswin(N, 0.355768, 0.487396, 0.144232, 0.012604);
        }

        /// <summary>
        /// Blackman & Nuttall window
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Blackman_Nuttall(int N)
        {
            return _gencoswin(N, 0.3635819, 0.4891775, 0.1365995, 0.0106411);
        }

        /// <summary>
        /// Blackman & Harris window
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Blackman_Harris(int N)
        {
            return _gencoswin(N, 0.35875, 0.48829, 0.14128, 0.001168);
        }

        /// <summary>
        /// FlatTop window
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] FlatTop(int N)
        {
            return _gencoswin(N, 1, 1.93, 1.29, 0.388, 0.028);
        }

        #endregion

        #region [ Power of Cosine Windows  ]
        /// <summary>
        /// Cosine window
        /// </summary>
        /// <param name="N">window lenght</param>
        /// <returns>window co-efficients</returns>
        public static double[] Cosine(int N)
        {
            return _gencoswin2(N, 1);
        }

        /// <summary>
        /// Hanning window by usign Power-Of-Cosine function
        /// Ref: https://en.wikipedia.org/wiki/Window_function#Power-of-cosine_windows
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Hann2(int N)
        {
            return _gencoswin2(N, 2);
        }
        #endregion

        #region [   Gaussian ]
     
        /// <summary>
        /// Adjustable Gaussian window of order 2 (p=2)
        /// </summary>
        /// <param name="N">window length</param>
        /// <param name="sigma">standard deviation</param>
        /// <returns>window co-efficients</returns>
        public static double[] Gaussian(int N, double sigma)
        {
            return _gengaussian(N, 2, sigma);
        }

        /// <summary>
        /// Adjustable Gaussian window of order p
        /// </summary>
        /// <param name="N">window length</param>
        /// <param name="p">order of the gaussian function</param>
        /// <param name="sigma">standard deviation</param>
        /// <returns>window co-efficients</returns>
        public static double[] GaussianGeneric(int N, int p, double sigma)
        {
            return _gengaussian(N, p, sigma);
        }
        #endregion

        #region [  Other  ]
        /// <summary>
        /// Boxcar or Rectangular window
        /// </summary>
        /// <param name="N">window length</param>
        /// <returns>window co-efficients</returns>
        public static double[] Boxcar(int N)
        {
            double[] arr = new double[N];
            for (int i = 0 ; i < arr.Length ; i++)
            {
                arr[i] = 1;
            }
            return arr;
        }

        /// <summary>
        /// Kaiser window
        /// </summary>
        /// <param name="N">window lenght</param>
        /// <param name="alpha"></param>
        /// <returns>window co-efficients</returns>
        public static double[] Kaiser(int N, double alpha = 2)
        {
            double[] retval = new double[N];
            double beta = Math.PI * alpha;
            kaiser(N, ref retval, beta);
            return retval;
        }
        
        /// <summary>
        /// Tukey window
        /// </summary>
        /// <param name="N">window length</param>
        /// <param name="alpha"></param>
        /// <returns>window co-efficients</returns>
        public static double[] Tukey(int N, double alpha)
        {
            double[] retval = new double[N];
            double first_lim_right = alpha * (N - 1) / 2;
            double secon_lim_right = (N - 1) * (1 - alpha / 2);
            double factor1 = 0;
            for (int i = 0 ; i < N ; i++)
            {
                if (i >= 0 && i <= first_lim_right)
                {
                    factor1 = ((2 * i) / (alpha * (N - 1))) - 1;
                    retval[i] = hvc(Math.PI * (factor1));
                }
                else if (i > first_lim_right && i <= secon_lim_right)
                {
                    retval[i] = 1;
                }
                else
                {
                    factor1 = ((2 * i) / (alpha * (N - 1))) + 1 - (2 / alpha);
                    retval[i] = hvc(Math.PI * (factor1));
                }
            }
            return retval;
        }




        #endregion
    }
}
