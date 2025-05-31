using DentRec.Domain.Entities;
using DentRec.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(i => i.Amount)
                .HasColumnType("decimal(10,2)");

            builder.Property(i => i.PaymentMethod)
                .HasConversion(new EnumToStringConverter<PaymentMethod>())
                .HasMaxLength(50)
                .IsRequired();

            builder.AddAuditFields();

            builder.HasOne(i => i.Patient)
                .WithMany(p => p.Payments)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
