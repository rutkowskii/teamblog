using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Queries.Subscriptions
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
            this._redisConnection = redisConnection;
            this._channelId = channelId;
        }

        public IEnumerable<RedisValue> Run()
        {
            this._redisDb = this._redisConnection.AccessRedis();
            var subscribers = this._redisDb
                .SetMembers(RedisDbObjects.ChannelSubscribersKey(this._channelId));
            return subscribers.ToList();
        }
    }
}
