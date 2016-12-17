using StackExchange.Redis;

namespace TeamBlog.RedisAccess
{
    public interface IRedisConnection
    {
        IDatabase Db { get; }
        void FlushDb();
    }
}