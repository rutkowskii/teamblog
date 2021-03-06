﻿using System;
using TeamBlog.RedisAccess;

namespace TeamBlog.Db.Access.Commands.Subscriptions
{
    public class ChannelSubscribeCommand
    {
        private readonly IRedisConnection _redisConnection;
        private readonly Guid _channelId;
        private readonly Guid _subscriberId;

        public ChannelSubscribeCommand(IRedisConnection redisConnection, ChannelSubscribeParams channelSubscribeParams)
        {
            _redisConnection = redisConnection;
            _channelId = channelSubscribeParams.ChannelId;
            _subscriberId = channelSubscribeParams.SubscriberId;
        }

        public void Run()
        {
            var db = _redisConnection.Db;
            db.SetAdd(RedisModel.ChannelSubscribers.KeyFor(_channelId), _subscriberId.ToString());
            db.SetAdd(RedisModel.UserChannels.KeyFor(_subscriberId), _channelId.ToString());
        }
    }
}