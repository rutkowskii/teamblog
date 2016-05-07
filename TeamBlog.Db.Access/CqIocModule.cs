using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}