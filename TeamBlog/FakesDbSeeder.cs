using System;
using MongoDB.Driver;
using Ninject;
using TeamBlog.App_Start;
using TeamBlog.Bl;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Services.Sessions;
using TeamBlog.Utils;

namespace TeamBlog
{
    public class FakesDbSeeder
    {
        public void InsertFakes()
        {
            var K = NinjectWebCommon.Kernel;

            CleanAll(K);

            SetupUsers(K);
            SetupChannels(K);
            var channelId = SetupSubscriptions(K);
            SetupPosts(K, channelId);
        }

        private static void CleanAll(IKernel K)
        {
            K.Get<IMongoAdapter>().ChannelCollection.Clear();
            K.Get<IMongoAdapter>().UserCollection.Clear();
            K.Get<IMongoAdapter>().PostCollection.Clear();
            K.Get<IMongoAdapter>().ChannelPostCollection.Clear();
            K.Get<IRedisConnection>().FlushDb();
        }

        private static void SetupPosts(IKernel K, Guid channelId)
        {
            K.Get<IUser>()
                .AddPost(new NewPostDto
                {
                    Channels = new[] {channelId},
                    Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw",
                    Title = "enbie"
                });
            K.Get<IUser>()
                .AddPost(new NewPostDto
                {
                    Channels = new[] {channelId},
                    Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw",
                    Title = "enbie"
                });
        }

        private static Guid SetupSubscriptions(IKernel K)
        {
            var channelId = K.Get<IMongoAdapter>().ChannelCollection.AsQueryable().First().Id;

            K.Get<IUser>().SubscribeToChannel(channelId);
            return channelId;
        }

        private static void SetupChannels(IKernel K)
        {
            K.Get<ICreateChannelCommandBuilder>().Build("śmieszki").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("dev general").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("programming").Run();
        }

        private static void SetupUsers(IKernel K)
        {
            K.Get<IMongoAdapter>().UserCollection.InsertOne(new User
            {
                Id = new Guid("388FED2C-879F-4F9E-86A9-68C4C410F56E"),
                Name = "James Doe"
            });
            K.Get<IMongoAdapter>().UserCollection.InsertOne(new User
            {
                Id = new Guid("4FAF742B-53FD-45B6-99AB-5C0FEABB070A"),
                Name = "Mark Smith"
            });
            K.Get<FakeSessionContainer>().SetupDefaultSession();
        }
    }
}