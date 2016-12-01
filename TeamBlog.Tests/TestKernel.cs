using System.Linq;
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

        public IMongoAdapter MongoAdapter => this.Instance.Get<IMongoAdapter>();

        public void Flush()
        {
            MongoAdapter.ChannelCollection.Clear();
            MongoAdapter.PostCollection.Clear();
            MongoAdapter.ChannelPostCollection.Clear();
            Instance.Get<IRedisConnection>().Flush();
        }
    }
}