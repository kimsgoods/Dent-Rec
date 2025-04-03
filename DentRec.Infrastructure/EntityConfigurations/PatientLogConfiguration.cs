using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PatientLogConfiguration : IEntityTypeConfiguration<PatientLog>
    {
        public void Configure(EntityTypeBuilder<PatientLog> builder)
        {

            builder.Property(x => x.ProcedureDate)
                .HasColumnType("datetime2(3)")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(255);

            builder.Property(x => x.PaymentStatus)
                .HasMaxLength(10);

            builder.Property(x => x.Fee)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(x => x.Patient)
                .WithMany(p => p.PatientLogs)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Dentist)
                .WithMany(d => d.PatientLogs)
                .HasForeignKey(x => x.DentistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Procedures)
                .WithMany(p => p.PatientLogs);

            builder.HasMany(x => x.Payments)
                .WithOne(p => p.PatientLog)
                .HasForeignKey(p => p.PatientLogId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.AddAuditFields();
        }
    }
}
