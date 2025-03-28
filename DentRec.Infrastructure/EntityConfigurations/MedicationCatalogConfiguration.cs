using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentRec.Infrastructure.EntityConfigurations
{
    public class MedicationCatalogConfiguration : IEntityTypeConfiguration<MedicationCatalog>
    {
        public void Configure(EntityTypeBuilder<MedicationCatalog> builder)
        {
            builder.Property(x => x.Name)                
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(100);

            builder.Property(x => x.Dosage)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Instructions)
                .HasMaxLength(100)
                .IsRequired();

            builder.AddAuditFields();
        }
    }
}
