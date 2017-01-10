using MongoDB.Driver;
using TeamBlog.Model;

namespace TeamBlog.MongoAccess
{
    public interface IMongoAdapter
    {
        IMongoCollection<Post> PostCollection { get; }
        IMongoCollection<Channel> ChannelCollection { get; }
        IMongoCollection<ChannelPost> ChannelPostCollection { get; }
        IMongoCollection<User> UserCollection { get; }
    }
}
