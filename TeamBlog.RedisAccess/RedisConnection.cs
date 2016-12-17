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

        public IDatabase Db
        {
            get
            {
                var db = _redis.GetDatabase(_connectionParamsProvider.DatabaseNumber);
                return db;
            }
        }

        public void FlushDb()
        {
            var server = _redis.GetServer(_connectionParamsProvider.ServerUrl, _connectionParamsProvider.ServerPort);
            server.FlushDatabase(_connectionParamsProvider.DatabaseNumber);
        }
    }
}
