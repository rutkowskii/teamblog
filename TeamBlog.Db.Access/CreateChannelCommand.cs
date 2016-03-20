﻿using System;
using TeamBlog.Model;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access
{
    public class CreateChannelCommand : ICommand
    {
        private readonly string _name;
        private readonly IMongoAdapter _mongoAdapter;

        public CreateChannelCommand(IMongoAdapter mongoAdapter, string name)
        {
            _mongoAdapter = mongoAdapter;
            _name = name;
        }

        public void Run()
        {
            var newChannel = new Channel
            {
                Id = Guid.NewGuid(),
                Name = _name
            };
            _mongoAdapter.ChannelCollection.InsertOne(newChannel);
        }
    }
}