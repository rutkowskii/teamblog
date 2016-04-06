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

        public RedisConnection(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }

        public IDatabase AccessRedis()
        {
            var db = _redis.GetDatabase();
            return db;
        }
    }

    public interface IRedisConnection
    {
        IDatabase AccessRedis();
    }
    
}
