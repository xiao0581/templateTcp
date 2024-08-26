using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP_server;

namespace MyServer
{
    public class myserver : AbstractTCPServer
    {
        public myserver(int port, string serverName) : base(port, serverName)
        {
        }

        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
            string line = reader.ReadLine();

            // Capitalize the line and write it back
            if (line != null)
            {
                string capitalizedLine = line.ToUpper();
                writer.WriteLine(capitalizedLine);
            }
        }
    }
    
       
    


}
