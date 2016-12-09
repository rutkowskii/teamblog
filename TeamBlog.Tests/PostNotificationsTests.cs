using System;
using System.Collections.Generic;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Model;
using TeamBlog.Utils;
using Xbehave;

namespace TeamBlog.Tests
{
    class PostNotificationsTests : TestBase
    {
        private Guid ChannelId = new Guid("4E5609DC-08A3-46B3-9403-7EAC93CCA65E");
        private Guid Subscriber1 = new Guid("6D49EC5A-B4EF-4859-8C40-C6F05B64825C");
        private Guid Subscriber2 = new Guid("B9657348-D734-4B1E-A596-63A263F028C6");

        public PostNotificationsTests()
        {
            //SubscribeUsersToChannel();
        }

        [Scenario]
        public void QueryForEmptyNotificationsCollection()
        {
            // todo
        }

        //class before_notifications_are_created : notifications_test_base
        //{
        //    Because notifications_do_not_exist = () => { };

        //    It should_not_return_any_notifications = () =>
        //    {
        //        var queryBuilder = K.Resolve<GetUserNotificationsQueryBuilder>();
        //        var actual = queryBuilder.Build(Subscriber1).Run();
        //        actual.Should().HaveCount(0);
        //    };
        //}

        //class when_creating_post_notifications : notifications_test_base
        //{
        //    Because command_run_is_complete = () =>
        //    {
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
        //    };

        //    It should_add_notifications_for_all_the_subscribers = () =>
        //    {
        //        var actualSubscriber1 = GetNotificationsForUser(Subscriber1);
        //        var actualSubscriber2 = GetNotificationsForUser(Subscriber1);
        //        actualSubscriber1.Should().HaveCount(2);
        //        actualSubscriber2.Should().HaveCount(2);
        //    };
        //}

        //class when_getting_notifications_with_paging_for_top_element : notifications_test_base
        //{
        //    Because command_run_is_complete = () =>
        //    {
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
        //    };

        //    It should_get_the_top_element_ordered_by_time_descending = () =>
        //    {
        //        var pagingParams = new PagingParams { Count = 1, Index = 0 };
        //        var actual = GetNotificationsForUser(Subscriber1, pagingParams);
        //        actual.Should().HaveCount(1);
        //        actual.First().Timestamp.Should().Be(new DateTime(2010, 1, 1, 17, 0, 0));
        //    };
        //}

        //class when_getting_notifications_with_paging_for_second_element : notifications_test_base
        //{
        //    Because command_run_is_complete = () =>
        //    {
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
        //    };

        //    It should_get_the_second_element_ordered_by_time_descending = () =>
        //    {
        //        var pagingParams = new PagingParams { Count = 1, Index = 1 };
        //        var actual = GetNotificationsForUser(Subscriber1, pagingParams);
        //        actual.Should().HaveCount(1);
        //        actual.First().Timestamp.Should().Be(new DateTime(2010, 1, 1, 16, 0, 0));

        //    };
        //}

        //class when_getting_many_notifications_with_paging : notifications_test_base
        //{
        //    Because command_run_is_complete = () =>
        //    {
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 16, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 17, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 18, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 19, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 20, 0, 0));
        //        InsertNotificationForChannel1(new DateTime(2010, 1, 1, 21, 0, 0));
        //    };

        //    It should_get_proper_notifications_page_with_notifications_ordered_by_time_desc = () =>
        //    {
        //        var pagingParams = new PagingParams { Count = 3, Index = 2 };
        //        var actual = GetNotificationsForUser(Subscriber1, pagingParams)
        //            .ToArray();
        //        actual.Should().HaveCount(3);
        //        actual[0].Timestamp.Should().Be(new DateTime(2010, 1, 1, 19, 0, 0));
        //        actual[1].Timestamp.Should().Be(new DateTime(2010, 1, 1, 18, 0, 0));
        //        actual[2].Timestamp.Should().Be(new DateTime(2010, 1, 1, 17, 0, 0));
        //    };
        //}

        //private void InsertNotificationForChannel1(DateTime timestamp)
        //{
        //    var postAddedDto = Fixture.Build<PostAddedDto>()
        //        .With(p => p.Timestamp, timestamp)
        //        .With(p => p.ChannelId, ChannelId)
        //        .Create();
        //    var cmdBuilder = K.Resolve<AddInsertPostNotificationCommandBuilder>();
        //    cmdBuilder
        //        .Build(postAddedDto)
        //        .Run();
        //}

//        private void SubscribeUsersToChannel()
//        {
//            var subscribeCmdBuilder = K.Resolve<ChannelSubscribeCommandBuilder>();
//            subscribeCmdBuilder.Build(ChannelId, Subscriber1).Run();
//            subscribeCmdBuilder.Build(ChannelId, Subscriber2).Run();
//        }
//
//        private IEnumerable<PostAddedUserNotification> GetNotificationsForUser(Guid userId)
//        {
//            var queryBuilder = K.Resolve<GetUserNotificationsQueryBuilder>();
//            var actualSubscriber1 = queryBuilder.Build(userId).Run();
//            return actualSubscriber1;
//        }
//
//        private IEnumerable<PostAddedUserNotification> GetNotificationsForUser(Guid userId, PagingParams pagingParams)
//        {
//            var queryBuilder = K.Resolve<GetUserNotificationsQueryBuilder>();
//            var actualSubscriber1 = queryBuilder.Build(pagingParams, userId).Run();
//            return actualSubscriber1;
//        }
    }
}
