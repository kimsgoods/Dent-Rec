using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PatientPrescriptionConfiguration : IEntityTypeConfiguration<PatientPrescription>
    {
        public void Configure(EntityTypeBuilder<PatientPrescription> builder)
        {

            builder.HasOne(x => x.Patient)
                   .WithMany(p => p.PatientPrescriptions)
                   .HasForeignKey(x => x.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Dentist)
                   .WithMany(d => d.PatientPrescriptions)
                   .HasForeignKey(x => x.DentistId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Medication)
                   .WithMany()
                   .HasForeignKey(x => x.MedicationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.AddAuditFields();
        }
    }
}
