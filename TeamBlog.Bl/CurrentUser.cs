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
    internal class CurrentUser : IUser
    {
        private readonly IChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly IInsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly IGetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly IGetLatestChannelsPostsQueryBuilder _channelsPostsQueryBuilder;
        private readonly IBus _bus;
        private readonly ISessionProvider _sessionProvider;
        private readonly NewPostDtoValidator _newPostDtoValidator;

        public CurrentUser(IChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            IInsertNewPostCommandBuilder insertNewPostCommandBuilder,
            IGetUserChannelsQueryBuilder userChannelsQueryBuilder,
            IGetLatestChannelsPostsQueryBuilder channelsPostsQueryBuilder,
            IBus bus,
            ISessionProvider sessionProvider, 
            NewPostDtoValidator newPostDtoValidator)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
            _userChannelsQueryBuilder = userChannelsQueryBuilder;
            _channelsPostsQueryBuilder = channelsPostsQueryBuilder;
            _bus = bus;
            _sessionProvider = sessionProvider;
            _newPostDtoValidator = newPostDtoValidator;
        }

        public PostDto[] GetGeneralFeedPosts()
        {
            var channels = _userChannelsQueryBuilder.Build(CurrentUserId).Run().ToArray();
            var posts = _channelsPostsQueryBuilder.Build(channels).Run();
            return posts;
        }

        public void SubscribeToChannel(Guid channelId)
        {
            _channelSubscribeCommandBuilder.Build(new ChannelSubscribeParams(channelId, CurrentUserId)).Run();
        }

        public void AddPost(NewPostDto newPostDto)
        {
            _newPostDtoValidator.ThrowIfInvalid(newPostDto);
            var cmd = _insertNewPostCommandBuilder
                .Build(BuildInsertNewPostParams(newPostDto));
            var result = cmd.Run();

            _bus.Publish(new PostCreatedEvent {ChannelIds = newPostDto.Channels});
        }

        private InsertNewPostParams BuildInsertNewPostParams(NewPostDto newPostDto)
        {
            return new InsertNewPostParams(
                channelIds: newPostDto.Channels, 
                title: newPostDto.Content,
                content: newPostDto.Content, 
                userId: CurrentUserId);
        }

        private Guid CurrentUserId => _sessionProvider.UserId;
    }
}