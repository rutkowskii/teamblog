using TeamBlog.Db.Access.Commands;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Db.Access.Commands.Posts;
using TeamBlog.Db.Access.Commands.Subscriptions;
using TeamBlog.Db.Access.Queries;
using TeamBlog.Db.Access.Queries.Channels;
using TeamBlog.Db.Access.Queries.Posts;
using TeamBlog.Db.Access.Queries.Subscriptions;
using TeamBlog.Model;
using TeamBlog.RedisAccess.Collections.Hash;
using TeamBlog.Utils;

namespace TeamBlog.Db.Access
{
    public class CqIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IRedisHashSerializer<PostAddedUserNotification>, PostAddedUserNotificationHashSerializer>();
            BindTransient<IRedisHashDeserializer<PostAddedUserNotification>, PostAddedUserNotificationDeserializer>();

            BindFactory<IInsertNewPostCommandBuilder>();
            BindFactory<IChannelSubscribeCommandBuilder>();
            BindFactory<IChannelUnsubscribeCommandBuilder>();
            BindFactory<ICreateChannelCommandBuilder>();
            BindFactory<IChannelsQueryBuilder>();
            BindFactory<IAddInsertPostNotificationCommandBuilder>();

            BindFactory<IGetChannelSubscribersQueryBuilder>();
            BindFactory<IGetUserChannelsQueryBuilder>();
            BindFactory<IGetLatestChannelsPostsQueryBuilder>();
        }

    }
}