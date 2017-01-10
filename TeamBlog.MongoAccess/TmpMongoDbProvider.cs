using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace TeamBlog.MongoAccess
{
    public class TmpMongoDbProvider : IMongoDbProvider
    {
        static TmpMongoDbProvider()
        {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new LocalTimeSerializer());
        }

        public IMongoDatabase Get()
        {
            var connectionString = "mongodb://localhost:27017";
            var mongoClient = new MongoClient(connectionString);
            var db = mongoClient.GetDatabase("teamblog");
            return db;
        }
    }
}