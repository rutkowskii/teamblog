using System;
using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    public interface IUser
    {
        PostDto[] GetGeneralFeedPosts();
        void SubscribeToChannel(Guid channelId);
        void AddPost(NewPostDto newPostDto);
    }
}