using System.Security.Claims;
using Identity.Lib.Internal.StoreProviders.EF.SqlServer;
using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identity.Lib.Internal.IdentityWrappers
{
    internal class UserStoreWrapper : UserStore<UserEntity, RoleEntity, UserDbContext, int, UserClaimEntity, UserRoleEntity, UserLoginEntity, UserTokenEntity, UserRoleClaimEntity>
    {
        public UserStoreWrapper(UserDbContext context, IdentityErrorDescriber describer = null) 
            : base(context, describer)
        {
        }

        protected override UserClaimEntity CreateUserClaim(UserEntity user, Claim claim)
        {
            var uClaim = new UserClaimEntity { UserId = user.Id };
            uClaim.InitializeFromClaim(claim);

            return uClaim;
        }

        protected override UserLoginEntity CreateUserLogin(UserEntity user, UserLoginInfo login)
        {
            return new UserLoginEntity
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserRoleEntity CreateUserRole(UserEntity user, RoleEntity role)
        {
            return new UserRoleEntity { UserId = user.Id, RoleId = role.Id };
        }

        protected override UserTokenEntity CreateUserToken(UserEntity user, string loginProvider, string name, string value)
        {
            return new UserTokenEntity
            {
                UserId = user.Id,
                LoginProvider = loginProvider, 
                Name = name,
                Value = value
            };
        }
    }
}
