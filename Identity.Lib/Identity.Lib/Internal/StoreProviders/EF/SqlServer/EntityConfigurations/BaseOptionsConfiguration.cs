using Identity.Lib.Internal.StoreProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Lib.Internal.StoreProviders.EF.SqlServer.EntityConfigurations
{
    internal class BaseOptionsConfiguration<TEnt> where TEnt : PolicyEntityBase
    {
        public void ConfigureBase(EntityTypeBuilder<TEnt> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Created)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedById)
                .HasColumnName("CreatedById")
                .IsRequired();

            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.CreatedById);
        }
    }
}
