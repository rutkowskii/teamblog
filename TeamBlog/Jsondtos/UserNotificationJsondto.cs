using System;

namespace TeamBlog.Jsondtos
{
    public class UserNotificationJsondto
    {
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }
    }
}
