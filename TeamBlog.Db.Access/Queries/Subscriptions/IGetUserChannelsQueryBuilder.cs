using System;

namespace TeamBlog.Db.Access.Queries.Subscriptions
{
    public interface IGetUserChannelsQueryBuilder
    {
        GetUserChannelsQuery Build(Guid userId);
    }
}