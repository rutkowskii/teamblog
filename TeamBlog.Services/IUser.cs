using System;
using Ploeh.AutoFixture;
using System.Linq;
using StackExchange.Redis;
using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Queries;
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
        private readonly ChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly IInsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly GetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly GetLatestChannelsPostsQueryBuilder _channelsPostsQueryBuilder;

        private readonly Guid _id = new Guid("668EA071-B0EE-4B1F-8FE4-4E95D4792FC8");

        public User(ChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            IInsertNewPostCommandBuilder insertNewPostCommandBuilder,
            GetUserChannelsQueryBuilder userChannelsQueryBuilder,
            GetLatestChannelsPostsQueryBuilder channelsPostsQueryBuilder)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
            _userChannelsQueryBuilder = userChannelsQueryBuilder;
            _channelsPostsQueryBuilder = channelsPostsQueryBuilder;
        }

        public PostDto[] GetGeneralFeedPosts()
        {
            var channelsAsRedisVals = _userChannelsQueryBuilder.Build(_id).Run().ToArray();
            var channels = Array.ConvertAll(channelsAsRedisVals, x => (string)x).Select(s => new Guid(s)).ToArray();
            var posts = _channelsPostsQueryBuilder.Build(channels).Run();
            return posts;
        }

        public void SubscribeToChannel(Guid channelId)
        {
            _channelSubscribeCommandBuilder.Build(channelId, _id).Run();
        }

        public void AddPost(NewPostDto newPostDto)
        {
            var cmd = _insertNewPostCommandBuilder
                .Build(newPostDto.Channels, newPostDto.Content, newPostDto.Content, _id);
            cmd.Run();

            //todo what about notifications?
        }
    }
}