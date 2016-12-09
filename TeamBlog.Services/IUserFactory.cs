using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Commands.Posts;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Posts;
using TeamBlog.Db.Access.Queries.Subscriptions;

namespace TeamBlog.Bl
{
    public interface IUserFactory
    {
        IUser GetCurrentUser();
    }

    public class UserFactory : IUserFactory
    {
        private readonly IChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly IInsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly IGetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly IGetLatestChannelsPostsQueryBuilder _channelPostsQueryBuilder;

        public UserFactory(IChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            IInsertNewPostCommandBuilder insertNewPostCommandBuilder,
            IGetUserChannelsQueryBuilder userChannelsQueryBuilder,
            IGetLatestChannelsPostsQueryBuilder channelPostsQueryBuilder)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
            _userChannelsQueryBuilder = userChannelsQueryBuilder;
            _channelPostsQueryBuilder = channelPostsQueryBuilder;
        }

        public IUser GetCurrentUser()
        {
            return new User(_channelSubscribeCommandBuilder, _insertNewPostCommandBuilder, _userChannelsQueryBuilder, _channelPostsQueryBuilder);
        }
    }
}