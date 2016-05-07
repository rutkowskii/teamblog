using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class CreateChannelCommandBuilder
    {
        private readonly IMongoAdapter _mongoAdapter;

        public CreateChannelCommandBuilder(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public CreateChannelCommand Build(string name)
        {
            return new CreateChannelCommand(_mongoAdapter, name);
        }
    }
}