using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Broker
{
    public class MsgStorage
    {
        private static ConcurrentQueue<MsgBase> _msgQueue = new ConcurrentQueue<MsgBase>();

        public static void Add(MsgBase msg)
        {
            _msgQueue.Enqueue(msg);
        }
        public static MsgBase GetNext()
        {
            MsgBase msg;
            _msgQueue.TryDequeue(out msg);
            return msg;
        }
        public static bool IsEmpty() => _msgQueue.IsEmpty;

    }
}
