using Machine.Specifications;
using MongoDB.Driver;
using Ninject;
using TeamBlog.Model;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Tests
{
    public class TestTearDown : ICleanupAfterEveryContextInAssembly
    {
        public void AfterContextCleanup()
        {
            TestKernel.MongoAdapter.ChannelCollection.Clear();
            TestKernel.MongoAdapter.PostCollection.Clear();
            TestKernel.MongoAdapter.ChannelPostCollection.Clear();
            TestKernel.Instance.Get<IRedisConnection>().Flush();
        }
    }
}