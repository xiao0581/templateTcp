using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCP_server
{
    public abstract class AbstractTCPServer
    {
        
        private readonly int _port;
        private readonly string _serverName;

        protected AbstractTCPServer(int port, string serverName)
        {
            _port = port;
            _serverName = serverName;
        }
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"{_serverName} started at port {_port}");


            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client incoming");
                Console.WriteLine($"remote (ip,port) = ({client.Client.RemoteEndPoint})");

                Task.Run(() =>
                {
                    TcpClient tmpClient = client;
                    DoOneClient(client);
                });

            }
        }

        private void DoOneClient(TcpClient sock)
        {
            using (StreamReader sr = new StreamReader(sock.GetStream()))
            using (StreamWriter sw = new StreamWriter(sock.GetStream()))
            {
                sw.AutoFlush = true;
                Console.WriteLine("Handle one client");

                // simple echo
                String? s = sr.ReadLine();
                sw.WriteLine(s);
            }

        }
        protected abstract void TcpServerWork(StreamReader reader, StreamWriter writer);
    }
}
