using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Engines;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;
using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Internal.Engines
{
    internal sealed class PolicyEngine : IRefreshSharedPoliciesEngine, ISetLockoutPolicyEngine, ISetPasswordPolicyEngine, ISetSignInPolicyEngine, ISetUserPolicyEngine, ISetUserStorePolicyEngine
    {
        #region Constructor and private members
        private readonly IPolicyAccessor _policyAccessor;
        private readonly ISharedPolicyAccessor _sharedPolicyAccessor;

        public PolicyEngine(IPolicyAccessor policyAccessor, ISharedPolicyAccessor sharedPolicyAccessor)
        {
            _policyAccessor = policyAccessor
                ?? throw new ArgumentNullException(nameof(policyAccessor));

            _sharedPolicyAccessor = sharedPolicyAccessor
                ?? throw new ArgumentNullException(nameof(sharedPolicyAccessor));
        }
        #endregion

        #region ISetLockoutPolicyEngine
        public async Task<GenericResult<IEnumerable<string>>> ValidateLockoutPolicyForSave(LockoutPolicy policy)
        {
            var isNull = policy == null;
            return await Task.FromResult(new GenericResult<IEnumerable<string>>
            {
                IsFailure = isNull,
                Message = isNull
                       ? "Lockout Policy cannot be null."
                       : string.Empty,
                Data = isNull 
                       ? new [] { "Lockout Policy cannot be null." }
                       : new string[0]
            });
        }

        public async Task<LockoutPolicy> SetLockoutPolicy(LockoutPolicy policy)
        {
            var data = await _policyAccessor.SetLockoutPolicy(policy);
            SetLockoutSharedPolicy(data);
            
            return data;
        }
        #endregion

        #region ISetPasswordPolicyEngine
        public async Task<GenericResult<IEnumerable<string>>> ValidatePasswordPolicyForSave(PasswordPolicy policy)
        {
            var isNull = policy == null;
            return await Task.FromResult(new GenericResult<IEnumerable<string>>
            {
                IsFailure = isNull,
                Message = isNull
                    ? "Password Policy cannot be null."
                    : string.Empty,
                Data = isNull 
                    ? new [] { "Password Policy cannot be null." }
                    : new string[0]
            });
        }

        public async Task<PasswordPolicy> SetPasswordPolicy(PasswordPolicy policy)
        {
            var data = await _policyAccessor.SetPasswordPolicy(policy);
            SetPasswordSharedPolicy(policy);
            return data;
        }
        #endregion

        #region ISetSignInPolicyEngine
        public async Task<GenericResult<IEnumerable<string>>> ValidateSignInPolicyForSave(SignInPolicy policy)
        {
            var isNull = policy == null;
            return await Task.FromResult(new GenericResult<IEnumerable<string>>
            {
                IsFailure = isNull,
                Message = isNull
                    ? "Sign In Policy cannot be null."
                    : string.Empty,
                Data = isNull 
                    ? new [] { "Sign In Policy cannot be null." }
                    : new string[0]
            });
        }

        public async Task<SignInPolicy> SetSignInPolicy(SignInPolicy policy)
        {
            var data = await _policyAccessor.SetSignInPolicy(policy);
            SetSignInSharedPolicy(policy);
            return data;
        }
        #endregion

        #region ISetUserPolicyEngine
        public async Task<GenericResult<IEnumerable<string>>> ValidateUserPolicyForSave(UserPolicy policy)
        {
            var isNull = policy == null;
            return await Task.FromResult(new GenericResult<IEnumerable<string>>
            {
                IsFailure = isNull,
                Message = isNull
                    ? "User Policy cannot be null."
                    : string.Empty,
                Data = isNull 
                    ? new [] { "User Policy cannot be null." }
                    : new string[0]
            });
        }

        public async Task<UserPolicy> SetUserPolicy(UserPolicy policy)
        {
            var data = await _policyAccessor.SetUserPolicy(policy);
            SetUserSharedPolicy(policy);
            return data;
        }
        #endregion

        #region ISetUserStorePolicyEngine
        public async Task<GenericResult<IEnumerable<string>>> ValidateUserStorePolicyForSave(UserStorePolicy policy)
        {
            var isNull = policy == null;
            return await Task.FromResult(new GenericResult<IEnumerable<string>>
            {
                IsFailure = isNull,
                Message = isNull
                    ? "User Store Policy cannot be null."
                    : string.Empty,
                Data = isNull 
                    ? new [] { "User Store Policy cannot be null." }
                    : new string[0]
            });
        }

        public async Task<UserStorePolicy> SetUserStorePolicy(UserStorePolicy policy)
        {
            var data = await _policyAccessor.SetUserStorePolicy(policy);
            SetUserStoredSharedPolicy(policy);
            return data;
        }
        #endregion

        #region IRefreshSharedPoliciesEngine
        public async Task RefreshPolicies()
        {
            var shared = _sharedPolicyAccessor.GetPolicy();

            await _policyAccessor.GetLockoutPolicy()
                .ContinueWith(async tsk => SetLockoutSharedPolicy(await tsk, shared.Lockout));

            await _policyAccessor.GetPasswordPolicy()
                .ContinueWith(async tsk => SetPasswordSharedPolicy(await tsk, shared.Password));

            await _policyAccessor.GetSignInPolicy()
                .ContinueWith(async tsk => SetSignInSharedPolicy(await tsk, shared.SignIn));

            await _policyAccessor.GetUserPolicy()
                .ContinueWith(async tsk => SetUserSharedPolicy(await tsk, shared.User));

            await _policyAccessor.GetUserStorePolicy()
                .ContinueWith(async tsk => SetUserStoredSharedPolicy(await tsk, shared.Stores));
        }

        private void SetLockoutSharedPolicy(LockoutPolicy policy, LockoutOptions shared = null)
        {
            if (shared == null)
                shared = _sharedPolicyAccessor.GetPolicy().Lockout;

            shared.AllowedForNewUsers = policy.AllowedForNewUsers;
            shared.DefaultLockoutTimeSpan = policy.DefaultLockoutTimeSpan;
            shared.MaxFailedAccessAttempts = policy.MaxFailedAccessAttempts;

            _sharedPolicyAccessor.SetLockoutPolicy(shared);
        }

        private void SetPasswordSharedPolicy(PasswordPolicy policy, PasswordOptions shared = null)
        {
            if (shared == null)
                shared = _sharedPolicyAccessor.GetPolicy().Password;

            shared.RequiredLength = policy.RequiredLength;
            shared.RequireDigit = policy.RequireDigit;
            shared.RequireLowercase = policy.RequireLowercase;
            shared.RequireNonAlphanumeric = policy.RequireNonAlphanumeric;
            shared.RequireUppercase = policy.RequireUppercase;
            shared.RequiredUniqueChars = policy.RequiredUniqueChars;

            _sharedPolicyAccessor.SetPasswordPolicy(shared);
        }

        private void SetSignInSharedPolicy(SignInPolicy policy, SignInOptions shared = null)
        {
            if (shared == null)
                shared = _sharedPolicyAccessor.GetPolicy().SignIn;

            shared.RequireConfirmedEmail = policy.RequireConfirmedEmail;
            shared.RequireConfirmedPhoneNumber = policy.RequireConfirmedPhoneNumber;

            _sharedPolicyAccessor.SetSignInPolicy(shared);
        }

        private void SetUserSharedPolicy(UserPolicy policy, UserOptions shared = null)
        {
            if (shared == null)
                shared = _sharedPolicyAccessor.GetPolicy().User;

            shared.AllowedUserNameCharacters = policy.AllowedUserNameCharacters;
            shared.RequireUniqueEmail = policy.RequireUniqueEmail;

            _sharedPolicyAccessor.SetUserPolicy(shared);
        }

        private void SetUserStoredSharedPolicy(UserStorePolicy policy, StoreOptions shared = null)
        {
            if (shared == null)
                shared = _sharedPolicyAccessor.GetPolicy().Stores;

            shared.MaxLengthForKeys = policy.MaxLengthForKeys;
            shared.ProtectPersonalData = policy.ProtectPersonalData;
            
            _sharedPolicyAccessor.SetUserStorePolicy(shared);
        }
        #endregion
    }
}
