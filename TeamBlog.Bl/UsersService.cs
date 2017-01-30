using TeamBlog.Db.Access.Queries;

namespace TeamBlog.Bl
{
    class UsersService : IUsersService
    {
        private readonly GetUsersQuery _getUsersQuery;

        public UsersService(GetUsersQuery getUsersQuery)
        {
            _getUsersQuery = getUsersQuery;
        }

        public UserDto[] GetAll()
        {
            return _getUsersQuery.Run();
        }
    }
}