using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision
{
    public class Logger
    {

        public static void D(string msg)
        {
            System.Diagnostics.Debug.Print("D: " + msg);
        }
    }
}
