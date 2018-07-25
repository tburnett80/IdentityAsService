using Microsoft.AspNetCore.Identity;

namespace Identity.Lib.Internal.Accessors
{
    internal static class SharedPolicyOptionsSingletonAccessor
    {
        internal static IdentityOptions PolicyOptions { get; set; }
    }
}
