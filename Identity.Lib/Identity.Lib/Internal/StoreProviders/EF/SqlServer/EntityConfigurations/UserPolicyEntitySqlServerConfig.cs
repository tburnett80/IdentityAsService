using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class UserPolicyEntitySqlServerConfig : BaseOptionsConfiguration<UserPolicyEntity>, IEntityTypeConfiguration<UserPolicyEntity>
    {
        public void Configure(EntityTypeBuilder<UserPolicyEntity> builder)
        {
            ConfigureBase(builder);

            builder.ToTable("UserPolicy", "dbo")
                .HasKey(e => e.Id);

            builder.Property(e => e.AllowedUserNameCharacters)
                .HasColumnName("AllowedUserNameCharacters")
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired();

            builder.Property(e => e.RequireUniqueEmail)
                .HasColumnName("RequireUniqueEmail")
                .IsRequired();
        }
    }
}
