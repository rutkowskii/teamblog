using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Machine.Specifications;
using MongoDB.Driver;
using Ninject;
using TeamBlog.Db.Access;
using TeamBlog.Model;
using TeamBlog.MongoAccess;
using Given = Machine.Specifications.Establish;
using When = Machine.Specifications.Because;
using Then = Machine.Specifications.It;

namespace TeamBlog.Tests
{
    public class ChannelsDbTests
    {
        [Subject("creating channels")]
        class when_create_channel_command_run_is_completed
        {
            When command_run_is_completed = () =>
            {
                var cmd = new CreateChannelCommand(TestKernel.Instance.Get<IMongoAdapter>(), "smieszki-channel");
                cmd.Run();
            };
            Then new_channel_should_exist = () =>
            {
                var actual = TestKernel.Adapter.ChannelCollection.AsQueryable()
                    .Where(ch => ch.Name == "smieszki-channel")
                    .ToList();
                actual.Should().HaveCount(1);
            };
        }
    }
}
