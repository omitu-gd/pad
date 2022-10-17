using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Subscriber
{
    internal class SubscriberSocket
    {
        private Socket _socket;
        private string _topic;
        public SubscriberSocket(string topic)
        {
            _topic = topic;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string IpAddress, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(IpAddress), port), ConnectedCallback, null);
        }

        private void ConnectedCallback(IAsyncResult asyncResult)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("INFO: Subscriber connected to broker");
                Subscribe();
                StartReceive();
            }          
        }

        private void StartReceive()
        {
            ConnectionInfo connection = new ConnectionInfo();
            connection.Socket = _socket;

            _socket.BeginReceive(connection.Data, 0, ConnectionInfo.DataSize, SocketFlags.None, ReceiveCallBack, connection);

        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            ConnectionInfo connectionInfo = asyncResult.AsyncState as ConnectionInfo;
            try
            {
                SocketError response;
                int dataSize = _socket.EndReceive(asyncResult, out response);

                if(response == SocketError.Success)
                {
                    byte[] msgbytes = connectionInfo.Data;
                    MsgHandler.Handle(msgbytes);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Cannot receiva data from broker. {e.Message}");
            }
            finally
            {
                try
                {
                    _socket.BeginReceive(connectionInfo.Data, 0, ConnectionInfo.DataSize, SocketFlags.None, ReceiveCallBack, connectionInfo);
                }
                catch (Exception e)
                {
                    connectionInfo.Socket.Close();
                    Console.WriteLine($"{e.Message}");
                }
            }
        }

        private void Subscribe()
        {
            var data = Encoding.UTF8.GetBytes("sub/" + _topic);
            Send(data);
        }

        private void Send(byte[] data)
        {
            try
            {
                _socket.Send(data);
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Cannot send data. {e.Message}");
            }
        }
    }
}
