namespace TeamBlog.RedisAccess.Collections.SortedSet
{
    public class SortedSetWriter : RedisAccessorBase
    {
        private readonly string _nextElementIdentifier;
        private readonly string _sortedSetIdentifier;
        private readonly long _maxScoreIncreaseStep;

        public SortedSetWriter(
            string nextElementIdentifier,
            string sortedSetIdentifier,
            IRedisConnection redisConnection, long maxScoreIncreaseStep) : base(redisConnection)
        {
            _nextElementIdentifier = nextElementIdentifier;
            _sortedSetIdentifier = sortedSetIdentifier;
            _maxScoreIncreaseStep = maxScoreIncreaseStep;
        }

        public void Append(string value)
        {
            var newNotificationScore = _redisDb.StringIncrement(_nextElementIdentifier, _maxScoreIncreaseStep);
            _redisDb.SortedSetAdd(_sortedSetIdentifier, value, newNotificationScore);
        }
    }
}