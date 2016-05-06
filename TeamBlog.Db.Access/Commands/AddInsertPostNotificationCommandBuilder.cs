using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections.SortedSet;

namespace TeamBlog.Db.Access
{
    public class AddInsertPostNotificationCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private readonly GetChannelSubscribersQueryBuilder _getChannelSubscribersQueryBuilder;
        private readonly SortedSetWriterBuilder _sortedSetWriterBuilder;

        public AddInsertPostNotificationCommandBuilder(IRedisConnection redisConnection,
            PostAddedUserNotificationBuilder notificationBuilder,
            GetChannelSubscribersQueryBuilder getChannelSubscribersQueryBuilder, SortedSetWriterBuilder sortedSetWriterBuilder)
        {
            _redisConnection = redisConnection;
            _notificationBuilder = notificationBuilder;
            _getChannelSubscribersQueryBuilder = getChannelSubscribersQueryBuilder;
            _sortedSetWriterBuilder = sortedSetWriterBuilder;
        }

        public AddInsertPostNotificationCommand Build(PostAddedDto postAddedDto)
        {
            return new AddInsertPostNotificationCommand(_redisConnection, 
                postAddedDto, _notificationBuilder,
                _getChannelSubscribersQueryBuilder, _sortedSetWriterBuilder);
        }
    }
}