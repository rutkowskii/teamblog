namespace TeamBlog.Db.Access.Commands
{
    public interface IAddUserCommandBuilder
    {
        AddUserCommand Build(string name);
    }
}