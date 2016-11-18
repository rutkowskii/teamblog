using System.Linq;
using Ninject;
using TeamBlog.MongoAccess;

namespace TeamBlog.Tests
{
    public class TestKernel
    {
        private static IKernel Kernel;
        public static IKernel Instance
        {
            get
            {
                if (Kernel == null)
                {
                    Kernel = new StandardKernel(new WebappModulesProvider().Get().ToArray());
                }
                return Kernel;
            }
        }

        public static IMongoAdapter MongoAdapter
        {
            get { return Instance.Get<IMongoAdapter>(); }
        }
}