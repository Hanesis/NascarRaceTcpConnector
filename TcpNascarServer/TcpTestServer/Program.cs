using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using TcpTestServer.Competition;
using TcpTestServer.Competition.Tires;
using System.Text.Json;
using Newtonsoft.Json;

namespace TcpTestServer
{
    class TcpTestServer
    {

        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }
            
            Console.WriteLine("Server is running");
            
            var clients = new Dictionary<TcpClient, Racer>();
            var grid = new List<Racer>();

            while (clients.Count < 2)
            {
                try
                {
                    if (listener.Pending())
                    {
                        var client = listener.AcceptTcpClient(); 
                       
                        var racerName = TcpConnector.GetMessage(client);
                        Console.WriteLine("Client {0} connected", racerName);
                        var newRacer = new Racer(37, racerName, 10, new HardTires());
                        grid.Add(newRacer);
                        clients.Add(client, newRacer);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            foreach (var client in clients)
            {
                TcpConnector.SendMessage(client.Key, "Grid is full and ready:");
                var some = "";
                foreach (var racer in grid)
                {
                    some += racer + "\n";
                }
                TcpConnector.SendMessage(client.Key, some);

            }

            Console.WriteLine("Server is ready");

            while (true)
            {
                foreach (var client in clients)
                    try
                    {
                        if (client.Key.GetStream().DataAvailable)
                        {
                            var msg = TcpConnector.GetMessage(client.Key);
                            Console.WriteLine(msg);
                            TcpConnector.SendMessage(client.Key, msg);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
            }

           
            /*foreach (TcpClient client in client)
            {
                client.Close();
            }*/
            

        }
    }
}
