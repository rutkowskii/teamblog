using System;
using System.Linq;
using Moq;
using Ninject;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Utils;

namespace TeamBlog.Tests
{
    public class TestKernel
    {
        private IKernel Kernel;
        public IKernel Instance => this.Kernel ?? (this.Kernel = new StandardKernel(new WebappModulesProvider().Get().ToArray()));

        public T Resolve<T>()
        {
            return Instance.Get<T>();
        }

        public void Override<T>(T instance)
        {
            Instance.Rebind<T>().ToConstant(instance);
        }

        public void OverrideWithMock<T>(Action<Mock<T>> mockSetup) where T : class
        {
            var mock = new Mock<T>();
            mockSetup(mock);
            Override(mock.Object);
        }

        public IMongoAdapter MongoAdapter => this.Instance.Get<IMongoAdapter>();

        public void Flush()
        {
            MongoAdapter.UserCollection.Clear(); //todo clear all should go to a separate class..
            MongoAdapter.ChannelCollection.Clear();
            MongoAdapter.PostCollection.Clear();
            MongoAdapter.ChannelPostCollection.Clear();
            Instance.Get<IRedisConnection>().FlushDb();
        }
    }
}