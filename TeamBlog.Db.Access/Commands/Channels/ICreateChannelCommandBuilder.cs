using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands.Channels
{
    public interface ICreateChannelCommandBuilder
    {
        CreateChannelCommand Build(string name);
    }
}