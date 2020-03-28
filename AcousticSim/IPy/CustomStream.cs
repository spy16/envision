using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.IPy
{

    public class CustomStream : MemoryStream
    {

        Object _output;
        private byte[] buffer;
        private int offset;

        private int count;

        public CustomStream(object textbox)
        {
            if (textbox == null)
            {
                textbox = new Object();
            }
            _output = textbox;
        }


        private void _write()
        {
            string s = Encoding.UTF8.GetString(buffer, offset, count);
            if (_output.GetType() == typeof(TextBox))
            {
                ((TextBox)_output).AppendText(s);
            }
            else if (_output.GetType() == typeof(RichTextBox))
            {
                ((RichTextBox)_output).AppendText(s);
            }
            else
            {
                Debug.Print(s);
            }
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.buffer = buffer;
            this.offset = offset;
            this.count = count;
            _write();
        }
    }
}
