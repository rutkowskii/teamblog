using MongoDB.Driver;
using Ninject;
using TeamBlog.App_Start;
using TeamBlog.Bl;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.Dtos;
using TeamBlog.Model;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog
{
    public class FakesDbSeeder
    {
        public void InsertFakes()
        {
            var K = NinjectWebCommon.Kernel;
            K.Get<IMongoAdapter>().ChannelCollection.Clear();
            K.Get<IMongoAdapter>().UserCollection.Clear();
            K.Get<IMongoAdapter>().PostCollection.Clear();
            K.Get<IMongoAdapter>().ChannelPostCollection.Clear();
            K.Get<IRedisConnection>().FlushDb();

            var userid = K.Get<ISessionProvider>().UserId;
            K.Get<IMongoAdapter>().UserCollection.InsertOne(new User { Id = userid, Name = "James Doe"});

            K.Get<ICreateChannelCommandBuilder>().Build("śmieszki").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("dev general").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("programming").Run();

            var channelId = K.Get<IMongoAdapter>().ChannelCollection.AsQueryable().First().Id;

            K.Get<IUser>().SubscribeToChannel(channelId);
            K.Get<IUser>().AddPost(new NewPostDto { Channels = new[] { channelId }, Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw", Title = "enbie" });
            K.Get<IUser>().AddPost(new NewPostDto { Channels = new[] { channelId }, Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw", Title = "enbie" });
        }
    }
}