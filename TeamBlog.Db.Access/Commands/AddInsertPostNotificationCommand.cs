using System.Globalization;
using StackExchange.Redis;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections.SortedSet;

namespace TeamBlog.Db.Access
{
    public class AddInsertPostNotificationCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly PostAddedDto _postAddedDto;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private readonly GetChannelSubscribersQueryBuilder _getChannelSubscribersQueryBuilder;
        private IDatabase _redisDb;
        private readonly SortedSetWriterBuilder _sortedSetWriterBuilder;

        public AddInsertPostNotificationCommand(IRedisConnection redisConnection, PostAddedDto postAddedDto,
            PostAddedUserNotificationBuilder notificationBuilder,
            GetChannelSubscribersQueryBuilder getChannelSubscribersQueryBuilder, 
            SortedSetWriterBuilder sortedSetWriterBuilder)
        {
            _redisConnection = redisConnection;
            _postAddedDto = postAddedDto;
            _notificationBuilder = notificationBuilder;
            _getChannelSubscribersQueryBuilder = getChannelSubscribersQueryBuilder;
            _sortedSetWriterBuilder = sortedSetWriterBuilder;
        }

        public void Run()
        {
            _redisDb = _redisConnection.AccessRedis();
            var subscribers = _getChannelSubscribersQueryBuilder.Build(_postAddedDto.ChannelId).Run();

            var dbNotification = _notificationBuilder.Build(_postAddedDto);
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
            var nextElementKey = RedisDbObjects.UserNotificationsNextElementKey(userId);
            var sortedSetKey = RedisDbObjects.UserNotificationsKey(userId);
            _sortedSetWriterBuilder.Build(nextElementKey, sortedSetKey).Append(newNotification.Id.ToString());
        }

        private void InsertNotification(PostAddedUserNotification dbNotification)
        {
            //todo reflction based code creating hash entries from a dto. 
            var hashEntries = new[]
            {
                new HashEntry("Timestamp", dbNotification.Timestamp.ToString(DateTimeFormatInfo.InvariantInfo)),
                new HashEntry("Content", dbNotification.Content)
            };
            _redisDb.HashSet(RedisDbObjects.NotificationsKey(dbNotification.Id), hashEntries);
        }
    }
}