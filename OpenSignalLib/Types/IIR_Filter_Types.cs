using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Filters
{
    // IIR filter design filter type
    public enum IIR_FilterType
    {
        BUTTER = 0,
        CHEBY1,
        CHEBY2,
        ELLIP,
        BESSEL
    } ;
    // IIR filter design band type
    public enum IIR_BandType
    {
        LOWPASS = 0,
        HIGHPASS,
        BANDPASS,
        BANDSTOP
    } ;

    // IIR filter design coefficients format
    public enum IIR_Format
    {
        SOS = 0,
        TF
    } ;


}
