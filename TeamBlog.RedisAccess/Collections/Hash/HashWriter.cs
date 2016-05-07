namespace TeamBlog.RedisAccess.Collections.Hash
{
    public class HashWriter<T> : RedisAccessorBase
    {
        private readonly string _hashIdentifier;
        private readonly IRedisHashSerializer<T> _redisHashSerializer;

        public HashWriter(IRedisConnection redisConnection, string hashIdentifier, IRedisHashSerializer<T> redisHashSerializer) : base(redisConnection)
        {
            _hashIdentifier = hashIdentifier;
            _redisHashSerializer = redisHashSerializer;
        }

        public void Write(T value)
        {
            var hashEntries = _redisHashSerializer.Serialize(value);
            _redisDb.HashSet(_hashIdentifier, hashEntries);
        }
    }
}