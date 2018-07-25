using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class UserStorePolicyEntitySqlServerConfig : BaseOptionsConfiguration<UserStorePolicyEntity>, IEntityTypeConfiguration<UserStorePolicyEntity>
    {
        public void Configure(EntityTypeBuilder<UserStorePolicyEntity> builder)
        {
            ConfigureBase(builder);

            builder.ToTable("StorePolicy", "dbo")
                .HasKey(e => e.Id);

            builder.Property(e => e.MaxLengthForKeys)
                .HasColumnName("MaxLengthForKeys")
                .IsRequired();

            builder.Property(e => e.ProtectPersonalData)
                .HasColumnName("ProtectPersonalData")
                .IsRequired();
        }
    }
}
