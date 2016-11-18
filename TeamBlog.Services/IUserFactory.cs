namespace TeamBlog.Services
{
    public interface IUserFactory
    {
        IUser GetCurrentUser();
    }

    public class UserFactory : IUserFactory
    {
        public IUser GetCurrentUser()
        {
            return new User();
        }
    }
}