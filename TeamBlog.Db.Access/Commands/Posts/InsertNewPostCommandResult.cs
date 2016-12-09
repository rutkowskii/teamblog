using System;

namespace TeamBlog.Db.Access.Commands.Posts
{
    public class InsertNewPostCommandResult
    {
        public Guid NewPostId { get; set; }
        public string OurUrl { get; set; }
    }
}