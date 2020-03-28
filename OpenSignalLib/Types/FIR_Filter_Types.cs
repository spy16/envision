using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Filters
{
    // band type specifier
    public enum FIR_PM_BandType
    {
        BANDPASS = 0,     // regular band-pass filter
        DIFFERENTIATOR, // differentiating filter
        HILBERT         // Hilbert transform
    } ;

    // weighting type specifier
    public enum FIR_PM_WeightingType
    {
        FLATWEIGHT = 0,   // flat weighting
        EXPWEIGHT,      // exponential weighting
        LINWEIGHT,      // linear weighting
    } ;

    // prototypes
    public enum FIR_Filter_Type
    {
        UNKNOWN = 0,   // unknown filter type

        // Nyquist filter prototypes
        KAISER,      // Nyquist Kaiser filter
        PM,          // Parks-McClellan filter
        RCOS,        // raised-cosine filter
        FEXP,        // flipped exponential
        FSECH,       // flipped hyperbolic secant
        FARCSECH,    // flipped arc-hyperbolic secant

        // root-Nyquist filter prototypes
        ARKAISER,    // root-Nyquist Kaiser (approximate optimum)
        RKAISER,     // root-Nyquist Kaiser (true optimum)
        RRC,         // root raised-cosine
        hM3,         // harris-Moerder-3 filter
        GMSKTX,      // GMSK transmit filter
        GMSKRX,      // GMSK receive filter
        RFEXP,       // flipped exponential
        RFSECH,      // flipped hyperbolic secant
        RFARCSECH,   // flipped arc-hyperbolic secant
    } ;
}
