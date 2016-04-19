using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBlog.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
