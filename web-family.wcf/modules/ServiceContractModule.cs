using Ninject.Modules;
using web_family.core;
using web_family.wcf.servicecontracts;

namespace web_family.wcf.modules
{
    public class ServiceContractModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUpgradeService>().To<UpgradeService>().InThreadScope();

        }
    }
}