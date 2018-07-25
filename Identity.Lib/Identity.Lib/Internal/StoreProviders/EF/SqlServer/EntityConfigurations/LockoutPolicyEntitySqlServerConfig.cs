using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class LockoutPolicyEntitySqlServerConfig : BaseOptionsConfiguration<LockoutPolicyEntity>, IEntityTypeConfiguration<LockoutPolicyEntity>
    {
        public void Configure(EntityTypeBuilder<LockoutPolicyEntity> builder)
        {
            ConfigureBase(builder);

            builder.ToTable("LockoutOptions", "dbo")
                .HasKey(e => e.Id);

            builder.Property(e => e.AllowedForNewUsers)
                .HasColumnName("AllowedForNewUsers")
                .IsRequired();

            builder.Property(e => e.MaxFailedAccessAttempts)
                .HasColumnName("MaxFailedAccessAttempts")
                .IsRequired();

            builder.Property(e => e.DefaultLockoutTimeSpanTicks)
                .HasColumnName("DefaultLockoutTimeSpanTicks")
                .IsRequired();
        }
    }
}
