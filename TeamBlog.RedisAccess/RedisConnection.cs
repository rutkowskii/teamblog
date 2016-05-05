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
        private readonly IRedisConnectionParamsProvider _connectionParamsProvider;
        private readonly ConnectionMultiplexer _redis;

        public RedisConnection(IRedisConnectionParamsProvider connectionParamsProvider)
        {
            _connectionParamsProvider = connectionParamsProvider;
            _redis = ConnectionMultiplexer.Connect(_connectionParamsProvider.ConnectionString);
        }

        public IDatabase AccessRedis()
        {
            var db = _redis.GetDatabase(_connectionParamsProvider.DatabaseNumber); 
            return db;
        }

        public void Flush()
        {
            var server = _redis.GetServer("127.0.0.1", 6379);
            server.FlushDatabase(1);
        }
    }
}
