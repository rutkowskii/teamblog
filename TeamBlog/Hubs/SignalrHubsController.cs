using System;
using System.Threading;
using System.Web.Hosting;
using Microsoft.AspNet.SignalR;
using TeamBlog.Bus;

namespace TeamBlog.Hubs
{
    public class SignalrHubsController : IRegisteredObject
    {
        private Timer taskTimer;
        private IHubContext hub;
        private Random _random;

        public SignalrHubsController()
        {
            HostingEnvironment.RegisterObject(this);

            this.hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            this.taskTimer = new Timer(this.OnTimerElapsed, null,
                TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

            this._random = new Random();

            // todo sth like that? http://stackoverflow.com/questions/32698757/storing-the-connection-id-of-a-specific-client-in-signalr-hub
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

    public interface ISignalRController
    {
        void PushToUser<T>(Guid userId, T message);
    }

    public class NotificationsCreatedEventHandler
    {
        private readonly ISignalRController _controller;

        public NotificationsCreatedEventHandler(ISignalRController controller)
        {
            _controller = controller;
        }

        public void Handle(NotificationsCreatedEvent notificationsCreatedEvent)
        {
            //todo sth like _controller.PushToUser();
        }
    }
}