using System;
using StackExchange.Redis;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQueryBuilder
    {
        private readonly IRedisConnection _redisDb;

        public GetUserNotificationsQueryBuilder(IRedisConnection redisDb)
        {
            _redisDb = redisDb;
        }

        public GetUserNotificationsQuery Build(PagingParams pagingParams, Guid userId)
        {
            return new GetUserNotificationsQuery(_redisDb, pagingParams, userId);
        }

        public GetUserNotificationsQuery Build(Guid userId)
        {
            return new GetUserNotificationsQuery(_redisDb, PagingParams.All, userId);
        }

    }
}