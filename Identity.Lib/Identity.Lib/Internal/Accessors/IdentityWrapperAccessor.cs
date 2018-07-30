using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Lib.Internal.IdentityWrappers;
using Identity.Lib.Internal.StoreProviders;
using Identity.Lib.Internal.StoreProviders.EF.SqlServer;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Extensions;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Lib.Internal.Accessors
{
    internal class IdentityWrapperAccessor : IIdentityAccessor
    {
        #region Constructor and private members
        private readonly UserManagerWrapper _userManagerWrapper;
        private readonly DbContextOptions<UserDbContext> _options;

        public IdentityWrapperAccessor(UserManagerWrapper userManagerWrapper, DbContextOptions<UserDbContext> options)
        {
            _userManagerWrapper = userManagerWrapper
                ?? throw new ArgumentNullException(nameof(userManagerWrapper));

            _options = options
                ?? throw new ArgumentNullException(nameof(options));
        }
        #endregion

        public async Task<GenericResult<IEnumerable<string>>> CreateUser(User usr)
        {
            var createResult = await _userManagerWrapper.CreateAsync(usr.ToEntity(), usr.Password.TryTrim());

            return new GenericResult<IEnumerable<string>>
            {
                IsFailure = !createResult.Succeeded,
                Message = createResult.Succeeded 
                    ? "Created user"
                    : "Failed to create user",
                Data = createResult.Errors.Select(e => e.Description)
            };
        }

        public async Task<User> GetUserByUserName(string usrName)
        {
            using (var ctx = new UserDbContext(_options))
            {
                var ent = await ctx.Users.FirstOrDefaultAsync(u => u.UserName.Equals(usrName));
                return ent.ToModel();
            }
        }
    }
}
