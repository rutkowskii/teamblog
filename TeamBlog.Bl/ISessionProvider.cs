using System;

namespace TeamBlog.Bl
{
    public interface ISessionProvider
    {
        Guid UserId { get; }
    }
}