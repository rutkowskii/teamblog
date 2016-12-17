using System;

namespace TeamBlog.Jsondtos
{
    public class PostJsondto
    {
        public string Timestamp { get; set; } //todo is this a good place?
        public string Content { get; set; }
        public string AddedBy { get; set; }
        public string Channel { get; set; }
    }

    public class NewPostJsondto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid[] Channels { get; set; }
    }
}