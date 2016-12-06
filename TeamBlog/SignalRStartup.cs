using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TeamBlog;

[assembly: OwinStartup(typeof(SignalRStartup))]
namespace TeamBlog
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            var hc = new HubConfiguration { EnableDetailedErrors = true, };
            app.MapSignalR("/signalr", hc);
        }
    }
}
