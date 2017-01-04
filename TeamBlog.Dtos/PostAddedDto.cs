using System;

namespace TeamBlog.Dtos
{
    public class PostAddedDto
    {
        // todo this class makes no sense at all :(
        public DateTime Timestamp { get; set; }
        public Guid ChannelId { get; set; }
    }
}