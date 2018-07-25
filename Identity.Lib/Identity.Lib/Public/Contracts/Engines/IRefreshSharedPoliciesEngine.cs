using System.Threading.Tasks;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface IRefreshSharedPoliciesEngine
    {
        Task RefreshPolicies();
    }
}
