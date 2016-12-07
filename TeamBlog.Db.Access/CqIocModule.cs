using Ninject.Extensions.Factory;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access
{
    public class CqIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IRedisHashSerializer<PostAddedUserNotification>, PostAddedUserNotificationHashSerializer>();
            BindTransient<IRedisHashDeserializer<PostAddedUserNotification>, PostAddedUserNotificationDeserializer>();

            /*
            todo seem we gonna need overwriting the default provider:
            https://github.com/ninject/Ninject.Extensions.Factory/wiki/Factory-interface%3A-Referencing-Named-Bindings
            */

            Kernel.Bind<IInsertNewPostCommandBuilder>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());

        }
    }
}