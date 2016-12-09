using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands.Subscriptions
{
    public class ChannelUnsubscribeCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private readonly Guid _subscriberId;

        public ChannelUnsubscribeCommand(IRedisConnection redisConnection, ChannelSubscribeParams channelSubscribeParams)
        {
            _redisConnection = redisConnection;
            _channelId = channelSubscribeParams.ChannelId;
            _subscriberId = channelSubscribeParams.SubscriberId;
        }

        public void Run()
        {
            var db = _redisConnection.AccessRedis();
            db.SetRemove(RedisDbObjects.ChannelSubscribersKey(_channelId), _subscriberId.ToString());
        }
    }
}