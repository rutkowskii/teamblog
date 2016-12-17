using System;

namespace TeamBlog.Db.Access.Queries.Posts
{
    public interface IGetLatestChannelsPostsQueryBuilder
    {
        GetLatestChannelsPostsQuery Build(Guid[] channelIds);
    }
}