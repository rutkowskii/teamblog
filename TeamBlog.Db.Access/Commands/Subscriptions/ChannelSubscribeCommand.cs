using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class ChannelSubscribeCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private readonly Guid _subscriberId;

        public ChannelSubscribeCommand(IRedisConnection redisConnection, Guid channelId, Guid subscriberId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId; 
            _subscriberId = subscriberId;
        }

        public void Run()
        {
            var db = _redisConnection.AccessRedis();
            db.SetAdd(RedisDbObjects.ChannelSubscribersKey(_channelId), _subscriberId.ToString());
            db.SetAdd(RedisDbObjects.UserChannelsKey(_subscriberId), _channelId.ToString());
        }
    }
}