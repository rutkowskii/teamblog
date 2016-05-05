using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using MongoDB.Driver;
using Ninject;
using TeamBlog.Db.Access;
using TeamBlog.MongoAccess;
using Given = Machine.Specifications.Establish;
using When = Machine.Specifications.Because;
using Then = Machine.Specifications.It;
using Ploeh.AutoFixture;

namespace TeamBlog.Tests
{
    public class ChannelsDbTests
    {
        [Subject("creating channels")]
        class when_create_channel_command_run_is_completed : TestBase
        {
            When command_run_is_completed = () =>
            {
                var cmdBuilder = ResolveFromKernel<CreateChannelCommandBuilder>();
                var cmd = cmdBuilder.Build("smieszki-channel");
                cmd.Run();
            };
            Then new_channel_should_exist = () =>
            {
                var actual = MongoAdapter.ChannelCollection.AsQueryable()
                    .Where(ch => ch.Name == "smieszki-channel")
                    .ToList();
                actual.Should().HaveCount(1);
            };
        }
    }

    public class TestBase
    {
        protected static T ResolveFromKernel<T>()
        {
            return TestKernel.Instance.Get<T>();
        }

        protected static IMongoAdapter MongoAdapter
        {
            get { return ResolveFromKernel<MongoAdapter>(); }
        }

        protected static IFixture Fixture
        {
            get
            {
                return new Fixture();
            }
        }
    }
}
