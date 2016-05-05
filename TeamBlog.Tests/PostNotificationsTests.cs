using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Machine.Specifications;
using Ninject;
using Ploeh.AutoFixture;
using TeamBlog.Db.Access;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;

namespace TeamBlog.Tests
{
    [Subject("creating notifications after post insertion")]
    class PostNotificationsTests : TestBase
    {

        private static Guid ChannelId = new Guid("4E5609DC-08A3-46B3-9403-7EAC93CCA65E");
        private static Guid Subscriber1 = new Guid("6D49EC5A-B4EF-4859-8C40-C6F05B64825C");
        private static Guid Subscriber2 = new Guid("B9657348-D734-4B1E-A596-63A263F028C6");

        class when_creating_post_notifications
        {
            Establish context = () =>
            {
                SubscribeUsersToChannel();
            };

            private static void SubscribeUsersToChannel()
            {
                var subscribeCmdBuilder = ResolveFromKernel<ChannelSubscribeCommandBuilder>();
                subscribeCmdBuilder.Build(ChannelId, Subscriber1).Run();
                subscribeCmdBuilder.Build(ChannelId, Subscriber2).Run();
            }

            Because command_run_is_complete = () =>
            {
                var postAddedDto = Fixture.Build<PostAddedDto>()
                    .With(p => p.Timestamp, new DateTime(2010, 1, 1))
                    .With(p => p.ChannelId, ChannelId)
                    .Create();
                ResolveFromKernel<AddInsertPostNotificationCommandBuilder>()
                    .Build(postAddedDto)
                    .Run();
            };

            It should_add_notifications_for_all_the_subscribers = () =>
            {
                var actual = ResolveFromKernel<GetUserNotificationsQueryBuilder>().Build(Subscriber1).Run();
                actual.Should().HaveCount(2);
                //todo looks like redis flush does not work here :(
                //todo tests for paging 
            };
        }
    }
}
