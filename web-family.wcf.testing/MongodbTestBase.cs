using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Moq;
using MongoDB.Driver;
using System.Configuration;
using web_family.core.datacontracts;

namespace web_family.wcf.testing
{
    [TestClass]
    public abstract class MongodbTestBase : IDisposable
    {
        private static Fixture _fixure = new Fixture();
        private static IMongoDatabase _mongodb = GetdbInstance();
        private MockRepository _mockRepository;


        public MongodbTestBase()
        {
            ClassInitialize();
        }

        protected static IMongoDatabase GetdbInstance()
        {
            var url = new MongoUrl(ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString);
            var mongoclient = new MongoClient(url);
            var server = mongoclient.GetDatabase(url.DatabaseName);
            return server;
        }

        private IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongodb.GetCollection<T>(name);
        }

        protected IMongoCollection<User> GetUserCollection()
        {
            return GetCollection<User>(Infrastructure.Collections.Users);
        }

        protected Fixture Fixture
        {
            get { return _fixure; }
        }

        protected MockRepository MockRepository
        {
            get
            {
                if (_mockRepository == null)
                {
                    _mockRepository = new MockRepository(MockBehavior.Default);
                }
                return _mockRepository;
            }
        }

        public void ClassInitialize()
        {
            BeforeTestSuite();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            BeforeEachTest();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            AfterEachTest();
        }

        public void Dispose()
        {
            AfterTestSuite();
        }

        protected virtual void BeforeTestSuite()
        {

        }

        protected virtual void BeforeEachTest()
        {

        }

        protected virtual void AfterEachTest()
        {

        }

        protected virtual void AfterTestSuite()
        {

        }
    }
}
