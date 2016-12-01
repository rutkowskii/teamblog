using System;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class InsertNewPostCommandBuilder
    {
        private readonly IMongoAdapter _mongoAdapter;

        public InsertNewPostCommandBuilder(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public InsertNewPostCommand Build(Guid[] channelIds, string title, string content, Guid userId)
        {
            return new InsertNewPostCommand(_mongoAdapter, channelIds, title, content, userId);
        }
    }
}