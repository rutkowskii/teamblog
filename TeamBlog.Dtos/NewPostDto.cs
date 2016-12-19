using System;

namespace TeamBlog.Dtos
{
    public class NewPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid[] Channels { get; set; }
    }
}