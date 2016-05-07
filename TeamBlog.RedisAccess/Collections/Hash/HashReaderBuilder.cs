namespace TeamBlog.RedisAccess.Collections.Hash
{
    public class HashReaderBuilder<T>
    {
        private readonly IRedisHashDeserializer<T> _deserializer;
        private readonly IRedisConnection _redisConnection;

        public HashReaderBuilder(IRedisHashDeserializer<T> deserializer, IRedisConnection redisConnection)
        {
            _deserializer = deserializer;
            _redisConnection = redisConnection;
        }

        public HashReader<T> Build(string hashIdentifier)
        {
            return new HashReader<T>(_redisConnection, hashIdentifier, _deserializer);
        } 
    }
}