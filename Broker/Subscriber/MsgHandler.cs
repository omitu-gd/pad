using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Subscriber
{
    internal class MsgHandler
    {
        public static void Handle(byte[] msgBytes)
        {
            var msgString = Encoding.UTF8.GetString(msgBytes);
            var msg = JsonConvert.DeserializeObject<MsgBase>(msgString);

            Console.WriteLine($"Received message: {msg.Message}");
        }
    }
}
