using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalLib.ComplexTypes;
using System.Runtime.InteropServices;

namespace OpenSignalLib.Operations
{
   public  static class Misc
    {

        public static float[] ToArray(this double[] arr)
        {
            float[] retval = new float[arr.Length];
            Parallel.For(0, arr.Length, i => {
                retval[i] = (float)arr[i];
            });
            return retval;
        }

        public static double[] ToArray(this float[] arr)
        {
            double[] retval = new double[arr.Length];
            Parallel.For(0, arr.Length, i => {
                retval[i] = (double)arr[i];
            });
            return retval;
        }

       public static double[] Abs(double[] samples)
       {
           System.Threading.Tasks.Parallel.For(0, samples.Length, i => {
               samples[i] = Math.Abs(samples[i]);
           });
           return samples;
       }

       public static double[] Abs(Complex[] samples)
       {
           double[] retval = new double[samples.Length];
           System.Threading.Tasks.Parallel.For(0, samples.Length, i => {
               retval[i] = samples[i].GetModulus();
           });
           return retval;
       }

       public static ComplexF[] List_to_ComplexArray(IronPython.Runtime.List list_object)
       {
           ComplexF[] _fft = new ComplexF[list_object.Count];
           System.Threading.Tasks.Parallel.For(0,_fft.Length, i=>{
               try
               {
                   if (list_object[i].GetType() == typeof(System.Numerics.Complex))
                   {
                       System.Numerics.Complex c = (System.Numerics.Complex)list_object[i];
                       _fft[i] = new ComplexF((float)c.Real, (float)c.Imaginary);
                   }
                   else
                   {
                       _fft[i] = new ComplexF(float.Parse(list_object[i].ToString()), 0);
                   }
               } catch (Exception)
               {
                   throw;
               }
           });
           return _fft;
       }

       public static IronPython.Runtime.List ComplexArray_to_List(ComplexF[] complex_array)
       {
           IronPython.Runtime.List l = new IronPython.Runtime.List();
           System.Threading.Tasks.Parallel.ForEach(complex_array, item => {
               l.append(new System.Numerics.Complex(item.Re, item.Im));
           });
           return l;
       }


        public static int ToNext2Pow(int n)
        {
            double y = Math.Floor(Math.Log(n, 2));
            return (int)Math.Pow(2, y + 1);
        }

        public static int Pow2(int exponent)
        {
            if (exponent >= 0 && exponent < 31)
            {
                return 1 << exponent;
            }
            return 0;
        }

        static public bool IsPowerOf2(int x)
        {
            return (x & (x - 1)) == 0;
            //return	( x == Pow2( Log2( x ) ) );
        }

        public static float[] linspace(float start, float stop, int slices = -1)
        {
            if (start > stop) throw new InvalidOperationException("condition not met: start < stop");
            if (slices == -1) slices = (int)(stop - start + 1);
            float[] retval = new float[slices];
            float c = (stop - start) / (slices - 2);
            System.Threading.Tasks.Parallel.For(0, slices, i => {
                retval[i] = start + i * (stop - start) / (slices - 1);
            });
            return retval;
        }

        public static double[] linspace(double start, double stop, int slices = -1)
        {
            if (start > stop) throw new InvalidOperationException("condition not met: start < stop");
            if (slices == -1) slices = (int)(stop - start + 1);
            double[] retval = new double[slices];
            double c = (stop - start) / (slices - 2);
            System.Threading.Tasks.Parallel.For(0, slices, i => {
                retval[i] = start + i * (stop - start) / (slices - 1);
            });
            return retval;
        }

    }
}
