using System.Threading.Tasks;
using web_family.core.datacontracts;

namespace web_family.wcf.interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsyn(string id);

        void InsertOneAsync(User user);

        Task<bool> ExistsAsyn(string id);
    }
}