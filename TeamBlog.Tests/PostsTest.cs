using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MongoDB.Driver;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using Xbehave;

namespace TeamBlog.Tests
{
    public class PostsTest : TestBase
    {
        private IEnumerable<PostDto> actualPosts;
        private const string ChannelName = "smieszki-channel";

        [Scenario]
        public void AddThenRetrievePosts()
        {
            "GIVEN the channel is created".x(
                () => { K.Resolve<CreateChannelCommandBuilder>().Build(ChannelName).Run(); });

            "and GIVEN the posts are inserted".x(() =>
            {
                InsertPost("abc", "aaa");
                InsertPost("xyz", "zzz");
            });

            "WHEN querying for the channel content".x(() =>
            {
                actualPosts = K.Resolve<GetLatestChannelsPostsQueryBuilder>()
                    .Build(new[]{ChannelId})
                    .Run();
            });

            "THEN proper posts should be returned".x(() =>
            {
                actualPosts.Select(p => p.Description)
                    .ShouldBeEquivalentTo(new[] {"zzz", "aaa"});
            });
        }

        private Guid ChannelId => 
            this.K.MongoAdapter.ChannelCollection.AsQueryable()
                .Where(ch => ch.Name == ChannelName)
                .Select(ch => ch.Id)
                .First();

        private void InsertPost(
            string url,
            string content)
        {
            K.Resolve<InsertNewPostCommandBuilder>()
                .Build(new[] {ChannelId}, url, content, Guid.Empty) //todo user id. 
                .Run();
        }
    }
}