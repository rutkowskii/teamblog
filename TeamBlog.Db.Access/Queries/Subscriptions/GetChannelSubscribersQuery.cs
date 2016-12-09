using System;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetChannelSubscribersQuery : IQuery<Guid>
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private IDatabase _redisDb;

        public GetChannelSubscribersQuery(IRedisConnection redisConnection, Guid channelId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId;
        }

        public Guid[] Run()
        {
            _redisDb = _redisConnection.AccessRedis();
            var subscribers = _redisDb
                .SetMembers(RedisDbObjects.ChannelSubscribersKey(this._channelId))
                .ToArray();

            var subscribersParsed = subscribers
                .Select(s => (string) s)
                .Select(str => new Guid(str))
                .ToArray();

            return subscribersParsed;
        }
    }
}
