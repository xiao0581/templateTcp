﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TCP_server.Server
{
    public abstract class AbstractTCPServer
    {
        // instance fields
        private const int SEC = 1000;
        private readonly IPAddress ListenOnIPAddress = IPAddress.Any;
        private bool running = true;
        private readonly List<Task> clients = new List<Task>();



        //properties

        /// <summary>
        /// Get the port number the server is starting on
        /// </summary>
        public int PORT { get; private set; }

        /// <summary>
        /// Get the the port number of the stopping server
        /// </summary>
        public int STOPPORT { get; private set; }

        /// <summary>
        /// Get the name given to the server
        /// </summary>
        public string NAME { get; private set; }
        public AbstractTCPServer(int port, string name) : this(port, port + 1, name)
        {
        }

        public AbstractTCPServer(int port, int stopport, string name)
        {
            PORT = port;
            STOPPORT = stopport;
            NAME = name;
        }

        /// <summary>
        /// Start the server
        /// </summary>
        public void Start()
        {
            // start stop server
            Task.Run(TheStopServer); // kort for Task.Run( ()=>{ TheStopServer(); });


            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();
            Console.WriteLine("Server started");

            while (running)
            {
                if (listener.Pending()) // der findes en klient
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Client incoming");
                    Console.WriteLine($"remote (ip,port) = ({client.Client.RemoteEndPoint})");

                    clients.Add(
                        Task.Run(() =>
                        {
                            TcpClient tmpClient = client;
                            DoOneClient(client);
                        })
                        );
                }
                else  // der er PT ingen klient
                {
                    Thread.Sleep(2 * SEC);
                }

            }
            // vente på alle task bliver færdige
            Task.WaitAll(clients.ToArray());

            Console.WriteLine("Server stopped");
        }

        private void DoOneClient(TcpClient client)
        {
            using (StreamReader sr = new StreamReader(client.GetStream()))
            using (StreamWriter sw = new StreamWriter(client.GetStream()))
            {
                sw.AutoFlush = true;
                Console.WriteLine("Handle one client");

                TcpServerWork(sr, sw);
            }
        }

        /// <summary>
        /// This method implement what is specific for this server 
        /// e.g. if this is an echo server read from sr and write to sw
        /// </summary>
        /// <param name="sr">The streamreader from where you can read strings from the socket</param>
        /// <param name="sw">The streamwriter whereto you can write strings to the socket</param>
        protected abstract void TcpServerWork(StreamReader sr, StreamWriter sw);




        /*
        * stop server
        */
        private void StoppingServer()
        {
            running = false;
        }

        private void TheStopServer()
        {
            TcpListener listener = new TcpListener(ListenOnIPAddress, STOPPORT);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient();
            //todo tjek om det er lovligt fx et password

            StoppingServer();
            client?.Close();
            listener?.Stop(); // bare for at være pæn - det hele lukker alligevel
        }

        

    }
}
