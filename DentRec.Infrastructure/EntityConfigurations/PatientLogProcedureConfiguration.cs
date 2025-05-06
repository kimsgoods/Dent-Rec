using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class PatientLogProcedureConfiguration : IEntityTypeConfiguration<PatientLogProcedure>
    {
        public void Configure(EntityTypeBuilder<PatientLogProcedure> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AdjustedFee)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(x => x.Notes)
                .HasMaxLength(255);

            builder.HasOne(x => x.PatientLog)
                .WithMany(x => x.PatientLogProcedures)
                .HasForeignKey(x => x.PatientLogId);

            builder.HasOne(x => x.Procedure)
                .WithMany(x => x.PatientLogProcedures)
                .HasForeignKey(x => x.ProcedureId);

            builder.AddAuditFields();
        }
    }
}