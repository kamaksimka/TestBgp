using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestBgp
{
    public class BgpClient
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly int _asn;

        public BgpClient(string ip, int port, int asn)
        {
            _ip = ip;
            _port = port;
            _asn = asn;
        }

        public void Run()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    Console.WriteLine("Connecting...");
                    client.Connect(_ip, _port);

                    NetworkStream stream = client.GetStream();

                    // Создаем OPEN сообщение
                    byte[] openMessage = BgpMessageBuilder.BuildOpenMessage(_asn);

                    Console.WriteLine("Sending OPEN...");
                    stream.Write(openMessage, 0, openMessage.Length);

                    // Читаем ответ
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    Console.WriteLine("Received:");
                    for (int i = 0; i < bytesRead; i++)
                    {
                        Console.Write($"{buffer[i]:X2} ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
