using DentRec.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentRec.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<MedicationCatalog> MedicationCatalogs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientPrescription> PatientPrescriptions { get; set; }
        public DbSet<PatientProcedure> PatientProcedures { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProcedureCatalog> ProcedureCatalogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = DateTime.Now;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.ModifiedOn = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}
