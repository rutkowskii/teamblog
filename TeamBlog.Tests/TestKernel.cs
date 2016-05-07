using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Ninject;
using Ninject.Modules;
using TeamBlog.Db.Access;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;

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
                    Kernel = new StandardKernel(Modules.ToArray());
                }
                return Kernel;
            }
        }

        public static IMongoAdapter MongoAdapter
        {
            get { return Instance.Get<IMongoAdapter>(); }
        }

        private static IEnumerable<INinjectModule> Modules
        {
            get
            {
                yield return new MongoAccessIocModule();
                yield return new RedisAccessIocModule();
                yield return new CqIocModule();
            }
        } 
    }
}