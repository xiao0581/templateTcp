using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.div
{

    public class JsonTracListener:TraceListener
    {
        private string filePath;

        public JsonTracListener(string filePath)
        {
            this.filePath = filePath;
        }

       
        public override void Write(string message)
        {
           

        }

        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }
    }
}
