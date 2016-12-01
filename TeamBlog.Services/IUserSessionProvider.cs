namespace TeamBlog.Bl
{
    public interface IUserSessionProvider
    {
        UserSession GetCurrent();
    }
}