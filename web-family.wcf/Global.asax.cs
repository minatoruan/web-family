using Ninject.Web.Common;
using Ninject;
using web_family.wcf.modules;
using System.Web.Routing;
using System.ServiceModel.Activation;
using Ninject.Extensions.Wcf;
using web_family.wcf.servicecontracts;
using System;

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
            
            RouteTable.Routes.Add(new ServiceRoute("upgradeservice", 
                                    new NinjectServiceHostFactory(), 
                                    typeof(UpgradeService)));
        }
    }
}