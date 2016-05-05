namespace TeamBlog.RedisAccess
{
    public class TestsRedisConnectionParamsProvider : IRedisConnectionParamsProvider
    {
        public int DatabaseNumber { get { return 1; } }

        public string ConnectionString
        {
            get { return "127.0.0.1:6379,allowAdmin=true"; }
        }
    }
}