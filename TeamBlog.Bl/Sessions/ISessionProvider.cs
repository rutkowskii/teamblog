using System;

namespace TeamBlog.Services.Sessions
{
    public interface ISessionProvider
    {
        Guid UserId { get; }
    }
}