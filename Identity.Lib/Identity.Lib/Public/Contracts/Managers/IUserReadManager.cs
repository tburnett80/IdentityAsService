using System;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Identity;

namespace Identity.Lib.Public.Contracts.Managers
{
    public interface IUserReadManager : IDisposable
    {
        Task<GenericResult<User>> GetUserByUserNameFromStore(string usrName);
    }
}
