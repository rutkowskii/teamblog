namespace TeamBlog.Bus
{
    public interface IBus
    {
        void Publish<IMessage>(IMessage message);
    }
}