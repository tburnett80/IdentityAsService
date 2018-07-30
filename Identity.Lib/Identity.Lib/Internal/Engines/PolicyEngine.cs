using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Internal.Accessors;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Engines;
using Identity.Lib.Public.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Internal.Engines
{
    internal sealed class PolicyEngine : IRefreshSharedPoliciesEngine, ISetLockoutPolicyEngine, ISetPasswordPolicyEngine, ISetSignInPolicyEngine, ISetUserPolicyEngine, ISetUserStorePolicyEngine
    {
        #region Constructor and private members
        private readonly IPolicyAccessor _policyAccessor;
        private static object _policyLock = new object();

        public PolicyEngine(IPolicyAccessor policyAccessor)
        {
            _policyAccessor = policyAccessor
                ?? throw new ArgumentNullException(nameof(policyAccessor));
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
            var lockout = await _policyAccessor.GetLockoutPolicy();
            var password = await _policyAccessor.GetPasswordPolicy();
            var signIn = await _policyAccessor.GetSignInPolicy();
            var user = await _policyAccessor.GetUserPolicy();
            var store = await _policyAccessor.GetUserStorePolicy();

            lock (_policyLock)
            {
                if(SharedPolicyOptionsSingletonAccessor.PolicyOptions == null)
                    SharedPolicyOptionsSingletonAccessor.PolicyOptions = new IdentityOptions();

                SetLockoutSharedPolicy(lockout, false);
                SetPasswordSharedPolicy(password, false);
                SetSignInSharedPolicy(signIn, false);
                SetUserSharedPolicy(user, false);
                SetUserStoredSharedPolicy(store, false);
            }
        }

        private void SetLockoutSharedPolicy(LockoutPolicy policy, bool useLock = true)
        {
            void Updater(LockoutPolicy pol)
            {
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Lockout.AllowedForNewUsers = pol.AllowedForNewUsers;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Lockout.DefaultLockoutTimeSpan = pol.DefaultLockoutTimeSpan;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Lockout.MaxFailedAccessAttempts = pol.MaxFailedAccessAttempts;
            }

            if (useLock)
                lock (_policyLock)
                    Updater(policy);
            else
                Updater(policy);
        }

        private void SetPasswordSharedPolicy(PasswordPolicy policy, bool useLock = true)
        {
            void Updater(PasswordPolicy pol)
            {
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequiredLength = pol.RequiredLength;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequireDigit = pol.RequireDigit;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequireLowercase = pol.RequireLowercase;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequireNonAlphanumeric = pol.RequireNonAlphanumeric;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequireUppercase = pol.RequireUppercase;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Password.RequiredUniqueChars = pol.RequiredUniqueChars;
            }

            if (useLock)
                lock (_policyLock)
                    Updater(policy);
            else
                Updater(policy);
        }

        private void SetSignInSharedPolicy(SignInPolicy policy, bool useLock = true)
        {
            void Updater(SignInPolicy pol)
            {
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.SignIn.RequireConfirmedEmail = pol.RequireConfirmedEmail;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.SignIn.RequireConfirmedPhoneNumber = pol.RequireConfirmedPhoneNumber;
            }

            if (useLock)
                lock (_policyLock)
                    Updater(policy);
            else
                Updater(policy);
        }

        private void SetUserSharedPolicy(UserPolicy policy, bool useLock = true)
        {
            void Updater(UserPolicy pol)
            {
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.User.AllowedUserNameCharacters = pol.AllowedUserNameCharacters;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.User.RequireUniqueEmail = pol.RequireUniqueEmail;
            }

            if (useLock)
                lock (_policyLock)
                    Updater(policy);
            else
                Updater(policy);
        }

        private void SetUserStoredSharedPolicy(UserStorePolicy policy, bool useLock = true)
        {
            void Updater(UserStorePolicy pol)
            {
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Stores.MaxLengthForKeys = pol.MaxLengthForKeys;
                SharedPolicyOptionsSingletonAccessor.PolicyOptions.Stores.ProtectPersonalData = pol.ProtectPersonalData;
            }

            if (useLock)
                lock (_policyLock)
                    Updater(policy);
            else
                Updater(policy);
        }
        #endregion
    }
}
