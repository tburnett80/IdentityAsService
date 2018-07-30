using System;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Managers
{
    public interface IPolicyReadManager : IDisposable
    {
        Task<GenericResult<LockoutPolicy>> GetLockoutPolicy();

        Task<GenericResult<PasswordPolicy>> GetPasswordPolicy();

        Task<GenericResult<SignInPolicy>> GetSignInPolicy();

        Task<GenericResult<UserPolicy>> GetUserPolicy();

        Task<GenericResult<UserStorePolicy>> GetUserStorePolicy();
    }
}
