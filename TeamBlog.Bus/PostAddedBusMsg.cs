using System;

namespace TeamBlog.Bus
{
    public class PostAddedBusMsg : IMessage //todo needed?
    {
        public DateTime Timestamp { get; set; }
        public Guid ChannelId { get; set; }
    }
}