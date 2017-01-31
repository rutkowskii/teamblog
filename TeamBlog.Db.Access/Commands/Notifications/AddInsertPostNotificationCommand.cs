using System;
using System.Collections.Generic;
using System.Linq;
using TeamBlog.Db.Access.Queries.Subscriptions;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.RedisAccess.Collections.SortedSet;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access.Commands.Notifications
{
    public class AddInsertPostNotificationCommand
    {
        private readonly AddInsertPostNotificationCommandParams _params;
        private readonly IGetChannelsSubscribersQueryBuilder _getChannelsSubscribersQueryBuilder;
        private readonly SortedSetWriterBuilder _sortedSetWriterBuilder;
        private readonly HashWriterBuilder<PostAddedUserNotification> _hashWriterBuilder;

        public AddInsertPostNotificationCommand(AddInsertPostNotificationCommandParams @params,
            IGetChannelsSubscribersQueryBuilder getChannelsSubscribersQueryBuilder,
            SortedSetWriterBuilder sortedSetWriterBuilder, HashWriterBuilder<PostAddedUserNotification> hashWriterBuilder)
        {
            _params = @params;
            _getChannelsSubscribersQueryBuilder = getChannelsSubscribersQueryBuilder;
            _sortedSetWriterBuilder = sortedSetWriterBuilder;
            _hashWriterBuilder = hashWriterBuilder;
        }

        // returns subscribers ids. 
        public Guid[] Run()
        {
            var subscribers = ResolveSubscribers();

            var dbNotification = InsertNotificationToDb();

            subscribers.ForEach(subscriber => AddNotificationForUser(subscriber, dbNotification));

            return subscribers;
        }

        private Guid[] ResolveSubscribers()
        {
            var subscribers = _getChannelsSubscribersQueryBuilder
                .Build(_params.ChannelIds)
                .Run()
                .Except(_params.AuthorId.AsArray())
                .ToArray();
            return subscribers;
        }

        private PostAddedUserNotification InsertNotificationToDb()
        {
            var dbNotification = new PostAddedUserNotification
            {
                Content = "New post in your channel",
                Id = Guid.NewGuid(),
                Timestamp = _params.Timestamp
            };
            InsertNotification(dbNotification);
            return dbNotification;
        }

        private void AddNotificationForUser(Guid subscriber, PostAddedUserNotification newNotification)
        {
            var userId = subscriber.ToString();
            var nextElementKey = RedisModel.UserNotifications.NextElementKeyFor(userId);
            var sortedSetKey = RedisModel.UserNotifications.KeyFor(userId);
            _sortedSetWriterBuilder.Build(nextElementKey, sortedSetKey).Append(newNotification.Id.ToString());
        }

        private void InsertNotification(PostAddedUserNotification dbNotification)
        {
            var hashIdentifier = RedisModel.Notifications.KeyFor(dbNotification.Id);
            var hashWriter = _hashWriterBuilder.Build(hashIdentifier);
            hashWriter.Write(dbNotification);
        }
    }
}