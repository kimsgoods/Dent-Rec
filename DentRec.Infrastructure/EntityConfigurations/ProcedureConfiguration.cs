using DentRec.Domain.Entities;
using DentRec.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(255);

            builder.Property(x => x.Fee)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.PricingType)
                .HasConversion(new EnumToStringConverter<PricingType>())
                .HasMaxLength(50)
                .IsRequired();

            builder.AddAuditFields();
        }
    }
}
