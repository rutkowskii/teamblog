using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBlog.Bus;
using TeamBlog.Db.Access.Commands.Notifications;
using TeamBlog.Utils;

namespace TeamBlog.NotificationsCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            //todo http://looselycoupledlabs.com/2014/06/masstransit-publish-subscribe-example/
            /*
            register for a message and write down the notifications to REDIS. 
    */
        }
    }

    public class PostCreatedEventHandler
    {
        private readonly IAddInsertPostNotificationCommandBuilder _builder;
        private readonly BaseMapper<PostCreatedEvent, AddInsertPostNotificationCommandParams> _mapper;
        private readonly IBus _bus;

        public PostCreatedEventHandler(IAddInsertPostNotificationCommandBuilder builder,
            BaseMapper<PostCreatedEvent, AddInsertPostNotificationCommandParams> mapper,
            IBus bus)
        {
            _builder = builder;
            _mapper = mapper;
            _bus = bus;
        }

        public void Handle(PostCreatedEvent ev)
        {
            var cmd = _builder.Build(_mapper.Map(ev));
            var users = cmd.Run();
            _bus.Publish(new NotificationsCreatedEvent {UserIds = users});
        }
    }
}
