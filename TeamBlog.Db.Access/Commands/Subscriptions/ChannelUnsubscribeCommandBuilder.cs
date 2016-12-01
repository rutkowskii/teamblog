using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class ChannelUnsubscribeCommandBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public ChannelUnsubscribeCommandBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public ChannelUnsubscribeCommand Build(Guid channelId, Guid subsriberId)
        {
            return new ChannelUnsubscribeCommand(_redisConnection, channelId, subsriberId);
        }
    }
}