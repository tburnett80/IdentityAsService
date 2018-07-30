using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Identity;

namespace Identity.Lib.Public.Contracts.Accessors
{
    public interface IIdentityAccessor
    {
        Task<GenericResult<IEnumerable<string>>> CreateUser(User usr);

        Task<User> GetUserByUserName(string usrName);
    }
}
