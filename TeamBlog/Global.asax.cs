using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TeamBlog.Hubs;

namespace TeamBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InsertFakes();

            RunServices();
        }

        private void InsertFakes() // this is tmp
        {
            new FakesDbSeeder().InsertFakes();
        }

        private void RunServices()
        {
            var backgroundTask = new BackgroundServerTimeTimer();
        }
    }
}