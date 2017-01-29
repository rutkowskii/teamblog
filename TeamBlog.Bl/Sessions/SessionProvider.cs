using System;

namespace TeamBlog.Services.Sessions
{
    class SessionProvider : ISessionProvider
    {
        private readonly FakeSessionContainer _sessionContainer;

        public SessionProvider(FakeSessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        public Guid UserId => _sessionContainer.UserId;
    }
}