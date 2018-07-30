using System;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Identity;

namespace Identity.Lib.Public.Contracts.Managers
{
    public interface IUserWriteManager : IDisposable
    {
        Task<GenericResult<User>> CreateNewUser(User usr);
    }
}
