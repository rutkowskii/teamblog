using System;
using System.Linq;
using TeamBlog.Bus;
using TeamBlog.Db.Access.Commands.Posts;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries.Posts;
using TeamBlog.Db.Access.Queries.Subscriptions;
using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    public class User : IUser
    {
        private readonly IChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly IInsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly IGetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly IGetLatestChannelsPostsQueryBuilder _channelsPostsQueryBuilder;
        private readonly IBus _bus;

        private readonly Guid _id = new Guid("668EA071-B0EE-4B1F-8FE4-4E95D4792FC8");

        public User(IChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            IInsertNewPostCommandBuilder insertNewPostCommandBuilder,
            IGetUserChannelsQueryBuilder userChannelsQueryBuilder,
            IGetLatestChannelsPostsQueryBuilder channelsPostsQueryBuilder, 
            IBus bus)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
            _userChannelsQueryBuilder = userChannelsQueryBuilder;
            _channelsPostsQueryBuilder = channelsPostsQueryBuilder;
            _bus = bus;
        }

        public PostDto[] GetGeneralFeedPosts()
        {
            var channels = _userChannelsQueryBuilder.Build(_id).Run().ToArray();
            var posts = _channelsPostsQueryBuilder.Build(channels).Run();
            return posts;
        }

        public void SubscribeToChannel(Guid channelId)
        {
            _channelSubscribeCommandBuilder.Build(new ChannelSubscribeParams(channelId, _id)).Run();
        }

        public void AddPost(NewPostDto newPostDto)
        {
            var cmd = _insertNewPostCommandBuilder
                .Build(new InsertNewPostParams(newPostDto.Channels, newPostDto.Content, newPostDto.Content, _id));
            var result = cmd.Run();
            
            _bus.Publish(new PostCreatedEvent {ChannelIds = newPostDto.Channels });
            
        }
    }
}