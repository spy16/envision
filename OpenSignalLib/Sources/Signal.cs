using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;

namespace OpenSignalLib.Sources
{
        [Serializable]
    public class Signal : AbstractSignal, IEnumerable<double>
    {
        private float _SampleRate;
        private double[] _Samples;


        public double this[int index]
        {
            get { return this.Samples[index]; }
            set { this._Samples[index] = value; }
        }
        public override float SamplingRate
        {
            get { return _SampleRate; }
            set { _SampleRate = value; setTimeScale(); }
        }

        public override double[] Samples
        {
            get { return _Samples; }
            set { 
                setAttribs(this.SamplingRate, value);
                setTimeScale();
            }
        }

        private double _startTime = 0;
        public override double StartTime
        {
            get
            {
                return _startTime;
            }
            set {
                this._startTime = value;
                this._stopTime = this._startTime + this.Samples.Length / SamplingRate; 
            }
        }

        private double _stopTime = 0;
        public override double StopTime
        {
            get { return this._stopTime; }
        }

        public override int Length
        {
            get
            {
                return this.Samples.Length;
            }
        }

        public override double Period
        {
            get { return this.Length / this.SamplingRate; }
        }

        private void setAttribs(float SampleRate, double[] samples)
        {
            this._SampleRate = SampleRate;
            this._Samples = new double[samples.Length];
            Array.Copy(samples, this._Samples, Samples.Length);
        }

        private void setTimeScale()
        {
            this._startTime = 0;
            if(this.Samples != null) this._stopTime = this.StartTime + this.Samples.Length / SamplingRate;
        }

        //public Signal(float SampleRate, List Samples)
        //{
        //    double[] tmp = new double[Samples.Count];
        //    for (int i = 0 ; i < tmp.Length ; i++)
        //    {
        //        tmp[i] = double.Parse(Samples[i].ToString());
        //    }

        //    setAttribs(SampleRate, tmp);
        //    setTimeScale();
        //}
        public Signal(float SampleRate, double[] Samples)
        {
            setAttribs(SampleRate, Samples);
            setTimeScale();
        }

        public Signal()
        {

        }


        public Signal Extend(Signal sig)
        {
            Signal x = new Signal();
            if (this.Samples == null) this.Samples = new double[0];
            x.Samples = this.Samples.Concat(sig.Samples).ToArray();
            if (this.SamplingRate == 0) this.SamplingRate = sig.SamplingRate;
            x.SamplingRate = this.SamplingRate;
            return x;
        }

        public Signal Invert()
        {
            return this * -1;
        }

        public void Display()
        {
            for (int i = 0 ; i < this.Samples.Length ; i++)
            {
                System.Diagnostics.Debug.Print(this.Samples[i].ToString());
            }
        }

        public void CopyToCB()
        {
            var s = string.Join(" ", this.Samples);
            System.Windows.Forms.Clipboard.SetText(s);
        }

        //
        // Overriden
        //
        public override string ToString()
        {
            return String.Format("Signal => SampleRate={0}, Length={1}", this.SamplingRate, this.Samples.Length);
        }
        
        //
        //Operators
        //
        public static Signal operator *(Signal A, float x)
        {
            Signal tmp = new Signal(A.SamplingRate, A.Samples);
            for (int i = 0 ; i < tmp.Samples.Length ; i++)
            {
                tmp.Samples[i] = tmp.Samples[i] * x;
            }
            return tmp;
        }

        public static Signal operator *(Signal A, Array x)
        {
            Signal tmp = new Signal(A.SamplingRate, A.Samples);
            if (A.Samples.Length != x.Length)
            {
                throw new InvalidOperationException("Signal and Array must be of same length");
            }
            for (int i = 0 ; i < tmp.Samples.Length ; i++)
            {
                tmp.Samples[i] = tmp.Samples[i] * (double)x.GetValue(i);
            }
            return tmp;
        }


        public static Signal operator *(Signal A, Signal B)
        {
            if (A.Samples.Length != B.Samples.Length)
            {
                throw new InvalidOperationException("Both the operand signals must have equal number of samples");
            }
            if (A.SamplingRate != B.SamplingRate)
            {
                System.Diagnostics.Debug.Print("WARNING: Two signals with different sampling rates are being multiplied");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] * B.Samples[i];
            }
            temp.SamplingRate = Math.Max(A.SamplingRate, B.SamplingRate);
            return temp;
        }

        public static Signal operator +(Signal A, Signal B)
        {
            if (A.Samples.Length != B.Samples.Length)
            {
                throw new InvalidOperationException("Both the operand signals must have equal number of samples");
            }
            if (A.SamplingRate != B.SamplingRate)
            {
                System.Diagnostics.Debug.Print("WARNING: Two signals with different sampling rates are being added");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] + B.Samples[i];
            }
            temp.SamplingRate = Math.Max(A.SamplingRate, B.SamplingRate);
            return temp;
        }

        public static Signal operator +(Signal A, double B)
        {
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] + B;
            }
            temp.SamplingRate = A.SamplingRate;
            return temp;
        }
        public static Signal operator +(Signal A, float B)
        {
            return A + (double)B;
        }


        public static Signal operator +(Signal A, double[] B)
        {
            if (A.Samples.Length != B.Length)
            {
                throw new InvalidOperationException("Both the operands must have equal number of samples");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] + B[i];
            }
            temp.SamplingRate = A.SamplingRate;
            return temp;
        }

        public static Signal operator +(Signal A, float[] B)
        {
            if (A.Samples.Length != B.Length)
            {
                throw new InvalidOperationException("Both the operands must have equal number of samples");
            }
            Signal temp = new Signal();
            temp.Samples = new double[A.Samples.Length];
            for (int i = 0 ; i < A.Samples.Length ; i++)
            {
                temp.Samples[i] = A.Samples[i] + B[i];
            }
            temp.SamplingRate = A.SamplingRate;
            return temp;
        }

        public double SignalEnergy()
        {
            double energy = 0;
            for (int i = 0 ; i < this.Samples.Length ; i++)
            {
                energy += Math.Pow(Math.Abs(this.Samples[i]), 2);
            }
            return energy / (this.Samples.Length);
        }




        public IEnumerator<double> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._Samples.GetEnumerator();
        }
    }
}
