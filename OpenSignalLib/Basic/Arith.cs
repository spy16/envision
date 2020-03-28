using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenSignalLib.Basic
{
    public static class Arith
    {

        public static float[] add(ICollection a, float b)
        {
            List<float> ans = new List<float>();
            foreach (double item in a)
            {
                ans.Add((float)(item + b));
            }
            return ans.ToArray();
        }

        public static float[] add(float b, ICollection a)
        {
            List<float> ans = new List<float>();
            foreach (double item in a)
            {
                ans.Add((float)(item + b));
            }
            return ans.ToArray();
        }

        //public static double[] ToDoubleArray(this ICollection arg)
        //{
        //    double[] array = new double[arg.Count];
        //    int i = 0;
        //    foreach (double item in arg)
        //    {
        //        double tmp = item/1.0;
        //        array[i++] = tmp;
        //    }
        //    return array;
        //}

        //public static float[] add(ICollection b, ICollection a)
        //{
        //    List<float> ans = new List<float>();
        //    if (b.Count == a.Count)
        //    {
        //        int[] _b = b.ToDoubleArray();
        //        int[] _a = a.ToDoubleArray();
        //        for (int i = 0 ; i < _a.Length ; i++)
        //        {
        //            double lp = (double)_b[i];
        //            double rp = (double)_a[i];

        //            ans.Add((float)((double)(_b[i]) + (double)(_a[i])));
        //        }
        //    }
        //    return ans.ToArray();
        //}


    }
}
