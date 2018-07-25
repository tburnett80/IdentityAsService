using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Lib.Internal.StoreProviders;
using Identity.Lib.Internal.StoreProviders.EF.SqlServer;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Lib.Internal.Accessors
{
    internal class PolicyAccessor : IPolicyAccessor
    {
        #region Constructor and Private members
        private readonly DbContextOptions _options;

        public PolicyAccessor(DbContextOptions options)
        {
            _options = options
                ?? throw new ArgumentNullException(nameof(options));
        }
        #endregion

        public async Task<LockoutPolicy> GetLockoutPolicy()
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.LockoutPolicies
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                return ent.ToModel();
            }
        }

        public async Task<PasswordPolicy> GetPasswordPolicy()
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.PasswordPolicies
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                return ent.ToModel();
            }
        }

        public async Task<SignInPolicy> GetSignInPolicy()
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.SignInPolicies
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                return ent.ToModel();
            }
        }

        public async Task<UserPolicy> GetUserPolicy()
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.UserPolicies
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                return ent.ToModel();
            }
        }

        public async Task<UserStorePolicy> GetUserStorePolicy()
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.UserStorePolicies
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();

                return ent.ToModel();
            }
        }

        public async Task<LockoutPolicy> SetLockoutPolicy(LockoutPolicy policy)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.LockoutPolicies.AddAsync(policy.ToEntity());
                var cnt = await ctx.SaveChangesAsync();

                if(cnt < 1 || cnt > 2)
                    throw new Exception("There was an error updating the Lockout Policy record.");

                return ent.Entity.ToModel();
            }
        }

        public async Task<PasswordPolicy> SetPasswordPolicy(PasswordPolicy policy)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.PasswordPolicies.AddAsync(policy.ToEntity());
                var cnt = await ctx.SaveChangesAsync();

                if(cnt < 1 || cnt > 2)
                    throw new Exception("There was an error updating the Password Policy record.");

                return ent.Entity.ToModel();
            }
        }

        public async Task<SignInPolicy> SetSignInPolicy(SignInPolicy policy)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.SignInPolicies.AddAsync(policy.ToEntity());
                var cnt = await ctx.SaveChangesAsync();

                if(cnt < 1 || cnt > 2)
                    throw new Exception("There was an error updating the Sign In Policy record.");

                return ent.Entity.ToModel();
            }
        }

        public async Task<UserPolicy> SetUserPolicy(UserPolicy policy)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.UserPolicies.AddAsync(policy.ToEntity());
                var cnt = await ctx.SaveChangesAsync();

                if(cnt < 1 || cnt > 2)
                    throw new Exception("There was an error updating the User Policy record.");

                return ent.Entity.ToModel();
            }
        }

        public async Task<UserStorePolicy> SetUserStorePolicy(UserStorePolicy policy)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.UserStorePolicies.AddAsync(policy.ToEntity());
                var cnt = await ctx.SaveChangesAsync();

                if(cnt < 1 || cnt > 2)
                    throw new Exception("There was an error updating the User Store Policy record.");

                return ent.Entity.ToModel();
            }
        }
    }
}
