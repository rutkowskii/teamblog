using MongoDB.Driver;

namespace TeamBlog.MongoAccess
{
    public interface IMongoDbProvider
    {
        IMongoDatabase Get();
    }
}