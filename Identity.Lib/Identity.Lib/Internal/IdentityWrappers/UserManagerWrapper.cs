using System;
using System.Collections.Generic;
using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.Lib.Internal.IdentityWrappers
{
    internal class UserManagerWrapper : UserManager<UserEntity>
    {
        public UserManagerWrapper(IUserStore<UserEntity> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserEntity> passwordHasher, IEnumerable<IUserValidator<UserEntity>> userValidators, IEnumerable<IPasswordValidator<UserEntity>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserEntity>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
