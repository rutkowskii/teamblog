using System;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetChannelSubscribersQuery : IQuery<RedisValue> //todo should queries expose redis values?
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private IDatabase _redisDb;

        public GetChannelSubscribersQuery(IRedisConnection redisConnection, Guid channelId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId;
        }

        public RedisValue[] Run()
        {
            _redisDb = _redisConnection.AccessRedis();
            var subscribers = _redisDb
                .SetMembers(RedisDbObjects.ChannelSubscribersKey(this._channelId));
            return subscribers.ToArray();
        }
    }
}
