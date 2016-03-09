using System;
using System.ServiceModel;
using System.Web.Mvc;
using web_family.core;

namespace web_family.web.Controllers
{
    public class UpgradeController : Controller
    {
        private const string _urlparttern = "http://localhost:8082/upgradeservice";

        private IUpgradeService _upgradeService;

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }
        // GET: Upgrade
        public ActionResult Index()
        {
            //_upgradeService.Upgrade();
            ChannelFactory<IUpgradeService>.CreateChannel(new BasicHttpBinding(), GetCurrentUriService("")).Upgrade();
            return View();
        }

        private EndpointAddress GetCurrentUriService(string servicename)
        {
            return new EndpointAddress(new Uri(_urlparttern));
        }
    }
}