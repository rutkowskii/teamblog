namespace TeamBlog.Bus
{
    public class MockBus : IBus
    {
        public void Publish<IMessage1>(IMessage1 message)
        {
            //do nothing..
        }
    }
}