using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries
{
    //todo start from here. 
    /*
        things i need to start playing with angular:
        channelsbyuser query 
        get posts from my channels ---->todo now it is taken from mongo, later change to redis. 

        //adding post. 

    */

    public class GetChannelSubscribersQuery : IQuery<RedisValue>
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private IDatabase _redisDb;

        public GetChannelSubscribersQuery(IRedisConnection redisConnection, Guid channelId)
        {
            _redisConnection = redisConnection;
            _channelId = channelId;
        }

        public IEnumerable<RedisValue> Run()
        {
            _redisDb = _redisConnection.AccessRedis();
            var subscribers = _redisDb
                .SetMembers(RedisDbObjects.ChannelSubscribersKey(_channelId));
            return subscribers.ToList();
        }
    }
}
