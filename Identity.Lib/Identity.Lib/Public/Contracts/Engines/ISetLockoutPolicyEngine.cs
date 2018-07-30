using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface ISetLockoutPolicyEngine
    {
        Task<GenericResult<IEnumerable<string>>> ValidateLockoutPolicyForSave(LockoutPolicy policy);

        Task<LockoutPolicy> SetLockoutPolicy(LockoutPolicy policy);
    }
}
