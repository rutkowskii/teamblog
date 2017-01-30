using TeamBlog.Db.Access.Queries;

namespace TeamBlog.Bl
{
    public interface IUsersService
    {
        UserDto[] GetAll();
    }
}