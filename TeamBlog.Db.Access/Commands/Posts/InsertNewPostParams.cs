using System;

namespace TeamBlog.Db.Access.Commands.Posts
{
    public class InsertNewPostParams
    {
        public InsertNewPostParams(Guid[] channelIds, string title, string content, Guid userId)
        {
            ChannelIds = channelIds;
            Title = title;
            Content = content;
            UserId = userId;
        }

        public Guid[] ChannelIds { get; }

        public string Title { get; }

        public string Content { get; }

        public Guid UserId { get; }
    }
}