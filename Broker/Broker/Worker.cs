using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Broker
{
    public class Worker
    {
        public void SendMessageWork()
        {
            while (true)
            {
                
                while (!MsgStorage.IsEmpty())
                {
                    var msg = MsgStorage.GetNext();

                    if (msg != null)
                    {
                        var conns = ConnStorage.GetConnectionsByTopic(msg.Topic);
                        foreach(var conn in conns)
                        {
                            var msgString = JsonConvert.SerializeObject(msg);
                            byte[] data = Encoding.UTF8.GetBytes(msgString);
                            conn.Socket.Send(data);
                        }
                    }
                    Thread.Sleep(300);
                }
                
            }
        }
    }
}
