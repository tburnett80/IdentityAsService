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

        #endregion

        #region ISetSignInPolicyEngine

        #endregion

        #region ISetUserPolicyEngine

        #endregion

        #region ISetUserStorePolicyEngine

        #endregion

        #region IRefreshSharedPoliciesEngine
        public async Task RefreshPolicies()
        {
            var lockout = await _policyAccessor.GetLockoutPolicy();

            lock (_policyLock)
            {
                if(SharedPolicyOptionsSingletonAccessor.PolicyOptions == null)
                    SharedPolicyOptionsSingletonAccessor.PolicyOptions = new IdentityOptions();

                SetLockoutSharedPolicy(lockout, false);
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
        #endregion
    }
}
