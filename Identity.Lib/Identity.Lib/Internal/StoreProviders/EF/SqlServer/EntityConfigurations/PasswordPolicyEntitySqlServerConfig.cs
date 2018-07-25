using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class PasswordPolicyEntitySqlServerConfig : BaseOptionsConfiguration<PasswordPolicyEntity>, IEntityTypeConfiguration<PasswordPolicyEntity>
    {
        public void Configure(EntityTypeBuilder<PasswordPolicyEntity> builder)
        {
            ConfigureBase(builder);

            builder.ToTable("PasswordPolicy", "dbo")
                .HasKey(e => e.Id);

            builder.Property(e => e.RequiredLength)
                .HasColumnName("RequiredLength")
                .IsRequired();

            builder.Property(e => e.RequiredUniqueChars)
                .HasColumnName("RequiredUniqueChars")
                .IsRequired();

            builder.Property(e => e.RequireNonAlphanumeric)
                .HasColumnName("RequireNonAlphanumeric")
                .IsRequired();

            builder.Property(e => e.RequireLowercase)
                .HasColumnName("RequireLowercase")
                .IsRequired();

            builder.Property(e => e.RequireUppercase)
                .HasColumnName("RequireUppercase")
                .IsRequired();

            builder.Property(e => e.RequireDigit)
                .HasColumnName("RequireDigit")
                .IsRequired();
        }
    }
}
