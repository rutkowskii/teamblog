using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using StackExchange.Redis;
using TeamBlog.Db.Access;
using TeamBlog.Db.Access.Queries;

namespace TeamBlog.Tests
{
    [Subject("subscribing and unsubsribing the channels")]
    public class ChannelSubscriptionsTests : TestBase
    {
        private static readonly Guid _channelId = new Guid("75FFB10B-576B-4939-9B08-AB315151311C");
        private static readonly Guid _subscriberId1 = new Guid("E0444695-C9D4-4464-A518-D4A87263E58B");
        private static readonly Guid _subscriberId2 = new Guid("9135BBCE-0A3F-4752-A551-BBE8163256C7");
        private static readonly Guid _subscriberId3 = new Guid("C0A46D47-7123-4AD1-B88D-EBC57C6B72E7");

        private static void InsertSubscribers()
        {
            var commandBuilder = ResolveFromKernel<ChannelSubscribeCommandBuilder>();
            commandBuilder
                .Build(_channelId, _subscriberId1)
                .Run();
            commandBuilder
                .Build(_channelId, _subscriberId2)
                .Run();
            commandBuilder
                .Build(_channelId, _subscriberId3)
                .Run();
        }

        private static void AssertSubscribers(IEnumerable<Guid> expected)
        {
            var actualSubscribers = ResolveFromKernel<GetChannelSubscribersQueryBuilder>()
                        .Build(_channelId).Run();
            var expectedSubscribers = expected.Select(g => (RedisValue)g.ToString());
            actualSubscribers.Should().BeEquivalentTo(expectedSubscribers);
        }

        class when_channel_subscribe_command_is_completed : TestBase
        {
            Because command_run_is_completed = () => { InsertSubscribers(); };

            It channel_subscribers_list_should_contain_them = () =>
            {
                AssertSubscribers(new[] { _subscriberId1, _subscriberId2, _subscriberId3});
            };
        }

        class when_duplicated_subscriber_is_inserted : TestBase
        {
            Establish context = () => { InsertSubscribers(); };

            Because command_run_is_completed = () =>
            {
                ResolveFromKernel<ChannelSubscribeCommandBuilder>()
                    .Build(_channelId, _subscriberId1)
                    .Run();
            };

            It should_not_add_any_duplication = () =>
            {
                AssertSubscribers(new[] { _subscriberId1, _subscriberId2, _subscriberId3 });
            };
        }

        class when_subscribers_get_unsubsribed : TestBase
        {
            Establish context = () => { InsertSubscribers(); };

            Because command_run_is_completed = () =>
            {
                ResolveFromKernel<ChannelUnsubscribeCommandBuilder>()
                    .Build(_channelId, _subscriberId1)
                    .Run();
            };

            It should_delete_the_subscribers = () =>
            {
                AssertSubscribers(new[] { _subscriberId2, _subscriberId3 });
            };
        }
    }
}