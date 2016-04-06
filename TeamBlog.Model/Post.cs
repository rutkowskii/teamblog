using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBlog.Bus;

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


    public class PostAddedUserNotification
    {
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }
    }

    public class PostAddedUserNotificationBuilder
    {
        public PostAddedUserNotification Build(PostAddedBusMsg busMsg)
        {
            //todo. 
            return new PostAddedUserNotification
            {
                Content = "[Post content starts]... Y added new post to channel X",
                Timestamp = busMsg.Timestamp,
                Id = Guid.NewGuid()
            };
        }
    }
}
