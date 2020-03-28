using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Blocks
{
    [Serializable]
    public class EventDescription
    {

        public EventDescription()
        {
            this.SimulationStart = 0.0;
            this.SimulationStop = -1;
            this.SimulationStep = 1;
        }
        public double Time { get; set; }

        public double SimulationStart { get; set; }
        public double SimulationStop { get; set; }
        public double SimulationStep { get; set; }
    }
}
