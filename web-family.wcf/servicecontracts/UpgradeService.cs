using Ninject;
using web_family.core;
using web_family.wcf.upgrades;

namespace web_family.wcf.servicecontracts
{
    public class UpgradeService : IUpgradeService
    {
        private IKernel _kernel;
        public UpgradeService(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Upgrade()
        {
            _kernel.Get<AdminInsertionUpgrade>().Upgrade();
        }
    }
}