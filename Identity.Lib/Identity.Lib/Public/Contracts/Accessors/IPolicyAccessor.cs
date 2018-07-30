using System.Threading.Tasks;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Accessors
{
    public interface IPolicyAccessor
    {
        Task<LockoutPolicy> GetLockoutPolicy();

        Task<PasswordPolicy> GetPasswordPolicy();

        Task<SignInPolicy> GetSignInPolicy();

        Task<UserPolicy> GetUserPolicy();

        Task<UserStorePolicy> GetUserStorePolicy();

        Task<LockoutPolicy> SetLockoutPolicy(LockoutPolicy policy);

        Task<PasswordPolicy> SetPasswordPolicy(PasswordPolicy policy);

        Task<SignInPolicy> SetSignInPolicy(SignInPolicy policy);

        Task<UserPolicy> SetUserPolicy(UserPolicy policy);

        Task<UserStorePolicy> SetUserStorePolicy(UserStorePolicy policy);
    }
}
