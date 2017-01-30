using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public interface IGetChannelsSubscribersQueryBuilder
    {
        GetChannelsSubscribersQuery Build(Guid[] channelIds);
    }
}