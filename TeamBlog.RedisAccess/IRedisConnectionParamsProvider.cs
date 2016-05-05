namespace TeamBlog.RedisAccess
{
    public interface IRedisConnectionParamsProvider
    {
        int DatabaseNumber { get; }
        string ConnectionString { get; }
    }
}