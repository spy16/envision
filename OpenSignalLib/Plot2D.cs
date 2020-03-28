using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPlot.PlotCommon;

namespace OpenSignalLib
{
    public class Plotting
    {
        public static void Plot2d(params object[] args)
        {
            IronPlot.PlotContext.OpenNextWindow();
            var p = IronPlot.Plotting.Plot2D(args);
            IronPlot.PlotContext.AddPlot(p[0].Plot);
        }

        public static void xlabel(string label)
        {
           
        }
    }
}
