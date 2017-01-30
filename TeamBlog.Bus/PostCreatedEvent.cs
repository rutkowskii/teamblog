using System;

namespace TeamBlog.Bus
{
    public class PostCreatedEvent: IMessage
    {
        public Guid[] ChannelIds { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}