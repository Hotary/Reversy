using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestConsole
{
    class BufferedOutput : TextWriter
    {
        public string Buffer = "";

        public override void Write(char value)
        {
            Buffer += value;
        }

        public override void Write(string value)
        {
            Buffer += value;
        }

        public string Pop()
        {
            var old = Buffer;
            Buffer = "";
            return old;
        }

        public override Encoding Encoding
        {
            get
            {
                return Encoding.ASCII;
            }
        }
    }
}
