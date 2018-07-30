using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Public.Contracts.Accessors
{
    public interface ISharedPolicyAccessor
    {
        IdentityOptions GetPolicy();

        void SetLockoutPolicy(LockoutOptions opts);

        void SetPasswordPolicy(PasswordOptions opts);

        void SetSignInPolicy(SignInOptions opts);

        void SetUserPolicy(UserOptions opts);

        void SetUserStorePolicy(StoreOptions opts);
    }
}
