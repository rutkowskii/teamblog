using TeamBlog.Utils;

namespace TeamBlog.RedisAccess
{
    public class RedisAccessIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindSingleton<IRedisConnection, RedisConnection>();
        }
    }
}