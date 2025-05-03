using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentRec.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientPrescription> PatientPrescriptions { get; set; }
        public DbSet<PatientLog> PatientLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<PatientLogProcedure> PatientLogProcedures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dentist>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Prescription>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Patient>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PatientPrescription>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PatientLog>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Procedure>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PatientLogProcedure>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUser = currentUserService.GetUserName();
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = DateTime.Now;
                    entry.Entity.ModifiedBy = currentUser;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.ModifiedOn = DateTime.Now;
                    entry.Entity.CreatedBy = currentUser;
                    entry.Entity.ModifiedBy = currentUser;
                }
            }

            foreach (var entry in ChangeTracker.Entries<PatientLogProcedure>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.CalculatedAdjustedFee();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}
