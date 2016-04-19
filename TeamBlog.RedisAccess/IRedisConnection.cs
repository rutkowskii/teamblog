using StackExchange.Redis;

namespace TeamBlog.RedisAccess
{
    public interface IRedisConnection
    {
        IDatabase AccessRedis();
        void Flush();
    }
}