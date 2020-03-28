using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OpenSignalLib.Filters
{
    public class FIRFilter
    {
        private float[] _coeffs;

        public float[] Coefficients
        {
            get { return _coeffs; }
        }
        
        public FIRFilter(float[] co_effs)
        {
            this._coeffs = co_effs;
        }
        
    }
}
