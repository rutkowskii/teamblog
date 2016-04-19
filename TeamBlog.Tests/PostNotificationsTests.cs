using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using Ninject;
using Ploeh.AutoFixture;
using TeamBlog.Db.Access;
using TeamBlog.Dtos;

namespace TeamBlog.Tests
{
    [Subject("creating notifications after post insertion")]
    class PostNotificationsTests
    {

        private static Guid ChannelId = new Guid("4E5609DC-08A3-46B3-9403-7EAC93CCA65E");
        private static Guid Subscriber1 = new Guid("6D49EC5A-B4EF-4859-8C40-C6F05B64825C");
        private static Guid Subscriber2 = new Guid("B9657348-D734-4B1E-A596-63A263F028C6");
        private static IFixture Fixture = new Fixture();

        //todo. 
        //todo insert notifications
        // then get top 10, next 10, etc -> effective paging!
        class when_creating_post_notifications : DbAccessTestBase
        {
            Establish context = () =>
            {

            };


            Because command_run_is_complete = () =>
            {
                var postAddedDto = Fixture.Build<PostAddedDto>()
                    .With(p => p.Timestamp, new DateTime(2010, 1, 1))
                    .With(p => p.ChannelId, ChannelId)
                    .Create();
                TestKernel.Instance
                    .Get<AddInsertPostNotificationCommandBuilder>()
                    .Build(postAddedDto)
                    .Run();
            };

            It should_add_notifications_for_all_the_subscribers = () =>
            {
                //todo notf queries. 
            };
        }
    }
}
