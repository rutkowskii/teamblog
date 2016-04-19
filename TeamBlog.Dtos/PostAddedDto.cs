using System;

namespace TeamBlog.Dtos
{
    public class PostAddedDto
    {
        public DateTime Timestamp { get; set; }
        public Guid ChannelId { get; set; }
    }
}