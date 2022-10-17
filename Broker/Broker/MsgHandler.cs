using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Broker
{
    public class MsgHandler
    {
        public static void Handle(byte[] msgBytes, ConnectionInfo connectionInfo)
        {
            var msgString = Encoding.UTF8.GetString(msgBytes);

            if (msgString.StartsWith("sub/"))
            {
                connectionInfo.Topic = msgString.Split("sub/").LastOrDefault().Replace("\0", "");
                ConnStorage.Add(connectionInfo);
            }
            else
            {
                MsgBase msg = JsonConvert.DeserializeObject<MsgBase>(msgString);
                MsgStorage.Add(msg);
            }
        }
    }
}
