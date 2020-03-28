using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenSignalLib.Filters
{

    public class IIRFilter
    {

        private int _order;

        public int Order
        {
            get { return _order; }
        }
                

        private float[] _num;

        public float[] Numerator
        {
            get { return _num; }
        }

        private float[] _den;

        public float[] Denominator
        {
            get { return _den; }
        }
        
        public IIRFilter(float[] num, float[] den)
        {
            this._num = num;
            this._den = den;
            this._order = num.Length;
        }

    }
}
