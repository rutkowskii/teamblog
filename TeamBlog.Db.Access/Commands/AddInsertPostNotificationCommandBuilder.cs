using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.RedisAccess.Collections.SortedSet;

namespace TeamBlog.Db.Access.Commands
{
    public class AddInsertPostNotificationCommandBuilder
    {
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private readonly GetChannelSubscribersQueryBuilder _getChannelSubscribersQueryBuilder;
        private readonly SortedSetWriterBuilder _sortedSetWriterBuilder;
        private readonly HashWriterBuilder<PostAddedUserNotification> _hashWriterBuilder;


        public AddInsertPostNotificationCommandBuilder(
            PostAddedUserNotificationBuilder notificationBuilder,
            GetChannelSubscribersQueryBuilder getChannelSubscribersQueryBuilder,
            SortedSetWriterBuilder sortedSetWriterBuilder,
            HashWriterBuilder<PostAddedUserNotification> hashWriterBuilder)
        {
            _notificationBuilder = notificationBuilder;
            _getChannelSubscribersQueryBuilder = getChannelSubscribersQueryBuilder;
            _sortedSetWriterBuilder = sortedSetWriterBuilder;
            _hashWriterBuilder = hashWriterBuilder;
        }

        public AddInsertPostNotificationCommand Build(PostAddedDto postAddedDto)
        {
            return new AddInsertPostNotificationCommand(postAddedDto, _notificationBuilder,
                _getChannelSubscribersQueryBuilder, _sortedSetWriterBuilder, _hashWriterBuilder);
        }
    }
}