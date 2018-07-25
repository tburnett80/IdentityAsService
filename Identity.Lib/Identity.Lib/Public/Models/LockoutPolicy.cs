using System;

namespace Identity.Lib.Public.Models
{
    public sealed class LockoutPolicy : PolicyBase
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
        /// Gets or sets the Ticks a user is locked out for when a lockout occurs. Defaults to 5 minutes.
        /// </summary>
        public TimeSpan DefaultLockoutTimeSpan { get; set; }
    }
}
