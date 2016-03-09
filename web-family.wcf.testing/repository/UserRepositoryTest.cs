using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Ploeh.AutoFixture;
using System;
using web_family.core.datacontracts;
using web_family.wcf.repositories;

namespace web_family.wcf.testing.repository
{
    [TestClass]
    public class UserRepositoryTest : MongodbTestBase
    {
        private IMongoCollection<User> _userCollection;
        private UserRepository _userRepository;
        private User _superuser;

        protected override void BeforeTestSuite()
        {
            base.BeforeTestSuite();
            _userCollection = GetUserCollection();
            _userRepository = new UserRepository(GetdbInstance());
        }

        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();
            _userCollection.FindOneAndDelete(x => x.Id.Equals(Infrastructure.Superuser.SuperuserId));
            _superuser = new User
            {
                Id = Infrastructure.Superuser.SuperuserId,
                Username = Infrastructure.Superuser.SuperuserAlias,
                Password = Fixture.Create<string>(),
                Fullname = Fixture.Create<string>()
            };
        }

        [TestMethod]
        public void UserRepositoryTest_ExistsAsyn_NoExist_Successfully()
        {
            Assert.IsFalse(_userCollection.Find(x => x.Id.Equals(Infrastructure.Superuser.SuperuserId)).Any());
            Assert.IsFalse(_userRepository.ExistsAsyn(Infrastructure.Superuser.SuperuserId).Result);
        }

        [TestMethod]
        public void UserRepositoryTest_ExistsAsyn_Exist_Successfully()
        {
            _userCollection.InsertOneAsync(_superuser);
            Assert.IsTrue(_userRepository.ExistsAsyn(Infrastructure.Superuser.SuperuserId).Result);
        }

        [TestMethod]
        public void UserRepositoryTest_GetByIdAsyn_Exist_Successfully()
        {
            _userCollection.InsertOneAsync(_superuser);
            Assert.IsNotNull(_userRepository.GetByIdAsyn(Infrastructure.Superuser.SuperuserId).Result);
        }

        [TestMethod]
        public void UserRepositoryTest_GetByIdAsyn_NotExist_ThrowException()
        {
            try
            {
                var user = _userRepository.GetByIdAsyn(Infrastructure.Superuser.SuperuserId).Result;
                Assert.Fail("No user appropriate returned");
            }
            catch (AggregateException ae)
            {
                Assert.AreEqual(ae.InnerException.Message, "Sequence contains no elements");
            }
        }

        [TestMethod]
        public void UserRepositoryTest_InsertOneAsync_Successfully()
        {
            _userRepository.InsertOneAsync(_superuser);
            Assert.IsTrue(_userCollection.Find(x => x.Id.Equals(Infrastructure.Superuser.SuperuserId)).Any());
        }
    }
}
