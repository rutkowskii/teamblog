using System;

namespace TeamBlog.Dtos
{
    public class PostDto
    {
        public Guid Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public Guid[] Channels { get; set; }
    }
}
