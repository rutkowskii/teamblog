using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            var hc = new HubConfiguration { EnableDetailedErrors = true, };
            app.MapSignalR("/signalr", hc);
        }
    }
}
