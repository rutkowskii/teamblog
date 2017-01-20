using System.Collections.Generic;
using System.Web.Http;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TeamBlog;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(SignalRStartup))]

namespace TeamBlog
{
    public class SignalRStartup
    {
        public void Configuration(
            IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            var hc = new HubConfiguration {EnableDetailedErrors = true,};
            app.MapSignalR("/signalr", hc);

            ConfigureIdentityServer(app);
            ConfigureAccess(app);
        }

        private static void ConfigureIdentityServer(
            IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                LoggingOptions = new LoggingOptions
                {
                    WebApiDiagnosticsIsVerbose = true,
                    EnableWebApiDiagnostics = true,
                    EnableHttpLogging = true,
                    EnableKatanaLogging= true
                },
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get()),
                RequireSsl = false,
                EnableWelcomePage = false,
            };

            app.UseIdentityServer(options);
        }

        private void ConfigureAccess(IAppBuilder app)
        {
            // accept access tokens from identityserver and require a scope of 'api1'

            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = "http://localhost:56522",
            //    ValidationMode = ValidationMode.Both,
            //    RequiredScopes = new[] { "api1" }
            //});

            // configure web api
            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            //config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}