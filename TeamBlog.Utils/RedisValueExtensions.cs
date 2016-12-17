using System;
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
}