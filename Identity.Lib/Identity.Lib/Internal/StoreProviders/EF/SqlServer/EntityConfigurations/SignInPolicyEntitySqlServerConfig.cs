using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class SignInPolicyEntitySqlServerConfig : BaseOptionsConfiguration<SignInPolicyEntity>, IEntityTypeConfiguration<SignInPolicyEntity>
    {
        public void Configure(EntityTypeBuilder<SignInPolicyEntity> builder)
        {
            ConfigureBase(builder);

            builder.ToTable("SignInPolicy", "dbo")
                .HasKey(e => e.Id);

            builder.Property(e => e.RequireConfirmedEmail)
                .HasColumnName("RequireConfirmedEmail")
                .IsRequired();

            builder.Property(e => e.RequireConfirmedPhoneNumber)
                .HasColumnName("RequireConfirmedPhoneNumber")
                .IsRequired();
        }
    }
}
