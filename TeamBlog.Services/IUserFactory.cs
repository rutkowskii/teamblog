using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Subscriptions;

namespace TeamBlog.Bl
{
    public interface IUserFactory
    {
        IUser GetCurrentUser();
    }

    public class UserFactory : IUserFactory
    {
        private readonly ChannelSubscribeCommandBuilder _channelSubscribeCommandBuilder;
        private readonly InsertNewPostCommandBuilder _insertNewPostCommandBuilder;
        private readonly GetUserChannelsQueryBuilder _userChannelsQueryBuilder;
        private readonly GetLatestChannelsPostsQueryBuilder _channelPostsQueryBuilder;

        public UserFactory(ChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            InsertNewPostCommandBuilder insertNewPostCommandBuilder,
            GetUserChannelsQueryBuilder userChannelsQueryBuilder,
            GetLatestChannelsPostsQueryBuilder channelPostsQueryBuilder)
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