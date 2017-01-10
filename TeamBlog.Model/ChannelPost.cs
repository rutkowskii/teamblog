using System;

namespace TeamBlog.Model
{
    public class ChannelPost
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid ChannelId { get; set; }
        public DateTime InsertionTime { get; set; }
    }
}