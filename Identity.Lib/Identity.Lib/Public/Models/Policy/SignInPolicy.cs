
namespace Identity.Lib.Public.Models.Policy
{
    public sealed class SignInPolicy : PolicyBase
    {
        /// <summary>
        /// Gets or sets a flag indicating whether a confirmed email address is required to sign in. Defaults to false.
        /// </summary>
        public bool RequireConfirmedEmail { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether a confirmed telephone number is required to sign in. Defaults to false.
        /// </summary>
        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}
