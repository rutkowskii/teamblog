using System;
using TeamBlog.Dtos;
using TeamBlog.Model;

namespace TeamBlog.Db.Access
{
    public class PostAddedUserNotificationBuilder
    {
        public PostAddedUserNotification Build(PostAddedDto busMsg)
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