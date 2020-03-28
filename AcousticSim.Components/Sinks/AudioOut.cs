using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.ComponentModel;

namespace Envision.Components.Sinks
{

    #region [ Raw Sample Player Wrapper ]
    public abstract class WaveProvider32Base : IWaveProvider
    {


        private WaveFormat wavefrmt;


        public void SetFormat(int sampleRate, bool stereo)
        {
            int chs = 0;
            if (stereo == true)
            {
                chs = 2;
            }
            else
            {
                chs = 1;
            }
            this.wavefrmt = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, chs);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer buf = new WaveBuffer(buffer);
            int sReq = count / 4;
            int samples_read = Read(buf.FloatBuffer, offset / 4, sReq);
            return (int)(samples_read * 4);
        }

        public abstract int Read(float[] buffer, int offset, int count);



        public NAudio.Wave.WaveFormat WaveFormat
        {
            get { return wavefrmt; }
        }
    }
    public class AnalogOut : WaveProvider32
    {


        private WaveOut wvOut = new WaveOut();
        private double _A;
        private double fs;

        private long _offset = 0;

        private float[] _buffer;
        public bool AutoPlayOnUpdate { get; set; }

        private void _init(int fs = 44100, double amplitude = 0.5)
        {
            _A = amplitude;
            this.SetWaveFormat(fs, 1);
        }

        public AnalogOut(float[] buffer, int fs = 44100, double amplitude = 0.5)
        {
            _buffer = buffer;
            _init(fs, amplitude);
        }

        private float[] ToFloatArray(double[] buffer)
        {
            float[] _buffer = new float[buffer.Length];
            for (int i = 0 ; i < buffer.Length ; i++)
            {
                _buffer[i] = (float)buffer[i];
            }
            return _buffer;
        }

        public AnalogOut(double[] buffer, int fs = 44100, double amplitude = 0.5)
        {
            _buffer = ToFloatArray(buffer);
            _init(fs, amplitude);
        }



        public void Play()
        {
            wvOut.Init(this);
            wvOut.Play();
        }

        public void Continue()
        {
            if (wvOut.PlaybackState == PlaybackState.Paused)
            {
                wvOut.Play();
            }
        }
        public void Pause()
        {
            wvOut.Pause();
        }

        public void Stop()
        {
            _offset = 0;
            _buffer = new float[0];
            wvOut.Stop();
            System.Diagnostics.Debug.Print("stopped");
        }

        public bool IsPlaying()
        {
            if (wvOut.PlaybackState == PlaybackState.Playing)
            {
                return true;
            }
            return false;
        }

        public int CurrentBufferLength { get { return _buffer.Length; } }

        public int Update(float[] newBuffer, int newFs = 44100)
        {
            this.SetWaveFormat(newFs, 1);
            if (wvOut.PlaybackState == PlaybackState.Playing)
            {
                _buffer = _buffer.Concat(newBuffer.ToArray()).ToArray();
            }
            else
            {
                _offset = 0;
                _buffer = newBuffer.ToArray();
                if (this.AutoPlayOnUpdate)
                {
                    this.Play();
                }
            }
            return newBuffer.Length;
        }

        public int Update(double[] newBuffer, int newFs = 44100)
        {
            return Update(ToFloatArray(newBuffer), newFs);
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            if (_offset + sampleCount > _buffer.Length)
            {
                for (int i = 0 ; i <= sampleCount ; i++)
                {
                    buffer[i] = 0;
                }
                wvOut.Stop();
                _offset = 0;
            }
            else
            {
                for (int i = 0 ; i <= sampleCount ; i++)
                {
                    buffer[i] = _buffer[_offset + i];
                }
                _offset += sampleCount;
            }
            return sampleCount;
        }
    }
    #endregion

    [Serializable]
    public class AudioOut : Blocks.BlockBase
    {
        [NonSerialized]
        BackgroundWorker bgWorker = new BackgroundWorker();

        [NonSerialized]
        AnalogOut aOut;

        public AudioOut()
        {
            Logger.D((this.VisualElement == null).ToString());
        }

        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Plays an array of floating point values as an audible tone"; }
        }

        public override string ProcessingType
        {
            get { return "Sinks"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            if (InputNodes[0].ConnectingNode != null)
            {
                var obj = InputNodes[0].Object;
                if (Utils.IsSignal(obj))
                {
                    var sig = Utils.AsSignal(obj);
                    double[] samples = sig.Samples;
                    bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += delegate(object sender, DoWorkEventArgs ex) {
                        if (aOut == null)
                        {
                            aOut = new AnalogOut(samples, (int)sig.SamplingRate);
                            aOut.AutoPlayOnUpdate = true;
                            aOut.Play();
                        }
                        else
                        {
                            aOut.Update(samples);
                        }
                    };
                    bgWorker.RunWorkerAsync();
                    System.Threading.Thread.Sleep(100);
                    while (aOut.IsPlaying()) { System.Threading.Thread.Sleep(100); }
                    aOut.Stop();
                }
            }
        }



        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            InputNodes = new List<Blocks.BlockInputNode>(){
               new Blocks.BlockInputNode(ref root, "input", "in")
           };
        }
    }
}
