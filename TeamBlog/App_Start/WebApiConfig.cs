using Microsoft.AspNet.SignalR;
using System;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;

namespace TeamBlog
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }


    public class NotificationsHub : Hub
    {
        private Random _random;

        public NotificationsHub()
        {
            _random = new Random();
        }
    }

    public class BackgroundServerTimeTimer : IRegisteredObject
    {
        private Timer taskTimer;
        private IHubContext hub;
        private Random _random;

        public BackgroundServerTimeTimer()
        {
            HostingEnvironment.RegisterObject(this);

            hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            taskTimer = new Timer(OnTimerElapsed, null,
                TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

            _random = new Random();
        }

        private void OnTimerElapsed(object sender)
        {
            var notfsCount = _random.Next(100);
            System.Diagnostics.Trace.WriteLine($"trying to push count to client [{notfsCount}]");
            hub.Clients.All.receiveNotifications(notfsCount);
        }

        public void Stop(bool immediate)
        {
            taskTimer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}