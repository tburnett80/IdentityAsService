
namespace Identity.Lib.Internal.StoreProviders.Entities
{
    internal class LockoutPolicyEntity : PolicyEntityBase
    {
        /// <summary>
        /// Gets or sets a flag indicating whether a new user can be locked out. Defaults to true.
        /// </summary>
        public bool AllowedForNewUsers { get; set; }

        /// <summary>
        /// Gets or sets the number of failed access attempts allowed before a user is locked out, assuming lock out is enabled. Defaults to 5.
        /// </summary>
        public int MaxFailedAccessAttempts { get; set; }

        /// <summary>
        /// Gets or sets the Ticks a user is locked out for when a lockout occurs. Defaults to 5 minutes. (Timespan.FromTicks())
        /// </summary>
        public long DefaultLockoutTimeSpanTicks { get; set; }
    }
}
