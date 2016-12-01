using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using StackExchange.Redis;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Subscriptions;
using Xbehave;

namespace TeamBlog.Tests
{
    public class ChannelSubscriptionsTest : TestBase //todo should be replaced w/ controllers. 
    {
        private readonly Guid _channelId = new Guid("75FFB10B-576B-4939-9B08-AB315151311C");
        private readonly Guid _subscriberId1 = new Guid("E0444695-C9D4-4464-A518-D4A87263E58B");
        private readonly Guid _subscriberId2 = new Guid("9135BBCE-0A3F-4752-A551-BBE8163256C7");
        private readonly Guid _subscriberId3 = new Guid("C0A46D47-7123-4AD1-B88D-EBC57C6B72E7");

        [Scenario]
        public void ChannelSubscription()
        {
            "GIVEN users subscribed to the channel".x(() =>
            {
                InsertSubscribers();
            });

            "WHEN querying for channel subscribers, THEN proper subsribers should be taken".x(() =>
            {
                AssertSubscribers(new[] {_subscriberId1, _subscriberId2, _subscriberId3});
            });
        }

        [Scenario]
        public void DuplicateChannelSubscription()
        {
            "GIVEN users subscribed to the channel".x(() =>
            {
                InsertSubscribers();
            });

            "and GIVEN duplicate subscription inserted".x(() =>
            {
                K.Resolve<ChannelSubscribeCommandBuilder>()
                   .Build(_channelId, _subscriberId1)
                   .Run();
            });

            "WHEN querying for channel subscribers, THEN subsribers set should still be unique".x(() =>
            {
                AssertSubscribers(new[] { _subscriberId1, _subscriberId2, _subscriberId3 });
            });
        }

        [Scenario]
        public void UnsubscribingFromChannel()
        {
            "GIVEN users subscribed to the channel".x(() =>
            {
                InsertSubscribers();
            });

            "WHEN user unsubsribes".x(() =>
            {
                K.Resolve<ChannelUnsubscribeCommandBuilder>()
                    .Build(_channelId, _subscriberId1)
                    .Run();
            });

            "THEN subsribers set should be updated".x(() =>
            {
                AssertSubscribers(new[] { _subscriberId2, _subscriberId3 });
            });
        }

        private void InsertSubscribers()
        {
            foreach (var subscriber in 
                new[] {_subscriberId1, _subscriberId2, _subscriberId3})
            {
                SubscribeToChannel(subscriber);
            }
        }

        private void SubscribeToChannel(Guid subscriber)
        {
            var commandBuilder = K.Resolve<ChannelSubscribeCommandBuilder>();
            commandBuilder
                .Build(_channelId, subscriber)
                .Run();
        }

        private void AssertSubscribers(
            IEnumerable<Guid> expected)
        {
            var actualSubscribers = K
                .Resolve<GetChannelSubscribersQueryBuilder>()
                .Build(_channelId)
                .Run();
            var expectedSubscribers = expected.Select(g => (RedisValue) g.ToString());
            actualSubscribers.Should().BeEquivalentTo(expectedSubscribers);
        }
    }
}