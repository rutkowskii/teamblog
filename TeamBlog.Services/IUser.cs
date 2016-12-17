using System;
using System.Linq;
using TeamBlog.Db.Access.Commands.Posts;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries.Posts;
using TeamBlog.Db.Access.Queries.Subscriptions;
using TeamBlog.Dtos;

namespace TeamBlog.Bl
{
    public interface IUser
    {
        PostDto[] GetGeneralFeedPosts();
        void SubscribeToChannel(Guid channelId);
        void AddPost(NewPostDto newPostDto);

        // todo mam co do tego watpliwosci -- przeciez user robi wszystko.. jak to zamodelować?

        // może w user powinno zostać tylko to co dla niego wlasciwe -> typu feed? 
        // z 2ej strony, jeżeli user stanie sie agg rootem to może nie ma tragedii?
    }

    public class User : IUser
    {
        private readonly IChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly IInsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly IGetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly IGetLatestChannelsPostsQueryBuilder _channelsPostsQueryBuilder;

        private readonly Guid _id = new Guid("668EA071-B0EE-4B1F-8FE4-4E95D4792FC8");

        public User(IChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            IInsertNewPostCommandBuilder insertNewPostCommandBuilder,
            IGetUserChannelsQueryBuilder userChannelsQueryBuilder,
            IGetLatestChannelsPostsQueryBuilder channelsPostsQueryBuilder)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
            _userChannelsQueryBuilder = userChannelsQueryBuilder;
            _channelsPostsQueryBuilder = channelsPostsQueryBuilder;
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
            cmd.Run();

            //todo what about notifications?
        }
    }
}