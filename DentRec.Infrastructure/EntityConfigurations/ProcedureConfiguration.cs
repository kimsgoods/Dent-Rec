using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(x => x.Cost)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.EstimatedDuration)
                .IsRequired(false);

            builder.AddAuditFields();
        }
    }
}
