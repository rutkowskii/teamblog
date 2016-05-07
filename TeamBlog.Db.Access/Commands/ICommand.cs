namespace TeamBlog.Db.Access.Commands
{
    public interface ICommand
    {
        void Run();
    }

    public interface ICommand<TRes>
    {
        TRes Run();
    }
}