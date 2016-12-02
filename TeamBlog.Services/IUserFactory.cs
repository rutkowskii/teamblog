using TeamBlog.Db.Access.Commands;

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

        public UserFactory(ChannelSubscribeCommandBuilder channelSubscribeCommandBuilder,
            InsertNewPostCommandBuilder insertNewPostCommandBuilder)
        {
            _channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
            _insertNewPostCommandBuilder = insertNewPostCommandBuilder;
        }

        public IUser GetCurrentUser()
        {
            return new User(_channelSubscribeCommandBuilder, _insertNewPostCommandBuilder);
        }
    }
}