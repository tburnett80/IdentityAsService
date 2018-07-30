using Identity.Lib.Public.Contracts.Accessors;
using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Tests.TestImplimentations
{
    public class TestSharedPolicyAccessor : ISharedPolicyAccessor
    {
        internal IdentityOptions _opts { get; set; }
        private static readonly object _lock = new object();

        public IdentityOptions GetPolicy()
        {
            lock (_lock)
            {
                return _opts ??
                       (_opts = new IdentityOptions());
            }
        }

        public void SetLockoutPolicy(LockoutOptions opts)
        {
            lock (_lock)
            {
                _opts.Lockout = opts;
            }
        }

        public void SetPasswordPolicy(PasswordOptions opts)
        {
            lock (_lock)
            {
                _opts.Password = opts;
            }
        }

        public void SetSignInPolicy(SignInOptions opts)
        {
            lock (_lock)
            {
                _opts.SignIn = opts;
            }
        }

        public void SetUserPolicy(UserOptions opts)
        {
            lock (_lock)
            {
                _opts.User = opts;
            }
        }

        public void SetUserStorePolicy(StoreOptions opts)
        {
            lock (_lock)
            {
                _opts.Stores = opts;
            }
        }
    }
}
