using System;
using System.Text;
using System.Net.Sockets;

namespace TcpClient
{
    class Program
    {
        private const string IpAddress = "127.0.0.1";
        private const int Port = 30001;

        private const string EndConnectionPhrase = "EOF"; 

        static void Main(string[] args)
        {
            var connectionSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                connectionSocket.Connect(IpAddress, Port);
            }
            catch
            {
                Console.WriteLine("Seems like no one waiting for our messages :(\nClosing application...");
                connectionSocket.Close();
                return;
            }

            while (true)
            {
                Console.Write("Message to the server: ");
                var messageToSend = Console.ReadLine();
                var bytesToSend = Encoding.UTF8.GetBytes(messageToSend);

                try
                {
                    connectionSocket.Send(bytesToSend);
                }
                catch
                {
                    Console.WriteLine("The connection was aborted by the remote server.");
                    connectionSocket.Close();
                    return;
                }

                if (messageToSend.Equals(EndConnectionPhrase))
                {
                    break;
                }
            }

            connectionSocket.Close();
        }
    }
}