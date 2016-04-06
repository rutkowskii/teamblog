using System;

namespace TeamBlog.Bus
{
    public class PostAddedBusMsg
    {
        public DateTime Timestamp { get; set; }
        public Guid ChannelId { get; set; }
    }
}