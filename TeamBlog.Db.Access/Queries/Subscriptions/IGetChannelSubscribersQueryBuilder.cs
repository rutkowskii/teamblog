using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public interface IGetChannelSubscribersQueryBuilder
    {
        GetChannelSubscribersQuery Build(Guid channelId);
    }
}