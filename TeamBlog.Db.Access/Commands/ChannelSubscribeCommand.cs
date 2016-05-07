using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class ChannelSubscribeCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private readonly Guid _subsriberId;

        public ChannelSubscribeCommand(IRedisConnection redisConnection, Guid channelId, Guid subsriberId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId;
            _subsriberId = subsriberId;
        }

        public void Run()
        {
            var db = _redisConnection.AccessRedis();
            db.SetAdd(RedisDbObjects.ChannelSubscribersKey(_channelId), _subsriberId.ToString());
        }
    }
}