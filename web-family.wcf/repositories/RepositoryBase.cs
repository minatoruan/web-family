using MongoDB.Driver;
using System.Threading.Tasks;
using web_family.core.datacontracts;

namespace web_family.wcf.repositories
{
    public class RepositoryBase<T> where T : EntityBase
    {
        private string _name;
        private IMongoDatabase _mongodb;

        public RepositoryBase(IMongoDatabase mongodb, string name)
        {
            this._name = name;
            this._mongodb = mongodb;
        }

        protected IMongoCollection<T> GetCollections()
        {
            return _mongodb.GetCollection<T>(this._name);
        }

        public void InsertOneAsync(T entity)
        {
            GetCollections().InsertOneAsync(entity);
        }

        public async Task<T> GetByIdAsyn(string id)
        {
            return await GetCollections().Find(x => x.Id.Equals(id)).Limit(1).SingleAsync();
        }

        public async Task<bool> ExistsAsyn(string id)
        {
            return await GetCollections().Find(x => x.Id.Equals(id)).Limit(1).AnyAsync();
        }
    }
}