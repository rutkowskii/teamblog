namespace TeamBlog.Db.Access
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