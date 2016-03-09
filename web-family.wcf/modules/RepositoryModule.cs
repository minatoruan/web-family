using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Ninject.Modules;
using System.Configuration;
using web_family.core.datacontracts;
using web_family.wcf.interfaces;
using web_family.wcf.repositories;

namespace web_family.wcf.modules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMongoDatabase>().ToMethod(x =>
            {
                var url = new MongoUrl(ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString);
                var mongoclient = new MongoClient(url);
                var server = mongoclient.GetDatabase(url.DatabaseName);
                return server;
            });
            
            Kernel.Bind<IUserRepository>().To<UserRepository>().InThreadScope();
        }
    }
}