using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using System.Threading.Tasks;
using web_family.core.datacontracts;
using web_family.wcf.interfaces;
using web_family.wcf.upgrades;

namespace web_family.wcf.testing.upgrades
{
    [TestClass]
    public class AdminInsertionUpgradeTest : MongodbTestBase
    {
        public AdminInsertionUpgradeTest()
        {
        }

        [TestMethod]
        public void UserUpgradeTest_RunUpgradeToAddSuperuser_Successfully()
        {
            var userRepository = MockRepository.Create<IUserRepository>();
            userRepository.Setup(x => x.GetByIdAsyn("superuser")).Returns<Task<User>>(null);

            var userUpgrade = new AdminInsertionUpgrade(userRepository.Object);
            userUpgrade.Upgrade();

            userRepository.Verify(x => x.InsertOneAsync(It.Is<User>(user => user.Id.Equals("superuser") && user.Username.Equals("superuser"))));
        }

        [TestMethod]
        public void UserUpgradeTest_SuperuserExisted_Successfully()
        {
            var user = Fixture.Build<User>()
                .With(x => x.Id, "superuser")
                .With(x => x.Username, "superuser")
                .Create();

            var userRepository = MockRepository.Create<IUserRepository>();
            userRepository.Setup(x => x.ExistsAsyn("superuser"))
                .Returns(Task.FromResult(true));

            var userUpgrade = new AdminInsertionUpgrade(userRepository.Object);
            userUpgrade.Upgrade();

            userRepository.Verify();
        }
    }
}
