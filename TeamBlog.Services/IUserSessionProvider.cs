namespace TeamBlog.Services
{
    public interface IUserSessionProvider
    {
        UserSession GetCurrent();
    }
}