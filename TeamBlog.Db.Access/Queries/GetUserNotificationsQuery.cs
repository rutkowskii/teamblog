using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.RedisAccess.Collections.SortedSet;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQuery : IQuery<PostAddedUserNotification>
    {
        private readonly HashReaderBuilder<PostAddedUserNotification> _hashReaderBuilder;
        private readonly PagingParams _pagingParams;
        private readonly Guid _userId;
        private readonly SortedSetReaderBuilder _sortedSetReaderBuilder;

        public GetUserNotificationsQuery(PagingParams pagingParams, Guid userId,
            SortedSetReaderBuilder sortedSetReaderBuilder,
            HashReaderBuilder<PostAddedUserNotification> hashReaderBuilder)
        {
            _pagingParams = pagingParams;
            _userId = userId;
            _sortedSetReaderBuilder = sortedSetReaderBuilder;
            _hashReaderBuilder = hashReaderBuilder;
        }

        public PostAddedUserNotification[] Run()
        {
            var userNotificationsKey = RedisDbObjects.UserNotificationsKey(_userId);
            var notificationIds = ResolveNotificationIds(userNotificationsKey);
            var results = ReadNotifications(notificationIds).ToArray();
            return results;
        }

        private RedisValue[] ResolveNotificationIds(string userNotificationsKey)
        {
            var accesor = _sortedSetReaderBuilder.Build(userNotificationsKey, Order.Descending, _pagingParams);
            var notificationIds = accesor.Resolve();
            return notificationIds;
        }

        private IEnumerable<PostAddedUserNotification> ReadNotifications(RedisValue[] notificationIds)
        {
            foreach (var notificationId in notificationIds)
            {
                var hashIdentifier = RedisDbObjects.NotificationsKey(notificationId);
                var reader = _hashReaderBuilder.Build(hashIdentifier);
                yield return reader.Read();
            }
        }
    }
}