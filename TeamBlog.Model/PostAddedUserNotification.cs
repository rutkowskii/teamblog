using System;

namespace TeamBlog.Model
{
    public class PostAddedUserNotification
    {
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }
    }
}