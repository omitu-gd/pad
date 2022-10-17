using Common;
using System;

namespace Subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Subscriber started. ");
            string topic;

            Console.WriteLine("Enter the topic: ");
            topic = Console.ReadLine().ToLower();

            var subscriberSocket = new SubscriberSocket(topic);
            subscriberSocket.Connect(BrokerSettings.Ip, BrokerSettings.Port);

            Console.ReadLine();
        }
    }
}
