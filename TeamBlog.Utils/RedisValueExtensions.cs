using System;
using System.Dynamic;
using StackExchange.Redis;

namespace TeamBlog.Utils
{
    public static class RedisValueExtensions
    {
        public static Guid ToGuid(this RedisValue redisValue)
        {
            var stringValue = (string) redisValue;
            return new Guid(stringValue);
        }
    }

    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow =>  DateTime.UtcNow;
    }

    public static class DateTimeExtensions
    {
        public static string ToWebFormat(this DateTime dateTime)
        {
            return dateTime.ToString("o");
        }
    }
}