using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Sources
{
    [Serializable]
    public abstract class AbstractSignal
    {
        public abstract float SamplingRate { get; set; }
        public abstract double[] Samples { get; set; }
        public abstract double StartTime { get; set; }
        public abstract double StopTime { get; }
        public abstract int Length { get; }
        public abstract double Period { get; }
    }

    
}
