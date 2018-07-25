﻿
namespace Identity.Lib.Public.Models
{
    public sealed class UserPolicy : PolicyBase
    {
        /// <summary>
        /// Gets or sets the list of allowed characters in the username used to validate user names. Defaults to abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
        /// </summary>
        public string AllowedUserNameCharacters { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the application requires unique emails for its users. Defaults to false.
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
    }
}
