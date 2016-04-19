using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBlog.RedisAccess
{
    /// <summary>
    /// This one should be singleton
    /// </summary>
    public class RedisConnection : IRedisConnection
    {
        private ConnectionMultiplexer _redis;

        private const string ConnectionString = "127.0.0.1:6379,allowAdmin=true";

        public RedisConnection()
        {
            _redis = ConnectionMultiplexer.Connect(ConnectionString); //todo connection string ioc. 
        }

        public IDatabase AccessRedis()
        {
            var db = _redis.GetDatabase(1); //todo 1 goes to ioc
            return db;
        }

        public void Flush()
        {
            var server = _redis.GetServer("127.0.0.1", 6379);
            server.FlushDatabase(1);
        }
    }
}
