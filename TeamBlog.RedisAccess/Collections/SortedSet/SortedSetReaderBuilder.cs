using StackExchange.Redis;
using TeamBlog.Utils;

namespace TeamBlog.RedisAccess.Collections.SortedSet
{
    public class SortedSetReaderBuilder
    {
        private readonly IRedisConnection _redisConnection;

        public SortedSetReaderBuilder(IRedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public SortedSetReader Build(string setIdentifier, Order order)
        {
            return new SortedSetReader(_redisConnection, setIdentifier, order, PagingParams.All);
        }

        public SortedSetReader Build(string setIdentifier, Order order, PagingParams pagingParams)
        {
            return new SortedSetReader(_redisConnection, setIdentifier, order, pagingParams);
        }
    }
}