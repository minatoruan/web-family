using Ninject.Modules;
using web_family.wcf.interfaces;
using web_family.wcf.repositories;
using web_family.wcf.upgrades;

namespace web_family.wcf.modules
{
    public class UpgradeModule : NinjectModule
    {
        public override void Load()
        {
            // Upgrader
            Kernel.Bind<AdminInsertionUpgrade>().ToSelf().InThreadScope();         


            // Service contract
        }
    }
}