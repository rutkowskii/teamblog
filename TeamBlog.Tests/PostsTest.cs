using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using Ploeh.SemanticComparison.Fluent;
using TeamBlog.Bus;
using TeamBlog.Controllers;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Jsondtos;
using TeamBlog.Model;
using TeamBlog.MongoAccess;
using TeamBlog.Services.Sessions;
using TeamBlog.Utils;
using TeamBlog.Utils.Datetime;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostsTest : TestBase
    {
        private IEnumerable<PostJsondto> _actualPosts;
        private Mock<IBus> _busMock;
        private IMessage _messageSent;
        private const string ChannelNameA = "smieszki-channel";
        private const string ChannelNameB = "serious-channel";
        private readonly DateTime _mockedNow = new DateTime(2015, 05, 23, 3, 23, 11);
        private readonly string _userName = "Jan Kowalski";
        private Guid _currentUserId;

        [Scenario]
        public void PostsRetrieval()
        {
            SetupDependencies();

            "GIVEN the channel is created and user is subscribed to the channel".x(() => {
                InsertChannel(ChannelNameA);
                SubscribeToChannelA();
            });

            "and GIVEN the posts are inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa", channelIds: new[] { ResolveChannelId(ChannelNameA) });
                InsertPost(title: "xyz", content: "zzz", channelIds: new[] { ResolveChannelId(ChannelNameA) });
            });

            "WHEN querying for the user feed".x(() =>
            {
                RetrieveUserFeed();
            });

            "THEN posts should be returned in proper order".x(() =>
            {
                _actualPosts
                    .Select(p => p.Content)
                    .ShouldBeEquivalentTo(new[] {"zzz", "aaa"});
            });

            "and THEN posts should have the proper timestamps set".x(() =>
            {
                _actualPosts
                    .Select(p => p.Timestamp)
                    .ShouldBeEquivalentTo(new [] {_mockedNow.ToWebFormat(), _mockedNow.ToWebFormat()});
            });

            "and THEN posts should have the proper user set".x(() =>
            {
                _actualPosts
                    .Select(p => p.AddedBy)
                    .ShouldAllBeEquivalentTo(new[] {_userName, _userName});
            });

            "and THEN posts should have proper channels label set".x(() =>
            {
                _actualPosts
                    .Select(p => p.Channels)
                    .ShouldAllBeEquivalentTo(new[] { new[] { ChannelNameA }, new[] { ChannelNameA } });
            });
        }

        [Scenario]
        public void ShowOnlyChannelsUserIsSubscribedTo()
        {
            SetupDependencies();

            "GIVEN the channel is created and user is subscribed to the channel".x(() => {
                InsertChannel(ChannelNameA);
                InsertChannel(ChannelNameB);
                SubscribeToChannelA();
            });

            "and GIVEN the post is inserted to 2 channels".x(() =>
            {
                InsertPost(title: "abc", content: "aaa", 
                    channelIds: new[] { ResolveChannelId(ChannelNameA) , ResolveChannelId(ChannelNameB) });
            });

            "WHEN querying for the user feed".x(() =>
            {
                RetrieveUserFeed();
            });

            "THEN post should only be marked with a channel user subscribed to".x(() =>
            {
                _actualPosts
                    .SelectMany(p => p.Channels)
                    .ShouldBeEquivalentTo(new[] { ChannelNameA });
            });
        }

        [Scenario]
        public void ShowOnlyPostsFromUserChannels()
        {
            SetupDependencies();

            "GIVEN the channel is created and user is subscribed to the channel".x(() => {
                InsertChannel(ChannelNameA);
                InsertChannel(ChannelNameB);
                SubscribeToChannelA();
            });

            "and GIVEN the post is inserted to 2 channels".x(() =>
            {
                InsertPost(title: "abc", content: "aaa",
                    channelIds: new[] { ResolveChannelId(ChannelNameA) });
                InsertPost(title: "zzz", content: "zzz",
                    channelIds: new[] {ResolveChannelId(ChannelNameB)});
            });

            "WHEN querying for the user feed".x(() =>
            {
                RetrieveUserFeed();
            });

            "THEN feed should only contain posts from ".x(() =>
            {
                _actualPosts
                    .Select(p => p.Content)
                    .ShouldBeEquivalentTo(new[] { "aaa" });
            });
        }

        [Scenario]
        public void NotifyBusWhenCreatingAPost()
        {
            SetupDependencies();

            "GIVEN the channel is created and user is subscribed to the channel".x(() => {
                InsertChannel(ChannelNameA);
                SubscribeToChannelA();
            });

            "and GIVEN the post is inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa", channelIds: new[] {ResolveChannelId(ChannelNameA)});
            });

            "THEN a proper notification should be created".x(() =>
            {
                _messageSent.Should().NotBeNull();
                _messageSent.Should().BeOfType<PostCreatedEvent>();
                var expectedEvent = new PostCreatedEvent
                {
                    ChannelIds = ResolveChannelId(ChannelNameA).AsArray(),
                    AuthorId = _currentUserId,
                    Timestamp = _mockedNow
                };
                _messageSent.As<PostCreatedEvent>()
                    .AsSource().OfLikeness<PostCreatedEvent>()
                    .With(x => x.ChannelIds).EqualsWhen((x,y) => x.ChannelIds.SequenceEqual(y.ChannelIds))
                    .ShouldEqual(expectedEvent);
            });
        }

        private void RetrieveUserFeed()
        {
            _actualPosts = K.Resolve<PostsController>().GetFeedPosts();
        }

        private void SubscribeToChannelA()
        {
            K.Resolve<ChannelSubscriptionsController>().Subscribe(ResolveChannelId(ChannelNameA));
        }

        private void SetupDependencies()
        {
            _messageSent = null;

            _busMock = new Mock<IBus>();
            _busMock.Setup(b => b.Publish(It.IsAny<IMessage>())).Callback<IMessage>(msg => _messageSent = msg);

            InsertUserAndSetupSessionProvider();

            K.Override(_busMock.Object);

            K.OverrideWithMock<IDateTimeProvider>(m => m.Setup(p => p.UtcNow).Returns(() => _mockedNow));
        }

        private void InsertUserAndSetupSessionProvider()
        {
            K.Resolve<IAddUserCommandBuilder>().Build(_userName).Run();
            _currentUserId = K.Resolve<IMongoAdapter>().UserCollection.AsQueryable().First().Id;
            K.OverrideWithMock<ISessionProvider>(m => m.Setup(p => p.UserId).Returns(() => _currentUserId));
        }

        private void InsertChannel(string channelName)
        {
            K.Resolve<ChannelsController>().AddNewChannel(new NewChannelJsondto { Name = channelName});
        }

        private Guid ResolveChannelId(string channelName)
        {
            return K.Resolve<ChannelsController>().GetAll()
                .Where(ch => ch.Name == channelName)
                .Select(ch => ch.Id)
                .First();
        }

        private void InsertPost(string title, string content, IEnumerable<Guid> channelIds)
        {
            var postsController = K.Resolve<PostsController>();
            postsController.AddNewPost(new NewPostJsondto
            {
                Channels = channelIds.ToArray(),
                Content = content,
                Title = title
            });
        }
    }
}