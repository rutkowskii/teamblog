using System;
using System.Threading;
using System.Web.Hosting;
using Microsoft.AspNet.SignalR;

namespace TeamBlog.Hubs
{
    public class BackgroundServerTimeTimer : IRegisteredObject
    {
        private Timer taskTimer;
        private IHubContext hub;
        private Random _random;

        public BackgroundServerTimeTimer()
        {
            HostingEnvironment.RegisterObject(this);

            this.hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            this.taskTimer = new Timer(this.OnTimerElapsed, null,
                TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

            this._random = new Random();
        }

        private void OnTimerElapsed(object sender)
        {
            var notfsCount = this._random.Next(100);
            System.Diagnostics.Trace.WriteLine($"trying to push count to client [{notfsCount}]");
            this.hub.Clients.All.receiveNotifications(notfsCount);
        }

        public void Stop(bool immediate)
        {
            this.taskTimer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}