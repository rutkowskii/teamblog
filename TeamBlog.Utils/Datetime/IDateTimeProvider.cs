using System;

namespace TeamBlog.Utils.Datetime
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}