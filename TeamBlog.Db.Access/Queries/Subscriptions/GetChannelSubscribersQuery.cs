using System;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

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
            _redisDb = _redisConnection.Db;
            var subscribers = _redisDb
                .SetMembers(RedisModel.ChannelSubscribers.KeyFor(_channelId))
                .ToArray();

            var subscribersParsed = subscribers
                .Select(s => s.ToGuid())
                .ToArray();

            return subscribersParsed;
        }
    }
}
