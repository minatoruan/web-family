using Ninject.Web.Common;
using Ninject;
using web_family.wcf.modules;
using System.Web.Routing;
using System.ServiceModel.Activation;
using Ninject.Extensions.Wcf;
using web_family.wcf.servicecontracts;
using web_family.core.helper;
using System.Linq;

namespace web_family.wcf
{
    public class Global : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            RegisterRoutes();
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new UpgradeModule(), 
                                                new ServiceContractModule(), 
                                                new RepositoryModule());
        }

        private void RegisterRoutes()
        {
            foreach (var type in typeof(UpgradeService).Assembly.GetTypes())
            {
                foreach (var serviceInterface in type.GetInterfaces().Where(x => x.IsServiceContract()))
                {
                    RouteTable.Routes.Add(new ServiceRoute(serviceInterface.GetServiceName(),
                        new NinjectServiceHostFactory(),
                        type));
                }
            }
        }
    }
}