using System;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetChannelsSubscribersQuery : IQuery<Guid>
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid[] _channelIds;
        private IDatabase _redisDb;

        public GetChannelsSubscribersQuery(IRedisConnection redisConnection, Guid[] channelIds)
        {
            _redisConnection = redisConnection;
            _channelIds = channelIds;
        }

        public Guid[] Run()
        {
            _redisDb = _redisConnection.Db;
            var subscribers = _channelIds.SelectMany(GetChannelSubscribers);

            var subscribersParsed = subscribers
                .Select(s => s.ToGuid())
                .Distinct()
                .ToArray();

            return subscribersParsed;
        }

        private RedisValue[] GetChannelSubscribers(Guid channelId)
        {
            var subscribers = _redisDb
                .SetMembers(RedisModel.ChannelSubscribers.KeyFor(channelId))
                .ToArray();
            return subscribers;
        }
    }
}
