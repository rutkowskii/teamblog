using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUserNotificationsQuery : IQuery<PostAddedUserNotification>
    {
        private IDatabase _redisDb;
        private readonly IRedisConnection _redisConnection;
        private readonly PagingParams _pagingParams;
        private readonly Guid _userId;

        public GetUserNotificationsQuery(IRedisConnection redisConnection, PagingParams pagingParams, Guid userId)
        {
            _redisConnection = redisConnection;
            _pagingParams = pagingParams;
            _userId = userId;
        }

        public IEnumerable<PostAddedUserNotification> Run()
        {
            _redisDb = _redisConnection.AccessRedis();

            var userNotificationsKey = RedisDbObjects.UserNotificationsKey(_userId);
            var userMaxScoreKey = RedisDbObjects.UserNotificationsNextElementKey(_userId);

            var userMaxScore = _redisDb.StringGet(userMaxScoreKey);
            if (userMaxScore.IsNullOrEmpty)
            {
                return Enumerable.Empty<PostAddedUserNotification>();
            }
            var maxScore = long.Parse(userMaxScore);
            var notificationIds = ResolveNotificationIds(userNotificationsKey);
            var results = ReadNotifications(notificationIds).ToArray();
            return results;
        }

        //todo common paging logic. 
        private RedisValue[] ResolveNotificationIds(string userNotificationsKey)
        {
            RedisValue[] notificationIds;
            if (_pagingParams.TakesAll)
            {
                notificationIds = _redisDb.SortedSetRangeByScore(userNotificationsKey, order: Order.Descending);
            }
            else
            {
                notificationIds = _redisDb.SortedSetRangeByScore(userNotificationsKey,
                    order: Order.Descending,
                    skip: _pagingParams.Index,
                    take: _pagingParams.Count);
            }
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
