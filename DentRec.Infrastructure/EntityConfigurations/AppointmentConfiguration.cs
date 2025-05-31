using DentRec.Domain.Entities;
using DentRec.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {

            builder.Property(x => x.AppointmentDateTime)
               .HasColumnType("datetime2(3)")
               .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(255);

            builder.Property(i => i.Status)
                .HasConversion(new EnumToStringConverter<AppointmentStatus>())
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(i => i.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Dentist)
                .WithMany(p => p.Appointments)
                .HasForeignKey(i => i.DentistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.AddAuditFields();

        }
    }
}
