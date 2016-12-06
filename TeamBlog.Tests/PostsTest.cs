using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Driver;
using TeamBlog.Bl;
using TeamBlog.Controllers;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Jsondtos;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostsTest : TestBase
    {
        private IEnumerable<PostJsondto> actualPosts;
        private const string ChannelName = "smieszki-channel";

        [Scenario]
        public void AddThenRetrievePosts()
        {
            //todo controllers in these 2 cases. 
            "GIVEN the channel is created".x(() => { InsertChannel(); });

            "and GIVEN the user is subscribed to the channel".x(
              () => { K.Resolve<IUserFactory>().GetCurrentUser().SubscribeToChannel(ChannelId); });

            "and GIVEN the posts are inserted".x(() =>
            {
                InsertPost(title: "abc", content: "aaa");
                InsertPost(title: "xyz", content: "zzz");
            });

            "WHEN querying for the user feed".x(() =>
            {
                actualPosts = K.Resolve<PostsController>()
                    .GetFeedPosts();
            });

            "THEN proper posts should be returned".x(() =>
            {
                actualPosts
                    .Select(p => p.Content)
                    .ShouldBeEquivalentTo(new[] {"zzz", "aaa"});
            });
        }

        private void InsertChannel()
        {
            this.K.Resolve<CreateChannelCommandBuilder>().Build(ChannelName).Run();
        }

        private Guid ChannelId => 
            this.K.MongoAdapter.ChannelCollection.AsQueryable()
                .Where(ch => ch.Name == ChannelName)
                .Select(ch => ch.Id)
                .First();

        private void InsertPost(string title, string content)
        {
            var postsController = K.Resolve<PostsController>();
            postsController.AddNewPost(new NewPostJsondto
            {
                Channels = new[] {ChannelId}, //todo object extensions 
                Content = content,
                Title = title
            });
        }
    }
}