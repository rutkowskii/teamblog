using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBlog.Db.Access;

namespace TeamBlog.Services
{
    public class PostService
    {
        private readonly IUserSessionProvider _userSessionProvider;
        private readonly InsertPostCommandBuilder _insertPostCommandBuilder;
        private readonly NotifyForInsertPostCommandBuilder _notifyForInsertPostCommandBuilder;

        // todo ctor

        public void InsertNewPost(Guid channelId, string url, string description)
        {
            var currentSession = _userSessionProvider.GetCurrent();
            var postInsertionResult = _insertPostCommandBuilder
                .Build(channelId, url, description, currentSession.UserId)
                .Run();
            _notifyForInsertPostCommandBuilder //todo build the cmd using the previous cmd results. 
        }

     

    }

    public interface IUserSessionProvider
    {
        UserSession GetCurrent();
    }

    public class UserSession
    {
        public Guid UserId { get; set; }
    }
}
