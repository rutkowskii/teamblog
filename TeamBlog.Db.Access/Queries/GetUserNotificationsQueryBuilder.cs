using System;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.RedisAccess.Collections.SortedSet;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQueryBuilder
    {
        private readonly SortedSetReaderBuilder _sortedSetReaderBuilder;
        private readonly HashReaderBuilder<PostAddedUserNotification> _hashReaderBuilder;

        public GetUserNotificationsQueryBuilder(SortedSetReaderBuilder sortedSetReaderBuilder,
            HashReaderBuilder<PostAddedUserNotification> hashReaderBuilder)
        {
            _sortedSetReaderBuilder = sortedSetReaderBuilder;
            _hashReaderBuilder = hashReaderBuilder;
        }

        public GetUserNotificationsQuery Build(PagingParams pagingParams, Guid userId)
        {
            return new GetUserNotificationsQuery(pagingParams, userId, _sortedSetReaderBuilder, _hashReaderBuilder);
        }

        public GetUserNotificationsQuery Build(Guid userId)
        {
            return new GetUserNotificationsQuery(PagingParams.All, userId, _sortedSetReaderBuilder, _hashReaderBuilder);
        }
    }
}