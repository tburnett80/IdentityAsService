using Identity.Lib.Public.Contracts.Accessors;
using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Internal.IdentityWrappers
{
    internal sealed class SharedPolicyAccessor : ISharedPolicyAccessor
    {
        internal static IdentityOptions PolicyOptions { get; set; }
        private static readonly object _lock = new object();

        public IdentityOptions GetPolicy()
        {
            lock (_lock)
            {
                return PolicyOptions ??
                       (PolicyOptions = new IdentityOptions());
            }
        }

        public void SetLockoutPolicy(LockoutOptions opts)
        {
            lock (_lock)
            {
                PolicyOptions.Lockout = opts;
            }
        }

        public void SetPasswordPolicy(PasswordOptions opts)
        {
            lock (_lock)
            {
                PolicyOptions.Password = opts;
            }
        }

        public void SetSignInPolicy(SignInOptions opts)
        {
            lock (_lock)
            {
                PolicyOptions.SignIn = opts;
            }
        }

        public void SetUserPolicy(UserOptions opts)
        {
            lock (_lock)
            {
                PolicyOptions.User = opts;
            }
        }

        public void SetUserStorePolicy(StoreOptions opts)
        {
            lock (_lock)
            {
                PolicyOptions.Stores = opts;
            }
        }
    }
}
