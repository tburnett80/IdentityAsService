using System.Threading.Tasks;
using Identity.Lib.Public.Models;

namespace Identity.Lib.Public.Contracts.Managers
{
    public interface IPolicyWriteManager
    {
        Task<GenericResult<LockoutPolicy>> SetLockoutPolicy(LockoutPolicy policy);

        Task<GenericResult<PasswordPolicy>> SetPasswordPolicy(PasswordPolicy policy);

        Task<GenericResult<SignInPolicy>> SetSignInPolicy(SignInPolicy policy);

        Task<GenericResult<UserPolicy>> SetUserPolicy(UserPolicy policy);

        Task<GenericResult<UserStorePolicy>> SetUserStorePolicy(UserStorePolicy policy);

        Task RefreshPolicies();
    }
}
