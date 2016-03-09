using MongoDB.Driver;
using web_family.wcf.interfaces;
using web_family.core.datacontracts;

namespace web_family.wcf.repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase mongodb)
            : base(mongodb, Infrastructure.Collections.Users)
        {
        }
    }
}