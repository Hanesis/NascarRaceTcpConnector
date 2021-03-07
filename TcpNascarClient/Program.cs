using System;
using System.Timers;

namespace TcpClient
{
    class Program
    {
        static System.Net.Sockets.TcpClient client = null;
        private static Timer aTimer;
        static void Main(string[] args)
        {
            Console.WriteLine("Set racers name:");
            var nickName = Console.ReadLine();
            
            const string server = "localhost";

            try
            {
                Console.WriteLine("Starting Client for racer {0}", nickName);

                client = new System.Net.Sockets.TcpClient(server, 8080);

                TcpConnector.SendMessage(client, nickName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }

            Console.WriteLine("Connection established");
            SetListenerTimer();

            while (true)
            {
                var msg = Console.ReadLine();
                if (msg != "")
                {
                    TcpConnector.SendMessage(client, msg);
                }
                
            }
            aTimer.Stop();
            aTimer.Dispose();

        }
        private static void SetListenerTimer()
        {
            aTimer = new Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (client.GetStream().DataAvailable)
            {
                var serverMsg = TcpConnector.GetMessage(client);
                Console.WriteLine(serverMsg);
            }
        }
    }
}
