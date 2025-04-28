using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> AddAuditFields<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.Property(x => x.CreatedOn)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(true);

            builder.Property(x => x.ModifiedOn)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("datetime2");

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100);

            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(100);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            return builder;
        }
    }
}
