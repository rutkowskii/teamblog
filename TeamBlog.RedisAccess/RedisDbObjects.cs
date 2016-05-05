namespace TeamBlog.RedisAccess
{
    public class RedisDbObjects
    {
        private static readonly string KeySpecialChar = ":";
        private const string ChannelSubscribers = "channel.subscribers";
        private const string UserNotifications = "user.notifications";
        private const string UserNotificationsNextElement = "user.notifications.nextElement";
        private const string Notifications = "notifications";

        public static string ChannelSubscribersKey(object channelId)
        {
            return CreateKey(ChannelSubscribers, channelId.ToString());
        }

        public static string UserNotificationsKey(object userId)
        {
            return CreateKey(UserNotifications, userId.ToString());
        }

        public static string UserNotificationsNextElementKey(object userId)
        {
            return CreateKey(UserNotificationsNextElement, userId.ToString());
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