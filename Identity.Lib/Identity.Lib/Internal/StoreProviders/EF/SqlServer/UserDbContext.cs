using Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations;
using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer
{
    internal class UserDbContext : IdentityDbContext<UserEntity, RoleEntity, int, UserClaimEntity, UserRoleEntity, UserLoginEntity, UserRoleClaimEntity, UserTokenEntity>
    {
        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LockoutPolicyEntitySqlServerConfig());
            builder.ApplyConfiguration(new PasswordPolicyEntitySqlServerConfig());
            builder.ApplyConfiguration(new SignInPolicyEntitySqlServerConfig());
            builder.ApplyConfiguration(new UserPolicyEntitySqlServerConfig());
            builder.ApplyConfiguration(new UserStorePolicyEntitySqlServerConfig());
        }

        internal DbSet<RoleEntity> AppRoles { get; set; }

        internal DbSet<UserEntity> AppUsers { get; set; }

        internal DbSet<UserClaimEntity> AppUserClaims { get; set; }

        internal DbSet<UserRoleEntity> AppUserRoles { get; set; }

        internal DbSet<UserLoginEntity> AppUserLogins { get; set; }

        internal DbSet<UserRoleClaimEntity> AppUserRoleClaims { get; set; }

        internal DbSet<UserTokenEntity> AppUserTokens { get; set; }

        internal DbSet<LockoutPolicyEntity> LockoutPolicies { get; set; }

        internal DbSet<PasswordPolicyEntity> PasswordPolicies { get; set; }

        internal DbSet<SignInPolicyEntity> SignInPolicies { get; set; }

        internal DbSet<UserPolicyEntity> UserPolicies { get; set; }

        internal DbSet<UserStorePolicyEntity> UserStorePolicies { get; set; }
    }
}
