using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries
{
    public class GetLatestChannelsPostsQueryBuilder
    {
        private readonly IMongoAdapter _adapter; //todo redis?

        public GetLatestChannelsPostsQueryBuilder(IMongoAdapter adapter)
        {
            _adapter = adapter;
        }

        public GetLatestChannelsPostsQuery Build(Guid[] channelIds) 
        {
            return new GetLatestChannelsPostsQuery(_adapter, channelIds);
        }
    }


    //todo replace it with smart factories !!!!!!!!!!1
}