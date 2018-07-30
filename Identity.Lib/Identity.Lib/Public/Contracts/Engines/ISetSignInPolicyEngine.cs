using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface ISetSignInPolicyEngine
    {
        Task<GenericResult<IEnumerable<string>>> ValidateSignInPolicyForSave(SignInPolicy policy);

        Task<SignInPolicy> SetSignInPolicy(SignInPolicy policy);
    }
}
