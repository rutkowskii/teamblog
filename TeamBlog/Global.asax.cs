using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using MongoDB.Driver;
using Ninject;
using TeamBlog.App_Start;
using TeamBlog.Db.Access.Commands.Channels;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Bl;
using TeamBlog.Dtos;
using TeamBlog.Hubs;
using TeamBlog.Utils;

namespace TeamBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InsertFakes();

            RunServices();
        }

        private void InsertFakes() //todo tmp
        {
            var K =  NinjectWebCommon.Kernel;
            K.Get<IMongoAdapter>().ChannelCollection.Clear();
            K.Get<IMongoAdapter>().PostCollection.Clear();
            K.Get<IMongoAdapter>().ChannelPostCollection.Clear();
            K.Get<IRedisConnection>().FlushDb();

            K.Rebind<IDateTimeProvider>().To<DateTimeProvider>().InTransientScope(); //todo tmp. 


            K.Get<ICreateChannelCommandBuilder>().Build("śmieszki").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("dev general").Run();
            K.Get<ICreateChannelCommandBuilder>().Build("programming").Run();

            var channelId = K.Get<IMongoAdapter>().ChannelCollection.AsQueryable().First().Id;

            K.Get<IUser>().SubscribeToChannel(channelId);
            K.Get<IUser>().AddPost(new NewPostDto {Channels = new [] {channelId}, Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw", Title = "enbie"});
            K.Get<IUser>().AddPost(new NewPostDto {Channels = new [] {channelId}, Content = "efbufebuebvebuevbuevoiep evwbivwonbiv ehiweovinhodiw", Title = "enbie"});
        }

        private void RunServices()
        {
            var backgroundTask = new BackgroundServerTimeTimer();
        }

    }

    static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
        {
            new Scope
            {
                Name = "api1"
            }
        };
        }
    }

    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
            new Client
            {
                Enabled = true,
                ClientName = "JS Client",
                ClientId = "js",
                Flow = Flows.Implicit,

                RedirectUris = new List<string>
                {
                    "http://localhost:56522"
                },

                AllowedCorsOrigins = new List<string>
                {
                    "http://localhost:56522"
                },

                AllowAccessToAllScopes = true
            }
        };
        }
    }

    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
        {
            new InMemoryUser
            {
                Username = "bob",
                Password = "secret",
                Subject = "1",

                Claims = new[]
                {
                    new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                    new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                    new Claim(Constants.ClaimTypes.Email, "bob.smith@email.com")
                }
            }
        };
        }
    }

}
