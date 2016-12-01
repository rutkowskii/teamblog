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

        public UserFactory(ChannelSubscribeCommandBuilder channelSubscribeCommandBuilder)
        {
            this._channelSubscribeCommandBuilder = channelSubscribeCommandBuilder;
        }

        public IUser GetCurrentUser()
        {
            return new User(_channelSubscribeCommandBuilder);
        }
    }
}