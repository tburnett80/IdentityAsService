using System.Security.Claims;
using Identity.Lib.Internal.StoreProviders.EF.SqlServer;
using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identity.Lib.Internal.IdentityWrappers
{
    internal class RoleStoreWrapper : RoleStore<RoleEntity, UserDbContext, int, UserRoleEntity, UserRoleClaimEntity>
    {
        public RoleStoreWrapper(UserDbContext context, IdentityErrorDescriber describer = null) 
            : base(context, describer)
        {
        }

        protected override UserRoleClaimEntity CreateRoleClaim(RoleEntity role, Claim claim)
        {
            return new UserRoleClaimEntity
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }
    }
}
