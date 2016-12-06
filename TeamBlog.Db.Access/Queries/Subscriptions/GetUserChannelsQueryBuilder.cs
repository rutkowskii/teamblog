using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetUserChannelsQueryBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public GetUserChannelsQueryBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public GetUserChannelsQuery Build(Guid userId)
        {
            return new GetUserChannelsQuery(this._redisConnection, userId);
        }
    }
}