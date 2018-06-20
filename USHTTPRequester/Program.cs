using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace USHTTPRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("USHTTPRequester stands for Ultra Simple HTTP Requester");
            var uri = new Uri(args[0]);

            var host = uri.Host;
            var hostAddresses = Dns.GetHostAddresses(host);

            IPEndPoint ipEndPoint = new IPEndPoint(hostAddresses[0], 80);
            var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
            if (socket.Connected)
            {
                var httpGET = "GET " + "/" + " HTTP/1.1\r\nHost: " + host + "\r\n\r\n";
                socket.Send(Encoding.ASCII.GetBytes(httpGET));

                var buffer = new byte[256];
                string result = "";
                int receivedBytes;
                do
                {
                    receivedBytes = socket.Receive(buffer);
                    result += Encoding.ASCII.GetString(buffer, 0, receivedBytes);
                } while (receivedBytes == 256);

                Console.WriteLine(result);
            }

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
