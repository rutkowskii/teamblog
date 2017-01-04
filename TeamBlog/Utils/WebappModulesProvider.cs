using System.Collections.Generic;
using Ninject.Modules;
using TeamBlog.Db.Access;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Bl;
using TeamBlog.Bus;

namespace TeamBlog.Utils
{
    public class WebappModulesProvider
    {
        public IEnumerable<INinjectModule> Get()
        {
            yield return new MongoAccessIocModule();
            yield return new RedisAccessIocModule();
            yield return new BusIocModule();
            yield return new CqIocModule();
            yield return new BlIocModule();
        }
    }
}