using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using TeamBlog.Dtos;
using TeamBlog.Model;
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
            var postChannels = _adapter.ChannelPostCollection.AsQueryable()
                .Where(chp => _channelIds.Contains(chp.ChannelId))
                .OrderByDescending(chp => chp.InsertionTime)
                .ToArray();
            var postChannelsTuples = postChannels
                .ToDictionaryWithDuplicates(chp => chp.PostId, chp => chp.ChannelId);

            var postsByIds = _adapter.PostCollection
                .AsQueryable()
                .Where(p => postChannelsTuples.Keys.Contains(p.Id))
                .ToDictionary(p => p.Id, p => p);

            return postChannelsTuples
                .Select(kvp => postsByIds[kvp.Key])
                .Select(post => new PostDto
                {
                    Author = post.AuthorId,
                    Url = post.Title,
                    Content = post.Content,
                    CreationDate = post.CreationDate,
                    Channels = postChannelsTuples[post.Id].ToArray()
                })
                .ToArray();
        }

        

        //private string ResolveUserName(Post post)
        //{
        //    return _adapter.UserCollection
        //        .AsQueryable()
        //        .Single(u => u.Id == post.AuthorId)
        //        .Name;
        //}
    }

    public static class CollectionExtensions
    {
        public static Dictionary<TKey, IEnumerable<TVal>> ToDictionaryWithDuplicates<TSrc, TKey, TVal>(
            this IEnumerable<TSrc> items,
            Func<TSrc, TKey> getKeys,
            Func<TSrc, TVal> getValues
        )
        {
            var res = new Dictionary<TKey, IEnumerable<TVal>>();
            foreach (var item in items)
            {
                var key = getKeys(item);
                var val= getValues(item);
                if (!res.ContainsKey(key))
                {
                    res.Add(key, new List<TVal>());
                }
                (res[key] as List<TVal>).Add(val);
            }
            return res;
        }
    }


}
