using Common;
using System;
using System.Threading.Tasks;

namespace Broker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker started: ");

            BrokerSocket brokerSocket = new BrokerSocket();
            brokerSocket.Start(BrokerSettings.Ip, BrokerSettings.Port);

            var worker = new Worker();
            Task.Factory.StartNew(worker.SendMessageWork, TaskCreationOptions.LongRunning);

            Console.ReadLine();
        }
    }
}
