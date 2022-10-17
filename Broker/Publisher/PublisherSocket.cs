using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Publisher
{
    internal class PublisherSocket
    {
        private Socket _socket;
        public bool IsConnected
        {
            get => _socket.Connected;
        }
        public PublisherSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Send(byte[] msg)
        {
            try
            {
                _socket.Send(msg);
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Could not send message. {e.Message}");
            }
        }

        public void Connect(string IpAddress, int port)
        {
            _socket.Connect(IpAddress, port);
        }
    }
}
