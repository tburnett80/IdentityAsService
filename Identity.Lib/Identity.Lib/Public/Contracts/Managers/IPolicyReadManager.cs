﻿using System.Threading.Tasks;
using Identity.Lib.Public.Models;

namespace Identity.Lib.Public.Contracts.Managers
{
    public interface IPolicyReadManager
    {
        Task<GenericResult<LockoutPolicy>> GetLockoutPolicy();

        Task<GenericResult<PasswordPolicy>> GetPasswordPolicy();

        Task<GenericResult<SignInPolicy>> GetSignInPolicy();

        Task<GenericResult<UserPolicy>> GetUserPolicy();

        Task<GenericResult<UserStorePolicy>> GetUserStorePolicy();
    }
}
