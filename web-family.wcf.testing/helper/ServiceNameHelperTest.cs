using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using web_family.core;
using web_family.core.helper;

namespace web_family.wcf.testing.helper
{
    [TestClass]
    public class ServiceNameHelperTest : MongodbTestBase
    {
        [TestMethod]
        public void ServiceNameHelper_GetServiceName_GetServiceName_Successful()
        {
            var name = typeof(IUpgradeService).GetServiceName();
            Assert.AreEqual("upgradeservice", name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ServiceNameHelper_GetServiceName_GetServiceName_NotSuccessful()
        {
            GetType().GetServiceName();
        }

        [TestMethod]
        public void ServiceNameHelper_IsServiceContract_Successful()
        {
            Assert.IsTrue(typeof(IUpgradeService).IsServiceContract());
            Assert.IsFalse(GetType().IsServiceContract());
        }
    }
}
