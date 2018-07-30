using System;
using System.Threading.Tasks;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Engines;
using Identity.Lib.Public.Contracts.Managers;
using Identity.Lib.Public.Models;

namespace Identity.Lib.Public.Managers
{
    public sealed class PolicyManager : IPolicyReadManager, IPolicyWriteManager
    {
        #region Constructor and private members
        private readonly IPolicyAccessor _policyAccessor;
        private readonly IRefreshSharedPoliciesEngine _refreshSharedPoliciesEngine;
        private readonly ISetLockoutPolicyEngine _setLockoutPolicyEngine;
        private readonly ISetPasswordPolicyEngine _setPasswordPolicyEngine;
        private readonly ISetSignInPolicyEngine _setSignInPolicyEngine;
        private readonly ISetUserPolicyEngine _setUserPolicyEngine;
        private readonly ISetUserStorePolicyEngine _setUserStorePolicyEngine;

        public PolicyManager(IPolicyAccessor policyAccessor, IRefreshSharedPoliciesEngine refreshSharedPoliciesEngine, 
            ISetLockoutPolicyEngine setLockoutPolicyEngine, ISetPasswordPolicyEngine setPasswordPolicyEngine, 
            ISetSignInPolicyEngine setSignInPolicyEngine, ISetUserPolicyEngine setUserPolicyEngine, 
            ISetUserStorePolicyEngine setUserStorePolicyEngine)
        {
            _policyAccessor = policyAccessor
                ?? throw new ArgumentNullException(nameof(policyAccessor));

            _refreshSharedPoliciesEngine = refreshSharedPoliciesEngine
                ?? throw new ArgumentNullException(nameof(refreshSharedPoliciesEngine));

            _setLockoutPolicyEngine = setLockoutPolicyEngine
                ?? throw new ArgumentNullException(nameof(setLockoutPolicyEngine));

            _setPasswordPolicyEngine = setPasswordPolicyEngine
                ?? throw new ArgumentNullException(nameof(setPasswordPolicyEngine));

            _setSignInPolicyEngine = setSignInPolicyEngine
                ?? throw new ArgumentNullException(nameof(setSignInPolicyEngine));

            _setUserPolicyEngine = setUserPolicyEngine
                ?? throw new ArgumentNullException(nameof(setUserPolicyEngine));

            _setUserStorePolicyEngine = setUserStorePolicyEngine
                ?? throw new ArgumentNullException(nameof(setUserStorePolicyEngine));
        }
        #endregion

        #region Read Impl
        public async Task<GenericResult<LockoutPolicy>> GetLockoutPolicy()
        {
            var result = new GenericResult<LockoutPolicy>();

            try
            {
                result.Data = await _policyAccessor.GetLockoutPolicy();
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<PasswordPolicy>> GetPasswordPolicy()
        {
            var result = new GenericResult<PasswordPolicy>();

            try
            {
                result.Data = await _policyAccessor.GetPasswordPolicy();
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<SignInPolicy>> GetSignInPolicy()
        {
            var result = new GenericResult<SignInPolicy>();

            try
            {
                result.Data = await _policyAccessor.GetSignInPolicy();
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<UserPolicy>> GetUserPolicy()
        {
            var result = new GenericResult<UserPolicy>();

            try
            {
                result.Data = await _policyAccessor.GetUserPolicy();
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<UserStorePolicy>> GetUserStorePolicy()
        {
            var result = new GenericResult<UserStorePolicy>();

            try
            {
                result.Data = await _policyAccessor.GetUserStorePolicy();
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion

        #region Write Impl
        public async Task<GenericResult<LockoutPolicy>> SetLockoutPolicy(LockoutPolicy policy)
        {
            var result = new GenericResult<LockoutPolicy>();

            try
            {
                var validationResults = await _setLockoutPolicyEngine.ValidateLockoutPolicyForSave(policy);
                if (validationResults.IsFailure)
                {
                    result.IsFailure = true;
                    result.Message = validationResults.Message;
                    return result;
                }

                result.Data = await _policyAccessor.SetLockoutPolicy(policy);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<PasswordPolicy>> SetPasswordPolicy(PasswordPolicy policy)
        {
            var result = new GenericResult<PasswordPolicy>();

            try
            {
                var validationResults = await _setPasswordPolicyEngine.ValidatePasswordPolicyForSave(policy);
                if (validationResults.IsFailure)
                {
                    result.IsFailure = true;
                    result.Message = validationResults.Message;
                    return result;
                }

                result.Data = await _setPasswordPolicyEngine.SetPasswordPolicy(policy);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<SignInPolicy>> SetSignInPolicy(SignInPolicy policy)
        {
            var result = new GenericResult<SignInPolicy>();

            try
            {
                var validationResults = await _setSignInPolicyEngine.ValidateSignInPolicyForSave(policy);
                if (validationResults.IsFailure)
                {
                    result.IsFailure = true;
                    result.Message = validationResults.Message;
                    return result;
                }

                result.Data = await _setSignInPolicyEngine.SetSignInPolicy(policy);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<UserPolicy>> SetUserPolicy(UserPolicy policy)
        {
            var result = new GenericResult<UserPolicy>();

            try
            {
                var validationResults = await _setUserPolicyEngine.ValidateUserPolicyForSave(policy);
                if (validationResults.IsFailure)
                {
                    result.IsFailure = true;
                    result.Message = validationResults.Message;
                    return result;
                }

                result.Data = await _setUserPolicyEngine.SetUserPolicy(policy);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GenericResult<UserStorePolicy>> SetUserStorePolicy(UserStorePolicy policy)
        {
            var result = new GenericResult<UserStorePolicy>();

            try
            {
                var validationResults = await _setUserStorePolicyEngine.ValidateUserStorePolicyForSave(policy);
                if (validationResults.IsFailure)
                {
                    result.IsFailure = true;
                    result.Message = validationResults.Message;
                    return result;
                }

                result.Data = await _setUserStorePolicyEngine.SetUserStorePolicy(policy);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task RefreshPolicies()
        {
            await _refreshSharedPoliciesEngine.RefreshPolicies();
        }
        #endregion
    }
}
