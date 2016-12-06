using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetUserChannelsQuery : IQuery<RedisValue> //todo queries should not expose redis values. 
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _userId;

        public GetUserChannelsQuery(IRedisConnection redisConnection, Guid userId)
        {
            _redisConnection = redisConnection;
            _userId = userId;
        }

        public RedisValue[] Run()
        {
            var redisDb = _redisConnection.AccessRedis(); //todo dry, extensions methods for redisDb?
            var channels = redisDb
                .SetMembers(RedisDbObjects.UserChannelsKey(_userId));
            return channels.ToArray();
        }
    }
}