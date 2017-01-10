using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using TeamBlog.Bl;
using TeamBlog.Bus;
using TeamBlog.Controllers;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Jsondtos;
using TeamBlog.MongoAccess;
using TeamBlog.Utils;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostsTest : TestBase
    {
        private IEnumerable<PostJsondto> _actualPosts;
        private Mock<IBus> _busMock;
        private IMessage _messageSent;
        private const string ChannelName = "smieszki-channel";
        private readonly DateTime _mockedNow = new DateTime(2015, 05, 23, 3, 23, 11);
        private readonly string _userName = "Jan Kowalski";

        [Scenario]
        public void AddThenRetrievePosts()
        {
            SetupDependencies();

            "GIVEN the channel is created".x(() => { InsertChannel(); });

            "and GIVEN the user is subscribed to the channel".x(() =>
            {
                K.Resolve<ChannelSubscriptionsController>().Subscribe(ChannelId);
            });

            "and GIVEN the posts are inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa");
                InsertPost(title: "xyz", content: "zzz");
            });

            "WHEN querying for the user feed".x(() =>
            {
                _actualPosts = K.Resolve<PostsController>()
                    .GetFeedPosts();
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

        }

        [Scenario]
        public void NotifyBusWhenCreatingAPost()
        {
            SetupDependencies();

            "GIVEN the channel is created".x(() => { InsertChannel(); });

            "and GIVEN the user is subscribed to the channel".x(() =>
            {
                K.Resolve<ChannelSubscriptionsController>().Subscribe(ChannelId);
            });

            "and GIVEN the post is inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa");
            });

            "THEN a notification should be created".x(() =>
            {
                _messageSent.Should().NotBeNull();
                _messageSent.Should().BeOfType<PostCreatedEvent>();
            });
        }

        private void SetupDependencies()
        {
            _messageSent = null;

            _busMock = new Mock<IBus>();
            _busMock.Setup(b => b.Publish(It.IsAny<IMessage>())).Callback<IMessage>(msg => _messageSent = msg);

            InsertUserAndSetupSessionProvider();

            K.Override<IBus>(_busMock.Object);

            K.OverrideWithMock<IDateTimeProvider>(m => m.Setup(p => p.UtcNow).Returns(() => _mockedNow));
        }

        private void InsertUserAndSetupSessionProvider()
        {
            K.Resolve<IAddUserCommandBuilder>().Build(_userName).Run();
            var userId = K.Resolve<IMongoAdapter>().UserCollection.AsQueryable().First().Id;
            K.OverrideWithMock<ISessionProvider>(m => m.Setup(p => p.UserId).Returns(() => userId));
        }

        private void InsertChannel()
        {
            K.Resolve<ChannelsController>().AddNewChannel(new NewChannelJsondto { Name = ChannelName});
        }

        private Guid ChannelId => 
            K.Resolve<ChannelsController>().GetAll()
                .Where(ch => ch.Name == ChannelName)
                .Select(ch => ch.Id)
                .First();

        private void InsertPost(string title, string content)
        {
            var postsController = K.Resolve<PostsController>();
            postsController.AddNewPost(new NewPostJsondto
            {
                Channels = ChannelId.AsArray(),
                Content = content,
                Title = title
            });
        }
    }
}