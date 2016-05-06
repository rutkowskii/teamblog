using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections;
using TeamBlog.RedisAccess.Collections.SortedSet;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQuery : IQuery<PostAddedUserNotification>
    {
        private IDatabase _redisDb;
        private readonly IRedisConnection _redisConnection;
        private readonly PagingParams _pagingParams;
        private readonly Guid _userId;
        private readonly SortedSetReaderBuilder _sortedSetReaderBuilder;

        public GetUserNotificationsQuery(IRedisConnection redisConnection, PagingParams pagingParams, Guid userId, SortedSetReaderBuilder sortedSetReaderBuilder)
        {
            _redisConnection = redisConnection;
            _pagingParams = pagingParams;
            _userId = userId;
            _sortedSetReaderBuilder = sortedSetReaderBuilder;
        }

        public IEnumerable<PostAddedUserNotification> Run()
        {
            _redisDb = _redisConnection.AccessRedis();

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
                var notificationData = _redisDb.HashGet(RedisDbObjects.NotificationsKey(notificationId),
                    new RedisValue[] {"Timestamp", "Content"}); //todo better way to deal with hash fields. 
                yield return new PostAddedUserNotification
                {
                    Id = Guid.Parse(notificationId),
                    Timestamp = DateTime.Parse(notificationData[0]),
                    Content = notificationData[1]
                };
            }
        }
    }
}
