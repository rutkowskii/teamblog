using System;
using Ploeh.AutoFixture;
using System.Linq;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    public interface IUser
    {
        PostDto[] GetGeneralFeedPosts();
        void SubscribeToChannel(Guid channelId);
        void AddPost(NewPostDto newPostDto);
    }

    public class User : IUser
    {
        private readonly ChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly InsertNewPostCommandBuilder _insertNewPostCommandBuilder;

        private readonly Guid _id = new Guid("668EA071-B0EE-4B1F-8FE4-4E95D4792FC8");

        public User(ChannelSubscribeCommandBuilder channelSubscribeCommandBuilder)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
        }

        public PostDto[] GetGeneralFeedPosts()
        {
            return new Fixture().CreateMany<PostDto>().ToArray(); //todo call to the real backend. 
        }

        public void SubscribeToChannel(Guid channelId)
        {
            _channelSubscribeCommandBuilder.Build(channelId, _id);
        }

        public void AddPost(NewPostDto newPostDto)
        {
            var cmd = _insertNewPostCommandBuilder
                .Build(newPostDto.Channels, newPostDto.Content, newPostDto.Content, _id);
            cmd.Run();

            //todo what about notifications?
        }


        //todo should the user add the post to the channel? probably..
    }
}