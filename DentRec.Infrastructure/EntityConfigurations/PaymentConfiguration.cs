using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(i => i.Amount)
                .HasColumnType("decimal(10,2)");           

            builder.Property(i => i.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.AddAuditFields();

            builder.HasOne(i => i.Patient)
                .WithMany(p => p.Payments)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.PatientLog)
                .WithMany()
                .HasForeignKey(i => i.PatientProcedureId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
