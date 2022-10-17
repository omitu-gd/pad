using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Broker
{
    public class BrokerSocket
    {
        private Socket _socket;
        public BrokerSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start(string ip, int port)
        {
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socket.Listen(BrokerSettings.ConnectionsLimit);
            Accept();
        }

        private void Accept()
        {
            _socket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            ConnectionInfo connection = new ConnectionInfo();

            try
            {
                connection.Socket = _socket.EndAccept(asyncResult);
                connection.Address = connection.Socket.RemoteEndPoint.ToString();
                connection.Socket.BeginReceive(connection.Data, 0, ConnectionInfo.DataSize, SocketFlags.None, ReceiveCallBack, connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Cannot accept message.{ ex.Message}");
            }
            finally
            {
                Accept();
            }
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            ConnectionInfo connection = (ConnectionInfo)asyncResult.AsyncState;
            try
            {
                Socket socket = connection.Socket;
                SocketError response;
                socket.EndReceive(asyncResult, out response);

                if(response == SocketError.Success)
                {
                    MsgHandler.Handle(connection.Data, connection);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Cannot receive message. {e.Message}");
            }
            finally
            {
                try
                {
                    connection.Socket.BeginReceive(connection.Data, 0, ConnectionInfo.DataSize, SocketFlags.None, ReceiveCallBack, connection);
                }
                catch(Exception e)
                {                   
                    var address = connection.Socket.RemoteEndPoint.ToString();
                    connection.Socket.Close();
                    ConnStorage.Remove(address);
                    Console.WriteLine($"ERROR: Disconnected. {e.Message}");
                }
            }

        }
    }
}
