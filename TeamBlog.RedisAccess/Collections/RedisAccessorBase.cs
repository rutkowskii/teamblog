using StackExchange.Redis;

namespace TeamBlog.RedisAccess.Collections
{
    public abstract class RedisAccessorBase
    {
        protected IDatabase _redisDb;

        protected RedisAccessorBase(IRedisConnection redisConnection)
        {
            _redisDb = redisConnection.Db;
        }
    }
}