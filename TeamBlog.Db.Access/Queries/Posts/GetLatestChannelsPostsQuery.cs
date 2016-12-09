using System;
using System.Linq;
using MongoDB.Driver;
using TeamBlog.Dtos;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries.Posts
{
    public class GetLatestChannelsPostsQuery : IQuery<PostDto>
    {
        private readonly IMongoAdapter _adapter;
        private readonly Guid[] _channelIds;

        public GetLatestChannelsPostsQuery(IMongoAdapter adapter, Guid[] channelIds)
        {
            _adapter = adapter;
            this._channelIds = channelIds;
        }

        public PostDto[] Run()
        {
            var postIds = _adapter.ChannelPostCollection.AsQueryable()
                .Where(chp => _channelIds.Contains(chp.ChannelId))
                .OrderByDescending(chp => chp.InsertionTime)
                .Select(chp => chp.PostId)
                .ToList();
            var postsByIds = _adapter.PostCollection.AsQueryable()
                .Where(p => postIds.Contains(p.Id))
                .ToList()
                .ToDictionary(p => p.Id, p => p);
            return postIds
                .Select(id => postsByIds[id])
                .Select(post => new PostDto
                {
                    Author = "dummy",
                    Url = post.Title,
                    Description = post.Content,
                    CreationDate = post.CreationDate
                })
                .ToArray();
        }
    }
}
