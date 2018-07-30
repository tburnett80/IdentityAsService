using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface ISetUserPolicyEngine
    {
        Task<GenericResult<IEnumerable<string>>> ValidateUserPolicyForSave(UserPolicy policy);

        Task<UserPolicy> SetUserPolicy(UserPolicy policy);
    }
}
