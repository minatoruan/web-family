using MongoDB.Bson.Serialization;
using web_family.core.datacontracts;

namespace web_family.wcf
{
    public static class Infrastructure
    {
        public static class Superuser
        {
            public const string SuperuserId = "superuser";
            public const string SuperuserAlias = "superuser";
        }

        public static class Collections
        {
            public const string Users = "users";
        }

        public static void SetupClassMap()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.MapMember(x => x.Fullname).SetElementName("fullname");
                cm.MapMember(x => x.Password).SetElementName("password");
                cm.MapMember(x => x.Username).SetElementName("username");
            });
        }
    }
}