using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access
{
    public class AddInsertPostNotificationCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private readonly GetChannelSubscribersQueryBuilder _getChannelSubscribersQueryBuilder;

        public AddInsertPostNotificationCommandBuilder(IRedisConnection redisConnection,
            PostAddedUserNotificationBuilder notificationBuilder,
            GetChannelSubscribersQueryBuilder getChannelSubscribersQueryBuilder)
        {
            _redisConnection = redisConnection;
            _notificationBuilder = notificationBuilder;
            _getChannelSubscribersQueryBuilder = getChannelSubscribersQueryBuilder;
        }

        public AddInsertPostNotificationCommand Build(PostAddedDto postAddedDto)
        {
            return new AddInsertPostNotificationCommand(_redisConnection, 
                postAddedDto, _notificationBuilder,
                _getChannelSubscribersQueryBuilder);
        }
    }
}