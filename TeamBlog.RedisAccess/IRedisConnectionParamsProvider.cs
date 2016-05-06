namespace TeamBlog.RedisAccess
{
    public interface IRedisConnectionParamsProvider
    {
        int DatabaseNumber { get; }
        string ConnectionString { get; }
        int ServerPort { get; }
        string ServerUrl { get; }
    }
}