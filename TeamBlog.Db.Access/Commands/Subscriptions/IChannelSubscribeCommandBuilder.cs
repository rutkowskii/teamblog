namespace TeamBlog.Db.Access.Commands.Subscriptions
{
    public interface IChannelSubscribeCommandBuilder
    {
        ChannelSubscribeCommand Build(ChannelSubscribeParams channelSubscribeParams);
    }
}