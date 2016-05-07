using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class ChannelUnsubscribeCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private readonly Guid _subsriberId;

        public ChannelUnsubscribeCommand(IRedisConnection redisConnection, Guid channelId, Guid subsriberId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId;
            _subsriberId = subsriberId;
        }

        public void Run()
        {
            var db = _redisConnection.AccessRedis();
            db.SetRemove(RedisDbObjects.ChannelSubscribersKey(_channelId), _subsriberId.ToString());
        }
    }
}