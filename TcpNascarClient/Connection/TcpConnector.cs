using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpClient
{
    class TcpConnector
    {
        System.Net.Sockets.TcpClient client;

        public static void SendMessage(System.Net.Sockets.TcpClient client, string message)
        {
            var byteBuffer = Encoding.UTF8.GetBytes(message);
            var netStream = client.GetStream();
            netStream.Write(byteBuffer, 0, byteBuffer.Length);
            netStream.Write(new byte[] { 0 }, 0, sizeof(byte));
            netStream.Flush();
        }

        public static string GetMessage(System.Net.Sockets.TcpClient client)
        {
            var buffer = new List<int>();
            var stream = client.GetStream();
            int readByte;
            while ((readByte = stream.ReadByte()) != 0)
            {
                buffer.Add(readByte);
            }
            return Encoding.UTF8.GetString(buffer.Select(b => (byte)b).ToArray(), 0, buffer.Count);
        }
    }
}
