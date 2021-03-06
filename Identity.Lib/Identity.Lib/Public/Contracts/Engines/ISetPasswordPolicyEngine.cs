﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Lib.Public.Models;
using Identity.Lib.Public.Models.Policy;

namespace Identity.Lib.Public.Contracts.Engines
{
    public interface ISetPasswordPolicyEngine
    {
        Task<GenericResult<IEnumerable<string>>> ValidatePasswordPolicyForSave(PasswordPolicy policy);

        Task<PasswordPolicy> SetPasswordPolicy(PasswordPolicy policy);
    }
}
