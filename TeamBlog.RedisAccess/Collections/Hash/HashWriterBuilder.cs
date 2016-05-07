namespace TeamBlog.RedisAccess.Collections.Hash
{
    public class HashWriterBuilder<T>
    {
        private readonly IRedisConnection _redisConnection;
        private readonly IRedisHashSerializer<T> _serializer;

        public HashWriterBuilder(IRedisConnection redisConnection, IRedisHashSerializer<T> serializer)
        {
            _redisConnection = redisConnection;
            _serializer = serializer;
        }

        public HashWriter<T> Build(string hashIdentifier)
        {
            return new HashWriter<T>(_redisConnection, hashIdentifier, _serializer);
        } 
    }
}