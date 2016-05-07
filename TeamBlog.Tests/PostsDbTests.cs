using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using MongoDB.Driver;
using Ninject;
using Ploeh.AutoFixture;
using TeamBlog.Db.Access;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Dtos;
using TeamBlog.MongoAccess;
using Given = Machine.Specifications.Establish;
using When = Machine.Specifications.Because;
using Then = Machine.Specifications.It;

namespace TeamBlog.Tests
{
    public class PostsDbTests
    {
        [Subject("querying posts")]
        class when_post_query_is_completed : TestBase
        {
            Given posts_inserted = () =>
            {
                ResolveFromKernel<CreateChannelCommandBuilder>().Build("smieszki-channel").Run();
                InsertPost("abc", "aaa");
                InsertPost("xyz", "zzz");
            };

            When query_run_is_completed = () =>
            {
                Actual = ResolveFromKernel<GetLatestChannelPostsQueryBuilder>().Build(getChannelId()).Run();
            };
            Then proper_posts_should_be_returned = () =>
            {
                Actual.Select(p => p.Description)
                    .ShouldBeEquivalentTo(new[] {"zzz", "aaa"});
            };

            private static IEnumerable<PostDto> Actual; 

            static void InsertPost(string url, string description)
            {
                ResolveFromKernel<InsertPostCommandBuilder>()
                    .Build(getChannelId(), url, description, Guid.Empty)
                    .Run();
            }

            static Guid getChannelId()
            {
                return TestKernel.MongoAdapter.ChannelCollection.AsQueryable()
                  .Where(ch => ch.Name == "smieszki-channel")
                  .Select(ch => ch.Id)
                  .First();
            }
        }
    }
}