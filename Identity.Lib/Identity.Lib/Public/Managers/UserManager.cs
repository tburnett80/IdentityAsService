using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Lib.Public.Contracts.Accessors;
using Identity.Lib.Public.Contracts.Managers;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Identity;

namespace Identity.Lib.Public.Managers
{
    public sealed class UserManager : IUserReadManager, IUserWriteManager
    {
        #region Constructor and private members
        private readonly IIdentityAccessor _accessor;

        public UserManager(IIdentityAccessor accessor)
        {
            _accessor = accessor
                ?? throw new ArgumentNullException(nameof(accessor));
        }

        public void Dispose()
        {
        }
        #endregion

        #region Read Impl
        public async Task<GenericResult<User>> GetUserByUserNameFromStore(string usrName)
        {
            var result = new GenericResult<User>();

            try
            {
                result.Data = await _accessor.GetUserByUserName(usrName);
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
            }

            return result;
        }
        #endregion

        #region Write Impl
        public async Task<GenericResult<User>> CreateNewUser(User usr)
        {
            var result = new GenericResult<User>();

            try
            {
                var createResult = await _accessor.CreateUser(usr);
                if (createResult.IsFailure)
                    throw new AggregateException(createResult.Message, createResult.Data.Select(e => new Exception(e)));

                result.Data = await _accessor.GetUserByUserName(usr.UserName);
            }
            catch (Exception ex)
            {
                result.IsFailure = true;
                result.Exception = ex;
            }

            return result;
        }
        #endregion
    }
}
