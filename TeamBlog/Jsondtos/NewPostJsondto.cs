using System;

namespace TeamBlog.Jsondtos
{
    public class NewPostJsondto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid[] Channels { get; set; }
    }
}