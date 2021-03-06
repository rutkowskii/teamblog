using TeamBlog.Utils;

namespace TeamBlog.RedisAccess
{
    public class RedisAccessIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IRedisConnectionParamsProvider, TestsRedisConnectionParamsProvider>();
            BindSingleton<IRedisConnection, RedisConnection>();
        }
    }
}