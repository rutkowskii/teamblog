using System.Linq;
using MongoDB.Driver;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Queries
{
    public class GetUsersQuery : IQuery<UserDto>
    {
        private readonly IMongoAdapter _mongoAdapter;

        public GetUsersQuery(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public UserDto[] Run()
        {
            return _mongoAdapter.UserCollection.AsQueryable().Select(u => new UserDto {Id = u.Id, Name = u.Name}).ToArray();
        }
    }
}