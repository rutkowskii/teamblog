using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries.Posts
{
    public interface IGetLatestChannelsPostsQueryBuilder
    {
        GetLatestChannelsPostsQuery Build(Guid[] channelIds);
    }
}