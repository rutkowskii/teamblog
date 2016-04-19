using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using MongoDB.Driver;
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
        class when_create_channel_command_run_is_completed : DbAccessTestBase
        {
            When command_run_is_completed = () =>
            {
                //todo use kernel instead of the fixture. 
                var cmd = Fixture.Create<CreateChannelCommandBuilder>().Build("smieszki-channel");
                cmd.Run();
            };
            Then new_channel_should_exist = () =>
            {
                var actual = TestKernel.MongoAdapter.ChannelCollection.AsQueryable()
                    .Where(ch => ch.Name == "smieszki-channel")
                    .ToList();
                actual.Should().HaveCount(1);
            };
        }
    }

    public class DbAccessTestBase
    {
        static protected IFixture Fixture;

        static DbAccessTestBase()
        {
            Fixture = new Fixture();
            Fixture.Register<IMongoAdapter>(() => TestKernel.MongoAdapter);
        }
    }
}
