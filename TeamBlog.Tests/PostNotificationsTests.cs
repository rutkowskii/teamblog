using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ploeh.AutoFixture;
using TeamBlog.Bus;
using TeamBlog.Controllers;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Jsondtos;
using TeamBlog.NotificationsCreator;
using TeamBlog.Services.Sessions;
using TeamBlog.Utils;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostNotificationsTests : TestBase
    {
        private Guid ChannelId = new Guid("4E5609DC-08A3-46B3-9403-7EAC93CCA65E");
        private Guid Subscriber1 = new Guid("6D49EC5A-B4EF-4859-8C40-C6F05B64825C");
        private Guid Subscriber2 = new Guid("B9657348-D734-4B1E-A596-63A263F028C6");

        public PostNotificationsTests()
        {
            SubscribeUsersToChannel();
        }

        [Scenario]
        public void QueryForEmptyNotificationsCollection()
        {
            "WHEN no notifications have been added, THEN it should return no notifications".x(() =>
            {
                var queryBuilder = K.Resolve<GetUserNotificationsQueryBuilder>();
                var actual = GetNotificationsForUser(Subscriber1);
                actual.Should().HaveCount(0);
            });
        }

        [Scenario]
        public void CreatePostNotifications()
        {
            "WHEN command run is complere".x(() =>
            {
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
            });

            "THEN notifications should be present for all the subscribers".x(() =>
            {
                var actualSubscriber1 = GetNotificationsForUser(Subscriber1);
                var actualSubscriber2 = GetNotificationsForUser(Subscriber1);
                actualSubscriber1.Should().HaveCount(2);
                actualSubscriber2.Should().HaveCount(2);
            });
        }

        [Scenario]
        public void QueryForNotificationsPagingTopElement()
        {
            "GIVEN command run is complete".x(() =>
            {
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
            });

            IEnumerable<UserNotificationJsondto> actual = null;
            "WHEN querying for notifications top element".x(() =>
            {
                var pagingParams = new PagingParams { Count = 1, Index = 0 };
                actual = GetNotificationsForUser(Subscriber1, pagingParams);
            });

            "THEN the top element should be returned ordering by time desc".x(() =>
            {
                actual.Should().HaveCount(1);
                actual.First().Timestamp.Should().Be(new DateTime(2010, 1, 1, 17, 0, 0));
            });
        }

        [Scenario]
        public void QueryForNotificationsPagingSecondElement()
        {
            "GIVEN command run is complete".x(() =>
            {
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
            });

            IEnumerable<UserNotificationJsondto> actual = null;
            "WHEN querying for notifications second element".x(() =>
            {
                var pagingParams = new PagingParams { Count = 1, Index = 1 };
                actual = GetNotificationsForUser(Subscriber1, pagingParams);
            });

            "THEN the second element should be returned ordering by time desc".x(() =>
            {
                actual.Should().HaveCount(1);
                actual.First().Timestamp.Should().Be(new DateTime(2010, 1, 1, 16, 0, 0));
            });
        }

        [Scenario]
        public void QueryForNotificationsManyElements()
        {
            "GIVEN command run is complete".x(() =>
            {
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 18, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 19, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 20, 0, 0));
                InsertNotificationForChannel1(new DateTime(2010, 1, 1, 21, 0, 0));
            });

            UserNotificationJsondto[] actual = null;
            "WHEN querying for many notifications".x(() =>
            {
                var pagingParams = new PagingParams { Count = 3, Index = 2 };
                actual = GetNotificationsForUser(Subscriber1, pagingParams).ToArray();
            });

            "THEN the results should be ordered by time desc".x(() =>
            {
                actual[0].Timestamp.Should().Be(new DateTime(2010, 1, 1, 19, 0, 0));
                actual[1].Timestamp.Should().Be(new DateTime(2010, 1, 1, 18, 0, 0));
                actual[2].Timestamp.Should().Be(new DateTime(2010, 1, 1, 17, 0, 0));
            });
        }

        private void InsertNotificationForChannel1(DateTime timestamp)
        {
            var postAddedEvent = new Fixture().Build<PostCreatedEvent>()
                .With(p => p.Timestamp, timestamp)
                .With(p => p.ChannelIds, ChannelId.AsArray())
                .Create();
            K.Resolve<PostCreatedEventHandler>().Handle(postAddedEvent);
        }

        private void SubscribeUsersToChannel()
        {
            var subscribeCmdBuilder = K.Resolve<IChannelSubscribeCommandBuilder>();
            subscribeCmdBuilder.Build(new ChannelSubscribeParams(ChannelId, Subscriber1)).Run();
            subscribeCmdBuilder.Build(new ChannelSubscribeParams(ChannelId, Subscriber2)).Run();
        }

        private IEnumerable<UserNotificationJsondto> GetNotificationsForUser(Guid userId)
        {
            SetupCurrentUser(userId);
            var notifications = K.Resolve<UserNotificationsController>().GetAll();
            return notifications;
        }

        private void SetupCurrentUser(Guid userId)
        {
            K.OverrideWithMock<ISessionProvider>(m => m.Setup(p => p.UserId).Returns(userId));
        }

        private IEnumerable<UserNotificationJsondto> GetNotificationsForUser(Guid userId, PagingParams pagingParams)
        {
            SetupCurrentUser(userId);
            var notifications = K.Resolve<UserNotificationsController>().Get(pagingParams);
            return notifications;
        }
    }
}
