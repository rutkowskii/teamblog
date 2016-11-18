using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Modules;
using TeamBlog.Db.Access;
using TeamBlog.MongoAccess;
using TeamBlog.RedisAccess;
using TeamBlog.Services;

namespace TeamBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(
            RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

        }

        public static void RegisterRoutes(
            HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes();
        }
    }

    public class WebappModulesProvider
    {
        public IEnumerable<INinjectModule> Get()
        {
            yield return new MongoAccessIocModule();
            yield return new RedisAccessIocModule();
            yield return new CqIocModule();
            yield return new BlIocModule();
        }
    }
}