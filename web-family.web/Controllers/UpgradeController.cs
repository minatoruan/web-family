using System.Web.Mvc;
using web_family.core;

namespace web_family.web.Controllers
{
    public class UpgradeController : BaseController
    {
        private IUpgradeService _upgradeService;

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        // GET: Upgrade
        public ActionResult Index()
        {
            _upgradeService.Upgrade();
            return View();
        }
    }
}