namespace TeamBlog.RedisAccess.Collections.SortedSet
{
    public class SortedSetWriterBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public SortedSetWriterBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public SortedSetWriter Build(
            string nextElementIdentifier,
            string sortedSetIdentifier)
        {
            return new SortedSetWriter(nextElementIdentifier, sortedSetIdentifier, _redisConnection, 1L);
        }
    }
}