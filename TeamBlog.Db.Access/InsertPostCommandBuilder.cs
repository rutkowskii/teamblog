using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access
{
    public class InsertPostCommandBuilder
    {
        private readonly IMongoAdapter _mongoAdapter;

        public InsertPostCommandBuilder(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public InsertPostCommand Build(Guid channelId, string url, string description, Guid userId)
        {
            return new InsertPostCommand(_mongoAdapter, channelId, url, description, userId);
        }
    }
}