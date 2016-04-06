namespace TeamBlog.RedisAccess
{
    public class RedisDbObjects
    {
        private const string ChannelSubscribers = "channel.subscribers";
        private const string UserNotifications = "user.notifications";
        private const string UserNotificationsNextElement = "user.notifications.nextElement";
        private const string Notifications = "notifications";

        public static string ChannelSubscribersKey(object channelId)
        {
            return ChannelSubscribers + ":" + channelId.ToString();
        }

        public static string UserNotificationsKey(object userId)
        {
            return UserNotifications + ":" + userId.ToString();
        }

        public static string UserNotificationsNextElementKey(object userId)
        {
            return UserNotificationsNextElement + ":" + userId.ToString();
        }

        public static string NotificationsKey(object notificationId)
        {
            return Notifications + ":" + notificationId; //todo dry. 
        }
    }
}