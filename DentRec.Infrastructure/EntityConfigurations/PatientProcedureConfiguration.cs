using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PatientProcedureConfiguration : IEntityTypeConfiguration<PatientProcedure>
    {
        public void Configure(EntityTypeBuilder<PatientProcedure> builder)
        {            

            builder.Property(x => x.ProcedureDate)
                .HasColumnType("datetime2(3)")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(255);

            builder.Property(x => x.Cost)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(x => x.Patient)
                .WithMany(p => p.PatientProcedures)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Dentist)
                .WithMany(d => d.PatientProcedures)
                .HasForeignKey(x => x.DentistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Procedure)
                .WithMany()
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
