using System;

namespace TeamBlog.Db.Access.Commands.Subscriptions
{
    public class ChannelSubscribeParams
    {
        public ChannelSubscribeParams(Guid channelId, Guid subscriberId)
        {
            ChannelId = channelId;
            SubscriberId = subscriberId;
        }

        public Guid ChannelId { get; }

        public Guid SubscriberId { get; }
    }
}