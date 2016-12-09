using Ninject.Modules;
using Ninject.Extensions.Factory;

namespace TeamBlog.Utils
{
    public abstract class BaseIocModule : NinjectModule
    {
        public void BindTransient<T1, T2>() where  T2 : T1
        {
            Bind<T1>().To<T2>().InTransientScope();
        }

        public void BindSingleton<T1, T2>() where T2 : T1
        {
            Bind<T1>().To<T2>().InSingletonScope();
        }

        public void BindFactory<T>() where T : class
        {
            Bind<T>().ToFactory(() => new TypeMatchingArgumentInheritanceInstanceProvider());
        }
    }
}
