using StackExchange.Redis;

namespace TeamBlog.RedisAccess.Collections.Hash
{
    public interface IRedisHashDeserializer<T>
    {
        string[] FieldNames { get;  }
        T Deserialize(RedisValue[] hashValues);
    }
}