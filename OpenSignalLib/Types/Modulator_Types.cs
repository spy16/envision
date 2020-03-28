using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalLib.Types
{
    

    public enum ModulationScheme 
    {
        UNKNOWN = 0, // Unknown modulation scheme

        // Phase-shift keying (PSK)
        PSK2, PSK4,
        PSK8, PSK16,
        PSK32, PSK64,
        PSK128, PSK256,

        // Differential phase-shift keying (DPSK)
        DPSK2, DPSK4,
        DPSK8, DPSK16,
        DPSK32, DPSK64,
        DPSK128, DPSK256,

        // amplitude-shift keying
        ASK2, ASK4,
        ASK8, ASK16,
        ASK32, ASK64,
        ASK128, ASK256,

        // rectangular quadrature amplitude-shift keying (QAM)
        QAM4,
        QAM8, QAM16,
        QAM32, QAM64,
        QAM128, QAM256,

        // amplitude phase-shift keying (APSK)
        APSK4,
        APSK8, APSK16,
        APSK32, APSK64,
        APSK128, APSK256,

        // specific modem types
        BPSK,      // Specific: binary PSK
        QPSK,      // specific: quaternary PSK
        OOK,       // Specific: on/off keying
        SQAM32,    // 'square' 32-QAM
        SQAM128,   // 'square' 128-QAM
        V29,       // V.29 star constellation
        ARB16OPT,  // optimal 16-QAM
        ARB32OPT,  // optimal 32-QAM
        ARB64OPT,  // optimal 64-QAM
        ARB128OPT, // optimal 128-QAM
        ARB256OPT, // optimal 256-QAM
        ARB64VT,   // Virginia Tech logo

        // arbitrary modem type
        ARB        // arbitrary QAM
    } ;
}
