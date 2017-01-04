using System;
using TeamBlog.Utils;

namespace TeamBlog.Bus
{
    public interface IBus
    {
        void Publish<IMessage>(IMessage message);
    }

    public class MockBus : IBus
    {
        public void Publish<IMessage1>(IMessage1 message)
        {
            //do nothing..
        }
    }

    public class PostCreatedEvent: IMessage
    {
        public Guid[] ChannelIds { get; set; }
    }

    public class BusIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IBus, MockBus>();
        }
    }
}