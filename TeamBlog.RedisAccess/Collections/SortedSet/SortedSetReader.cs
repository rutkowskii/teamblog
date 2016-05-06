using StackExchange.Redis;
using TeamBlog.Utils;

namespace TeamBlog.RedisAccess.Collections.SortedSet
{
    public class SortedSetReader : RedisAccessorBase
    {
        private readonly string _sortedSetIdentifier;
        private readonly Order _scoreOrder;
        private readonly PagingParams _pagingParams;

        public SortedSetReader(IRedisConnection redisConnection, string sortedSetIdentifier, Order scoreOrder, PagingParams pagingParams)
            :base(redisConnection)
        {
            _sortedSetIdentifier = sortedSetIdentifier;
            _scoreOrder = scoreOrder;
            _pagingParams = pagingParams;
        }

        public RedisValue[] Resolve()
        {
            RedisValue[] resultingValues;
            if (_pagingParams.TakesAll)
            {
                resultingValues = _redisDb.SortedSetRangeByScore(_sortedSetIdentifier, order: _scoreOrder);
            }
            else
            {
                resultingValues = _redisDb.SortedSetRangeByScore(_sortedSetIdentifier,
                    order: _scoreOrder,
                    skip: _pagingParams.Index,
                    take: _pagingParams.Count);
            }
            return resultingValues;
        }
    }
}
