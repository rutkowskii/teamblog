using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TeamBlog;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(SignalRStartup))]

namespace TeamBlog
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            var hc = new HubConfiguration {EnableDetailedErrors = true,};
            app.MapSignalR("/signalr", hc);


            app.Map("/api", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    SigningCertificate = LoadCertificate(),
                    RequireSsl = false,

                    Factory = new IdentityServerServiceFactory()
                                .UseInMemoryUsers(Users.Get())
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(StandardScopes.All)
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44339/api",
                ClientId = "mvc",
                RedirectUri = "https://localhost:44339/",
                ResponseType = "id_token",

                SignInAsAuthenticationType = "Cookies"
            });
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\idsrv3test.pfx", "idsrv3test");
        }
    }
}