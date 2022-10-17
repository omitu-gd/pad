using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Broker
{
    public class ConnStorage
    {
        private static List<ConnectionInfo> _connections = new List<ConnectionInfo>();
        private static object _locker = new object();
        public static void Add(ConnectionInfo connection)
        {
            lock(_locker)
            {
                _connections.Add(connection);
            }        
        }
        public static void Remove(string address)
        {
            lock (_locker)
            {
                _connections.RemoveAll(x => x.Address == address);
            }
        }
        public static List<ConnectionInfo> GetConnectionsByTopic(string topic)
        {
            lock (_locker)
            {
                return _connections.Where(x => x.Topic == topic).ToList();
            }
        }
    }
}
