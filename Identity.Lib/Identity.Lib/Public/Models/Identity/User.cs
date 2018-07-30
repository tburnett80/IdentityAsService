using System;

namespace Identity.Lib.Public.Models.Identity
{
    public sealed class User
    {
        public int UserId { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
