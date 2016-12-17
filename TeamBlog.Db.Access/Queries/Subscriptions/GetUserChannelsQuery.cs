using System;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetUserChannelsQuery : IQuery<Guid> 
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _userId;

        public GetUserChannelsQuery(IRedisConnection redisConnection, Guid userId)
        {
            _redisConnection = redisConnection;
            _userId = userId;
        }

        public Guid[] Run()
        {
            var channels = _redisConnection
                .Db
                .SetMembers(RedisDbObjects.UserChannelsKey(_userId))
                .Select(ch => ch.ToGuid())
                .ToArray();
            return channels;
        }
    }
}