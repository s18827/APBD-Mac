using Microsoft.EntityFrameworkCore;
using Tut11Proj.Configuration;

namespace Tut11Proj.Entities
{
    public class s18827DbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }

        public s18827DbContext() { }
        public s18827DbContext(DbContextOptions options)
            : base(options) { } // constructor from superclass - we use it with options => ... in Startup.cs to pass conection string

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // to not mix bussiness logic with DataAdnotations

            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());

            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());

            modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());

            modelBuilder.ApplyConfiguration(new Prescription_MedicamentConfiguration());
            // ALL LOGIC LIKE THIS BELOW MOVED TO Configuration folder
            // modelBuilder.Entity<Prescription_Medicament>()
            //             .HasKey(pr_m => new { pr_m.IdMedicament, pr_m.IdPrescription });

            // modelBuilder.Entity<Prescription_Medicament>()
            //             .Property(pr_m => pr_m.Dose).HasMaxLength(250);

            // modelBuilder.Entity<Prescription_Medicament>()
            //             .HasOne(pr_m => pr_m.Medicament)
            //             .WithMany(m => m.Prescriptions_Medicaments)
            //             .HasForeignKey(pr_m => pr_m.IdMedicament);

            // modelBuilder.Entity<Prescription_Medicament>()
            //             .HasOne(pr_m => pr_m.Prescription)
            //             .WithMany(pr => pr.Prescriptions_Medicaments)
            //             .HasForeignKey(pr_m => pr_m.IdPrescription);

        }
    }
}