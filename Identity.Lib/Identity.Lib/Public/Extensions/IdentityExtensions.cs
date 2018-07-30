using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Identity.Lib.Internal.StoreProviders.EF.SqlServer;
using Microsoft.EntityFrameworkCore;
using Identity.Lib.Internal.Accessors;
using Identity.Lib.Internal.Engines;
using Identity.Lib.Internal.IdentityWrappers;
using Identity.Lib.Internal.StoreProviders.Entities;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Engines;
using Identity.Lib.Public.Contracts.Managers;
using Identity.Lib.Public.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Lib.Public.Extensions
{
    public static class IdentityExtensions
    {
        private static IServiceProvider _provider { get; set; }

        /// <summary>
        /// Will wireup the conection string for the data store using SQL Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        public static void AddIdentityStoreConfigSqlServer(this IServiceCollection services, string conn)
        {
            var ctxOptsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            ctxOptsBuilder.UseSqlServer(conn);
            services.AddSingleton<DbContextOptions<UserDbContext>>(ctxOptsBuilder.Options);
            services.AddTransient<UserDbContext>();
        }

        /// <summary>
        /// Will wire up the Identity components for dependency injection
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomIdentityProviders(this IServiceCollection services)
        {
            SharedPolicyAccessor.PolicyOptions = new IdentityOptions();

            services.AddTransient<IOptions<IdentityOptions>>(ctx => 
                Options.Create(SharedPolicyAccessor.PolicyOptions));

            services.AddSingleton<IPasswordHasher<UserEntity>>(new PasswordHasher<UserEntity>(
                Options.Create(new PasswordHasherOptions
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
                    IterationCount = 1000
                })));

            services.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddSingleton(new IdentityErrorDescriber());
            services.AddSingleton<IEnumerable<IUserValidator<UserEntity>>>(new List<IUserValidator<UserEntity>>
            {
                new UserValidator<UserEntity>()
            });

            services.AddSingleton<IEnumerable<IPasswordValidator<UserEntity>>>(new List<IPasswordValidator<UserEntity>>
            {
                new PasswordValidator<UserEntity>()
            });

            services.AddTransient<IUserStore<UserEntity>, UserStoreWrapper>();
            services.AddTransient<IRoleStore<RoleEntity>, RoleStoreWrapper>();

            services.AddTransient<UserManagerWrapper>();
            services.AddTransient<RoleManagerWrapper>();

            services.AddTransient<IIdentityAccessor, IdentityWrapperAccessor>();
            services.AddTransient<IPolicyAccessor, PolicyAccessor>();
            services.AddSingleton<ISharedPolicyAccessor, SharedPolicyAccessor>();

            services.AddTransient<IRefreshSharedPoliciesEngine, PolicyEngine>();
            services.AddTransient<ISetLockoutPolicyEngine, PolicyEngine>();
            services.AddTransient<ISetPasswordPolicyEngine, PolicyEngine>();
            services.AddTransient<ISetSignInPolicyEngine, PolicyEngine>();
            services.AddTransient<ISetUserPolicyEngine, PolicyEngine>();
            services.AddTransient<ISetUserStorePolicyEngine, PolicyEngine>();

            services.AddTransient<IUserReadManager, UserManager>();
            services.AddTransient<IUserWriteManager, UserManager>();

            services.AddTransient<IPolicyReadManager, PolicyManager>();
            services.AddTransient<IPolicyWriteManager, PolicyManager>();
        }

        public static bool EnsureUserStoreCreated(this IServiceProvider provider)
        {
            using (var ctx = new UserDbContext(provider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                return ctx.Database.EnsureCreated();
            }
        }

        public static bool EnsureUserStoreDeleted(this IServiceProvider provider)
        {
            #if DEBUG
            using (var ctx = new UserDbContext(provider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                return ctx.Database.EnsureDeleted();
            }
            #endif
        }
    }
}
