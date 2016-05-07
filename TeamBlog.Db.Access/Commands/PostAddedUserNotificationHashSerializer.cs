using System.Globalization;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;

namespace TeamBlog.Db.Access.Commands
{
    public class PostAddedUserNotificationHashSerializer : IRedisHashSerializer<PostAddedUserNotification>
    {
        public HashEntry[] Serialize(PostAddedUserNotification value)
        {
            var hashEntries = new[]
            {
                new HashEntry("Timestamp", value.Timestamp.ToString(DateTimeFormatInfo.InvariantInfo)),
                new HashEntry("Content", value.Content)
            };
            return hashEntries;
        }
    }
}