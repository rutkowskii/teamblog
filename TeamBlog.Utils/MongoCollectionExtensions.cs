using MongoDB.Driver;

namespace TeamBlog.Utils
{
    public static class MongoCollectionExtensions
    {
        public static void Clear<T>(this IMongoCollection<T> collection)
        {
            collection.DeleteMany(Builders<T>.Filter.Empty);
        }
    }
}