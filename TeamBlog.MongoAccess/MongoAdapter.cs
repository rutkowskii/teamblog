using System;
using MongoDB.Driver;
using TeamBlog.Model;

namespace TeamBlog.MongoAccess
{
    public class MongoAdapter : IMongoAdapter
    {
        private readonly IMongoDbProvider _mongoDbProvider;

        public MongoAdapter(IMongoDbProvider mongoDbProvider)
        {
            _mongoDbProvider = mongoDbProvider;
        }

        public IMongoCollection<Post> PostCollection => GetCollectionByName<Post>("posts");

        public IMongoCollection<Channel> ChannelCollection => GetCollectionByName<Channel>("channels");

        public IMongoCollection<ChannelPost> ChannelPostCollection => GetCollectionByName<ChannelPost>("channelPosts");

        public IMongoCollection<User> UserCollection => GetCollectionByName<User>("users");

        private IMongoCollection<T> GetCollectionByName<T>(string name)
        {
            var db = _mongoDbProvider.Get();
            return db.GetCollection<T>(name);
        }
    }
}