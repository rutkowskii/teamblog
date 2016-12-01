using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBlog.Dtos
{
    public class PostDto
    {
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }

    public class NewPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid[] Channels { get; set; }
    }
}
