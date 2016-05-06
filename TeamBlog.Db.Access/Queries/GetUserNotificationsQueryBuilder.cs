using System;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections;
using TeamBlog.RedisAccess.Collections.SortedSet;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQueryBuilder
    {
        private readonly IRedisConnection _redisDb;
        private readonly SortedSetReaderBuilder _sortedSetReaderBuilder;

        public GetUserNotificationsQueryBuilder(IRedisConnection redisDb, SortedSetReaderBuilder sortedSetReaderBuilder)
        {
            _redisDb = redisDb;
            _sortedSetReaderBuilder = sortedSetReaderBuilder;
        }

        public GetUserNotificationsQuery Build(PagingParams pagingParams, Guid userId)
        {
            return new GetUserNotificationsQuery(_redisDb, pagingParams, userId, _sortedSetReaderBuilder);
        }

        public GetUserNotificationsQuery Build(Guid userId)
        {
            return new GetUserNotificationsQuery(_redisDb, PagingParams.All, userId, _sortedSetReaderBuilder);
        }

    }
}