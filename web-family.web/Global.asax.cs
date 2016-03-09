using Ninject.Web.Common;
using Ninject;
using web_family.core.client;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Reflection;
using web_family.web.App_Start;

namespace web_family.web
{
    public class Global : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new ServiceClientModule());
            kernel.Load(Assembly.GetExecutingAssembly());

            var httpResolver = new NinjectHttpDependencyResolver(kernel);
            DependencyResolver.SetResolver(httpResolver);
            return kernel;
        }
    }
}