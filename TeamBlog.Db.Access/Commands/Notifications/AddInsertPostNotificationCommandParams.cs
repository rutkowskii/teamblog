using System;

namespace TeamBlog.Db.Access.Commands.Notifications
{
    public class AddInsertPostNotificationCommandParams
    {
        public Guid[] ChannelIds { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}