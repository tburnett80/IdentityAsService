using System.Collections.Generic;
using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Lib.Internal.IdentityWrappers
{
    internal class RoleManagerWrapper : RoleManager<RoleEntity>
    {
        public RoleManagerWrapper(IRoleStore<RoleEntity> store, IEnumerable<IRoleValidator<RoleEntity>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<RoleEntity>> logger) 
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
