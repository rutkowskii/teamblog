using TeamBlog.Utils;

namespace TeamBlog.Bus
{
    public class BusIocModule : BaseIocModule
    {
        public override void Load()
        {
            BindTransient<IBus, MockBus>();
        }
    }
}