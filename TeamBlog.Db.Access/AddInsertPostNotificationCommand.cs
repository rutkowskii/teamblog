using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access
{
    public class AddInsertPostNotificationCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly PostAddedBusMsg _busMsg;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private IDatabase _redisDb;

        public AddInsertPostNotificationCommand(IRedisConnection redisConnection, PostAddedBusMsg busMsg,
            PostAddedUserNotificationBuilder notificationBuilder)
        {
            _redisConnection = redisConnection;
            _busMsg = busMsg;
            _notificationBuilder = notificationBuilder;
        }

        public void Run()
        {
            _redisDb = _redisConnection.AccessRedis();
            var subscribers = _redisDb
                .SetMembers(RedisDbObjects.ChannelSubscribersKey(_busMsg.ChannelId));

            var dbNotification = _notificationBuilder.Build(_busMsg);
            InsertNotification(dbNotification);
            
            foreach (var subscriber in subscribers)
            {
                AddNotificationForUser(subscriber, dbNotification);
            }

            //todo transaction?
        }

        private void AddNotificationForUser(RedisValue subscriber, PostAddedUserNotification newNotification)
        {
            var userId = (string) subscriber;
            var newNotificationScore =
                _redisDb.StringIncrement(RedisDbObjects.UserNotificationsNextElementKey(userId), 1L);

            _redisDb.SortedSetAdd(RedisDbObjects.UserNotificationsKey(userId), newNotification.Id.ToString(),
                newNotificationScore);
        }

        private void InsertNotification(PostAddedUserNotification dbNotification)
        {
//todo reflction based code creating hash entries from a dto. 
            var hashEntries = new[]
            {
                new HashEntry("Timestamp", dbNotification.Timestamp.ToShortTimeString()),
                new HashEntry("Content", dbNotification.Content)
            };
            _redisDb.HashSet(RedisDbObjects.NotificationsKey(dbNotification.Id), hashEntries);
        }
    }
}