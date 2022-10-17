using Common;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Producer started: ");
            PublisherSocket socket = new PublisherSocket();
            socket.Connect(BrokerSettings.Ip, BrokerSettings.Port);

            if (socket.IsConnected)
            {
                while (true)
                {
                    MsgBase msg = new MsgBase();
                    Console.Write("topic: ");
                    msg.Topic = Console.ReadLine();
                    Console.WriteLine("message: ");
                    msg.Message = Console.ReadLine();
                    byte[] msgBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
                    socket.Send(msgBytes);
                }                
            } 
            
            
        }
    }
}
