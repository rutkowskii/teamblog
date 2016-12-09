using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands.Subscriptions
{
    public interface IChannelUnsubscribeCommandBuilder
    {
        ChannelUnsubscribeCommand Build(ChannelSubscribeParams channelSubscribeParams);
    }
}