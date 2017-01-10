using System;

namespace TeamBlog.Utils.Datetime
{
    public static class DateTimeExtensions
    {
        public static string ToWebFormat(this DateTime dateTime)
        {
            return dateTime.ToString("o");
        }
    }
}