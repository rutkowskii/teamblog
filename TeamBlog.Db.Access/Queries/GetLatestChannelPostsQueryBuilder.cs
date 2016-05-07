using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries
{
    public class GetLatestChannelPostsQueryBuilder
    {
        private readonly IMongoAdapter _adapter;

        public GetLatestChannelPostsQueryBuilder(IMongoAdapter adapter)
        {
            _adapter = adapter;
        }

        public GetLatestChannelPostsQuery Build(Guid channelId)
        {
            return new GetLatestChannelPostsQuery(_adapter, channelId);
        }
    }
}