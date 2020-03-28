using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OpenSignalLib.Sources
{
    public class BinaryData : Signal
    {
        public short [] Data { get; set; }
        public BinaryData(bool[] Data,  int length)
        {
            this.Data = new short[Data.Length];
            int increment = length / Data.Length;
            int tmp = increment;
            this.Samples = new double[ length ];
            this.SamplingRate = increment;
            for (int i = 0 ; i < Data.Length ; i++)
            {
                for (int j = tmp - increment ; j < tmp ; j++)
                {
                    Samples[j] = Data[i] ? 1 : 0; 
                }
                this.Data[i] = (short)(Data[i] ? 1 : 0);
                tmp += increment;
            }
        }

        public BinaryData(bool[] Data, float SampleRate)
        {
            this.SamplingRate = SampleRate;
            this.Data = new short[Data.Length];
            int length = (int) (Data.Length * SampleRate);
            this.Samples = new double[length];
            int j = -1,i = 0;
            do
            {
                if (i % SampleRate == 0) j++;
                this.Samples[i] = (Data[j] ? 1 : 0);
                i++;
            } while (i < this.Samples.Length);

        }

        public new void Display()
        {
            foreach (var item in this.Data)
            {
                Debug.Print(item.ToString());
            }
        }

        //public static BinaryData FromSampleArray(float[] samples,float samplingrate)
        //{
        //    BinaryData b;

        //}
    }
}
