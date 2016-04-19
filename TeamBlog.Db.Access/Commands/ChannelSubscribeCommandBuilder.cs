using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access
{
    public class ChannelSubscribeCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public ChannelSubscribeCommandBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public ChannelSubscribeCommand Build(Guid channelId, Guid subsriberId)
        {
            return new ChannelSubscribeCommand(_redisConnection, channelId, subsriberId);
        }
    }
}