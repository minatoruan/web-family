using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.IO;
using System.IO;
using MongoDB.Bson.Serialization;
using web_family.core.datacontracts;
using web_family.wcf.modules;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace web_family.wcf.testing.datacontracts
{
    /// <summary>
    /// Summary description for UserTest
    /// </summary>
    [TestClass]
    public class UserTest : MongodbTestBase
    {
        public UserTest()
        {
        }

        protected override void BeforeTestSuite()
        {
            base.BeforeTestSuite();
        }

        private void ClearClassMap<T>()
        {
            var cm = BsonClassMap.GetRegisteredClassMaps().FirstOrDefault();
            if (cm == null) return;
            var fi = typeof(BsonClassMap).GetField("__classMaps", BindingFlags.Static | BindingFlags.NonPublic);
            var classMaps = (Dictionary<Type, BsonClassMap>) fi.GetValue(cm);
            classMaps.Remove(typeof(T));
        }

        [TestMethod]
        public void User_TestLowerCaseAllFields_Success()
        {
            ClearClassMap<User>();
            Infrastructure.SetupClassMap();
            var classMap = BsonClassMap.LookupClassMap(typeof(User));

            foreach (var element in classMap.DeclaredMemberMaps)
            {
                Assert.AreEqual(element.MemberName.ToLower(), element.ElementName);
            }
            
        }
    }
}
