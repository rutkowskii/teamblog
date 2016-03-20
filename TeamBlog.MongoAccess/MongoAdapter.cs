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

        public IMongoCollection<Post> PostCollection
        {
            get { return GetCollectionByName<Post>("posts"); }
        }

        public IMongoCollection<Channel> ChannelCollection
        {
            get { return GetCollectionByName<Channel>("channels"); }
        }
        public IMongoCollection<ChannelPost> ChannelPostCollection
        {
            get { return GetCollectionByName<ChannelPost>("channelPosts"); }
        }

        private IMongoCollection<T> GetCollectionByName<T>(string name)
        {
            var db = _mongoDbProvider.Get();
            return db.GetCollection<T>(name);
        }
    }
}