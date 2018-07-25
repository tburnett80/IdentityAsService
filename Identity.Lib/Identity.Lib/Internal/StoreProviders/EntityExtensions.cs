using System;
using Identity.Lib.Internal.StoreProviders.Entities;
using Identity.Lib.Public.Models;

namespace Identity.Lib.Internal.StoreProviders
{
    /// <summary>
    /// Will convert an entity to a model
    /// </summary>
    internal static class EntityExtensions
    {
        #region Entity to Model conversions
        internal static LockoutPolicy ToModel(this LockoutPolicyEntity ent)
        {
            if (ent == null)
                return null;

            return new LockoutPolicy
            {
                Id = ent.Id,
                Created = ent.Created,
                CreatedById = ent.CreatedById,
                AllowedForNewUsers = ent.AllowedForNewUsers,
                MaxFailedAccessAttempts = ent.MaxFailedAccessAttempts,
                DefaultLockoutTimeSpan = TimeSpan.FromTicks(ent.DefaultLockoutTimeSpanTicks)
            };
        }

        internal static PasswordPolicy ToModel(this PasswordPolicyEntity ent)
        {
            if (ent == null)
                return null;

            return new PasswordPolicy
            {
                Id = ent.Id,
                Created = ent.Created,
                CreatedById = ent.CreatedById,
                RequiredLength = ent.RequiredLength,
                RequiredUniqueChars = ent.RequiredUniqueChars,
                RequireNonAlphanumeric = ent.RequireNonAlphanumeric,
                RequireLowercase = ent.RequireLowercase,
                RequireUppercase = ent.RequireUppercase,
                RequireDigit = ent.RequireDigit
            };
        }

        internal static SignInPolicy ToModel(this SignInPolicyEntity ent)
        {
            if (ent == null)
                return null;

            return new SignInPolicy
            {
                Id = ent.Id,
                Created = ent.Created,
                CreatedById = ent.CreatedById,
                RequireConfirmedEmail = ent.RequireConfirmedEmail,
                RequireConfirmedPhoneNumber = ent.RequireConfirmedPhoneNumber
            };
        }

        internal static UserPolicy ToModel(this UserPolicyEntity ent)
        {
            if (ent == null)
                return null;

            return new UserPolicy
            {
                Id = ent.Id,
                Created = ent.Created,
                CreatedById = ent.CreatedById,
                AllowedUserNameCharacters = ent.AllowedUserNameCharacters,
                RequireUniqueEmail = ent.RequireUniqueEmail
            };
        }

        internal static UserStorePolicy ToModel(this UserStorePolicyEntity ent)
        {
            if (ent == null)
                return null;

            return new UserStorePolicy
            {
                Id = ent.Id,
                Created = ent.Created,
                CreatedById = ent.CreatedById,
                MaxLengthForKeys = ent.MaxLengthForKeys,
                ProtectPersonalData = ent.ProtectPersonalData
            };
        }
        #endregion

        #region Model to Entity conversions
        internal static LockoutPolicyEntity ToEntity(this LockoutPolicy model)
        {
            if (model == null)
                return null;

            return new LockoutPolicyEntity
            {
                Id = model.Id,
                Created = model.Created,
                CreatedById = model.CreatedById,
                AllowedForNewUsers = model.AllowedForNewUsers,
                MaxFailedAccessAttempts = model.MaxFailedAccessAttempts,
                DefaultLockoutTimeSpanTicks = model.DefaultLockoutTimeSpan.Ticks
            };
        }

        internal static PasswordPolicyEntity ToEntity(this PasswordPolicy model)
        {
            if (model == null)
                return null;

            return new PasswordPolicyEntity
            {
                Id = model.Id,
                Created = model.Created,
                CreatedById = model.CreatedById,
                RequiredLength = model.RequiredLength,
                RequiredUniqueChars = model.RequiredUniqueChars,
                RequireNonAlphanumeric = model.RequireNonAlphanumeric,
                RequireLowercase = model.RequireLowercase,
                RequireUppercase = model.RequireUppercase,
                RequireDigit = model.RequireDigit
            };
        }

        internal static SignInPolicyEntity ToEntity(this SignInPolicy model)
        {
            if (model == null)
                return null;

            return new SignInPolicyEntity
            {
                Id = model.Id,
                Created = model.Created,
                CreatedById = model.CreatedById,
                RequireConfirmedEmail = model.RequireConfirmedEmail,
                RequireConfirmedPhoneNumber = model.RequireConfirmedPhoneNumber
            };
        }

        internal static UserPolicyEntity ToEntity(this UserPolicy model)
        {
            if (model == null)
                return null;

            return new UserPolicyEntity
            {
                Id = model.Id,
                Created = model.Created,
                CreatedById = model.CreatedById,
                AllowedUserNameCharacters = model.AllowedUserNameCharacters,
                RequireUniqueEmail = model.RequireUniqueEmail
            };
        }

        internal static UserStorePolicyEntity ToEntity(this UserStorePolicy model)
        {
            if (model == null)
                return null;

            return new UserStorePolicyEntity
            {
                Id = model.Id,
                Created = model.Created,
                CreatedById = model.CreatedById,
                MaxLengthForKeys = model.MaxLengthForKeys,
                ProtectPersonalData = model.ProtectPersonalData
            };
        }
        #endregion
    }
}
