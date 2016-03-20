using Machine.Specifications;
using MongoDB.Driver;
using TeamBlog.Model;
using TeamBlog.Utils;

namespace TeamBlog.Tests
{
    public class TestTearDown : ICleanupAfterEveryContextInAssembly
    {
        public void AfterContextCleanup()
        {
            TestKernel.Adapter.ChannelCollection.Clear();
            TestKernel.Adapter.PostCollection.Clear();
            TestKernel.Adapter.ChannelPostCollection.Clear();
        }
    }
}