using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP_server.Server;

namespace MyServer
{
    public class myserver : AbstractTCPServer
    {
        public myserver(int port, int stopport, string name) : base( port,stopport,name)
        {
        }
        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
           string l = reader.ReadLine();
            writer.WriteLine("Hello " + l);
        }
    }





}
