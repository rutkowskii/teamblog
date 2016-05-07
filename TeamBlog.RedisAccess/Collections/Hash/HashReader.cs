using System.Linq;
using StackExchange.Redis;

namespace TeamBlog.RedisAccess.Collections.Hash
{
    public class HashReader<T> : RedisAccessorBase
    {
        private readonly string _hashIdentifier;
        private readonly IRedisHashDeserializer<T> _deserializer;

        public HashReader(IRedisConnection redisConnection, string hashIdentifier, IRedisHashDeserializer<T>  deserializer) : base(redisConnection)
        {
            _hashIdentifier = hashIdentifier;
            _deserializer = deserializer;
        }

        public T Read()
        {
            var fieldValues =_redisDb.HashGet(_hashIdentifier, _deserializer.FieldNames.Select(f => (RedisValue)f).ToArray());
            var res = _deserializer.Deserialize(fieldValues);
            return res;
        }
    }
}