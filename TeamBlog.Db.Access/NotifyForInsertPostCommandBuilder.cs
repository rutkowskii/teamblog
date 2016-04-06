using TeamBlog.Model;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access
{
    public class NotifyForInsertPostCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;

        public NotifyForInsertPostCommandBuilder(IRedisConnection redisConnection, PostAddedUserNotificationBuilder notificationBuilder)
        {
            _redisConnection = redisConnection;
            _notificationBuilder = notificationBuilder;
        }

        public AddInsertPostNotificationCommand Build(PostAddedBusMsg busMsg)
        {
            return new AddInsertPostNotificationCommand(_redisConnection, busMsg, _notificationBuilder);
        }
    }
}