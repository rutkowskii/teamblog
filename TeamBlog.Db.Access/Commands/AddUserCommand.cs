using System;
using TeamBlog.Model;
using TeamBlog.MongoAccess;

namespace TeamBlog.Db.Access.Commands
{
    public class AddUserCommand : ICommand
    {
        private readonly IMongoAdapter _mongoAdapter;
        private readonly string _name;

        public AddUserCommand(IMongoAdapter mongoAdapter, string name)
        {
            _mongoAdapter = mongoAdapter;
            _name = name;
        }

        public void Run()
        {
            var user = new User
            {
                Name = _name,
                Id = Guid.NewGuid()
            };
            _mongoAdapter.UserCollection.InsertOne(user);
        }
    }
}
