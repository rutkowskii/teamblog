namespace TeamBlog.RedisAccess
{
    public class RedisDbObjects
    {
        private static readonly string KeySpecialChar = ":";
        private const string ChannelSubscribers = "channel.subscribers";
        private const string UserNotifications = "user.notifications";
        private const string UserChannels = "user.channels";
        private const string Notifications = "notifications";

        public static string ChannelSubscribersKey(object channelId)
        {
            return CreateKey(ChannelSubscribers, channelId.ToString());
        }

        public static string UserChannelsKey(object userId) //todo refactor to more generic implementation
        {
            return CreateKey(UserChannels, userId.ToString());
        }

        public static string UserNotificationsKey(object userId)
        {
            return CreateKey(UserNotifications, userId.ToString());
        }

        public static string UserNotificationsNextElementKey(object userId)
        {
            return CreateKeyForNextElement(UserNotifications, userId.ToString());
        }

        private static string CreateKeyForNextElement(string identifier, string value)
        {
            return CreateKey(identifier + ".nextElement", value);
        }

        public static string NotificationsKey(object notificationId)
        {
            return CreateKey(Notifications, notificationId.ToString());
        }

        private static string CreateKey(string category, string id)
        {
            return category + KeySpecialChar + id;
        }
    }
}