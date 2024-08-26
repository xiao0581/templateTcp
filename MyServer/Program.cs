// See https://aka.ms/new-console-template for more information
using MyServer;

Console.WriteLine("Hello, World!");
myserver server = new myserver(7007, "My");
    server.Start();