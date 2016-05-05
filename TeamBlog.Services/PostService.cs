using System;
using TeamBlog.Db.Access;

namespace TeamBlog.Services
{
    public class PostService
    {
        private readonly IUserSessionProvider _userSessionProvider;
        private readonly InsertPostCommandBuilder _insertPostCommandBuilder;

        public PostService(IUserSessionProvider userSessionProvider, InsertPostCommandBuilder insertPostCommandBuilder)
        {
            _userSessionProvider = userSessionProvider;
            _insertPostCommandBuilder = insertPostCommandBuilder;
        }

        public void InsertNewPost(Guid channelId, string url, string description)
        {
            var currentSession = _userSessionProvider.GetCurrent();
            var postInsertionResult = _insertPostCommandBuilder
                .Build(channelId, url, description, currentSession.UserId)
                .Run();
            //todo build the cmd using the previous cmd results. 
            throw new NotImplementedException();
        }
    }
}