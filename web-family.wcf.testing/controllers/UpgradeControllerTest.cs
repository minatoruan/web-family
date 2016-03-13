using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;
using web_family.core;
using web_family.web.Controllers;
using web_family.web.helpers;

namespace web_family.wcf.testing.controllers
{
    [TestClass]
    public class UpgradeControllerTest : MongodbTestBase
    {
        public UpgradeControllerTest()
        {

        }

        [TestMethod]
        public void UpdageController_OpenChannel_Successfully()
        {
            var upgraderServiceMoq = this.MockRepository.Create<IUpgradeService>();
            var upgradeController = new UpgradeController(upgraderServiceMoq.Object);
            upgradeController.Index();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdageController_OpenChannel_Unsuccessfully()
        {
            var upgraderServiceMoq = this.MockRepository.Create<IUpgradeService>();
            upgraderServiceMoq.Setup(x => x.Upgrade()).Throws<FaultException>();
            var upgradeController = new UpgradeController(upgraderServiceMoq.Object);
            upgradeController.Index();
        }
    }
}
