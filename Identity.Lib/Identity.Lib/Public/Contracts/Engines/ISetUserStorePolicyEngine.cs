using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface ISetUserStorePolicyEngine
    {
        Task<GenericResult<IEnumerable<string>>> ValidateUserStorePolicyForSave(UserStorePolicy policy);

        Task<UserStorePolicy> SetUserStorePolicy(UserStorePolicy policy);
    }
}
