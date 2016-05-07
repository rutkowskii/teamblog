using System;
using StackExchange.Redis;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;

namespace TeamBlog.Db.Access.Queries
{
    public class PostAddedUserNotificationDeserializer : IRedisHashDeserializer<PostAddedUserNotification>
    {
        public string[] FieldNames
        {
            get { return new[] {"Timestamp", "Content"}; }
        }

        public PostAddedUserNotification Deserialize(RedisValue[] hashValues)
        {
            return new PostAddedUserNotification
            {
                Timestamp = DateTime.Parse(hashValues[0]),
                Content = hashValues[1]
            };
        }
    }
}