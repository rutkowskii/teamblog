using System;

namespace TeamBlog.Db.Access
{
    public class InsertNewPostCommandResult
    {
        public Guid NewPostId { get; set; }
        public string OurUrl { get; set; }
    }
}