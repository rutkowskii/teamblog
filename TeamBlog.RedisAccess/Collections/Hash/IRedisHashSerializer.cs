using StackExchange.Redis;

namespace TeamBlog.RedisAccess.Collections.Hash
{
    public interface IRedisHashSerializer<T>
    {
        HashEntry[] Serialize(T value);
    }
}