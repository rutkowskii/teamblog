using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands.Channels
{
    public class CreateChannelCommandBuilder
    {
        private readonly IMongoAdapter _mongoAdapter;

        public CreateChannelCommandBuilder(IMongoAdapter mongoAdapter)
        {
            this._mongoAdapter = mongoAdapter;
        }

        public CreateChannelCommand Build(string name)
        {
            return new CreateChannelCommand(this._mongoAdapter, name);
        }
    }
}