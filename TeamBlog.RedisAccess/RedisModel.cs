namespace TeamBlog.RedisAccess
{
    public class RedisModel
    {
        public static ChannelSubscribersCategory ChannelSubscribers = new ChannelSubscribersCategory();
        public static UserNotificationsCategory UserNotifications = new UserNotificationsCategory();
        public static UserChannelsCategory UserChannels = new UserChannelsCategory();
        public static NotificationsCategory Notifications = new NotificationsCategory();

        public abstract class Category
        {
            const string KeySeparator = ":";

            public abstract string Name { get; }

            public string KeyFor(object id)
            {
                return Name + KeySeparator + id;
            }

            public string NextElementKeyFor(object id)
            {
                return Name + ".nextElement" + KeySeparator + id;
            }
        }

        public class ChannelSubscribersCategory : Category
        {
            public override string Name => "channel.subscribers";
        }

        public class UserNotificationsCategory : Category
        {
            public override string Name => "user.notifications";
        }

        public class UserChannelsCategory : Category
        {
            public override string Name => "user.channels";
        }

        public class NotificationsCategory : Category
        {
            public override string Name => "notifications";
        }
    }
}