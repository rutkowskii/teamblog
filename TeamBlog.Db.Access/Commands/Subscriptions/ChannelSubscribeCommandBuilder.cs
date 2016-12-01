using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class ChannelSubscribeCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public ChannelSubscribeCommandBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public ChannelSubscribeCommand Build(Guid channelId, Guid subscriberId)
        {
            return new ChannelSubscribeCommand(_redisConnection, channelId, subscriberId);
        }
    }
}