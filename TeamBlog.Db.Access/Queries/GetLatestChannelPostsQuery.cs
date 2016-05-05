using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TeamBlog.Dtos;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access
{
    public class GetLatestChannelPostsQuery : IQuery<PostDto>
    {
        private readonly IMongoAdapter _adapter;
        private readonly Guid _channelId;

        public GetLatestChannelPostsQuery(IMongoAdapter adapter, Guid channelId)
        {
            _adapter = adapter;
            _channelId = channelId;
        }

        public IEnumerable<PostDto> Run()
        {
            var postIds = _adapter.ChannelPostCollection.AsQueryable()
                .Where(chp => chp.ChannelId == _channelId)
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
                    Url = post.Url,
                    Description = post.Description,
                    CreationDate = post.CreationDate
                });
        }
    }
}
