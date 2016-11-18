using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class InsertPostCommandBuilder
    {
        private readonly IMongoAdapter _mongoAdapter;

        public InsertPostCommandBuilder(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public InsertNewPostCommand Build(Guid channelId, string url, string description, Guid userId)
        {
            return new InsertNewPostCommand(_mongoAdapter, channelId, url, description, userId);
        }
    }
}