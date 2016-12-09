using System;
using StackExchange.Redis;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Subscriptions;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.RedisAccess.Collections.SortedSet;

namespace TeamBlog.Db.Access.Commands
{
    public class AddInsertPostNotificationCommand
    {
        private readonly PostAddedDto _postAddedDto;
        private readonly PostAddedUserNotificationBuilder _notificationBuilder;
        private readonly IGetChannelSubscribersQueryBuilder _getChannelSubscribersQueryBuilder;
        private readonly SortedSetWriterBuilder _sortedSetWriterBuilder;
        private readonly HashWriterBuilder<PostAddedUserNotification> _hashWriterBuilder;

        public AddInsertPostNotificationCommand(PostAddedDto postAddedDto,
            PostAddedUserNotificationBuilder notificationBuilder,
            IGetChannelSubscribersQueryBuilder getChannelSubscribersQueryBuilder,
            SortedSetWriterBuilder sortedSetWriterBuilder, HashWriterBuilder<PostAddedUserNotification> hashWriterBuilder)
        {
            _postAddedDto = postAddedDto;
            _notificationBuilder = notificationBuilder;
            _getChannelSubscribersQueryBuilder = getChannelSubscribersQueryBuilder;
            _sortedSetWriterBuilder = sortedSetWriterBuilder;
            _hashWriterBuilder = hashWriterBuilder;
        }

        public void Run()
        {
            var subscribers = _getChannelSubscribersQueryBuilder.Build(_postAddedDto.ChannelId).Run();
            var dbNotification = _notificationBuilder.Build(_postAddedDto);
            InsertNotification(dbNotification);

            foreach (var subscriber in subscribers)
            {
                AddNotificationForUser(subscriber, dbNotification);
            }
        }

        private void AddNotificationForUser(Guid subscriber, PostAddedUserNotification newNotification)
        {
            var userId = subscriber.ToString();
            var nextElementKey = RedisDbObjects.UserNotificationsNextElementKey(userId);
            var sortedSetKey = RedisDbObjects.UserNotificationsKey(userId);
            _sortedSetWriterBuilder.Build(nextElementKey, sortedSetKey).Append(newNotification.Id.ToString());
        }

        private void InsertNotification(PostAddedUserNotification dbNotification)
        {
            var hashIdentifier = RedisDbObjects.NotificationsKey(dbNotification.Id);
            var hashWriter = _hashWriterBuilder.Build(hashIdentifier);
            hashWriter.Write(dbNotification);
        }
    }
}