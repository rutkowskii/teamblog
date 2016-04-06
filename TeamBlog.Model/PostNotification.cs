using System;

namespace TeamBlog.Model
{
    public class PostNotification
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid PostId { get; set; }
    }
}