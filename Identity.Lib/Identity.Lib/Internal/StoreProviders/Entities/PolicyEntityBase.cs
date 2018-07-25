using System;

namespace Identity.Lib.Internal.StoreProviders.Entities
{
    internal abstract class PolicyEntityBase
    {
        /// <summary>
        /// Database record Id / Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Timestamp when this record was created
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// User Id of the user who created this record
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Optional User Model containing the details of the user who created this policy.
        /// </summary>
        public virtual UserEntity User { get; set; }
    }
}
