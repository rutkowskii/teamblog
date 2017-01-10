using System;

namespace TeamBlog.Utils.Datetime
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow =>  DateTime.UtcNow;
    }
}