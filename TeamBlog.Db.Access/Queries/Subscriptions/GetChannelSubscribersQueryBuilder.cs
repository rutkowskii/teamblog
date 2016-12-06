using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public class GetChannelSubscribersQueryBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public GetChannelSubscribersQueryBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public GetChannelSubscribersQuery Build(Guid channelId)
        {
            return new GetChannelSubscribersQuery(this._redisConnection, channelId);
        }
    }
}