namespace TeamBlog.Jsondtos
{
    public class PostJsondto
    {
        public string Timestamp { get; set; }
        public string Content { get; set; }
        public string AddedBy { get; set; }
        public string[] Channels { get; set; }
    }
}