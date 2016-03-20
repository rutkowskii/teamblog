using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace TeamBlog.Utils
{
    public abstract class BaseIocModule : NinjectModule
    {
        public void BindTransient<T1, T2>() where  T2 : T1
        {
            Bind<T1>().To<T2>().InTransientScope();
        }
    }
}
