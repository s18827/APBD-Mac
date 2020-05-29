using Microsoft.EntityFrameworkCore;

namespace Tut11Proj.Models
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Patient
            modelBuilder.Entity<Patient>()
                        .HasKey(p => p.IdPatient);

            modelBuilder.Entity<Patient>()
                        .Property(p => p.IdPatient).ValueGeneratedOnAdd();

            modelBuilder.Entity<Patient>()
                        .Property(p => p.FirstName).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(p => p.LastName).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(p => p.Birthdate).IsRequired();

            #endregion

            #region Doctor
            modelBuilder.Entity<Doctor>()
                        .HasKey(d => d.IdDoctor);

            modelBuilder.Entity<Doctor>()
                        .Property(d => d.IdDoctor).ValueGeneratedOnAdd();

            modelBuilder.Entity<Doctor>()
                        .Property(d => d.FirstName).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Doctor>()
                        .Property(d => d.LastName).HasMaxLength(100).IsRequired();

            #endregion

            #region Prescription
            modelBuilder.Entity<Prescription>()
                        .HasKey(pr => pr.IdPrescription);

            modelBuilder.Entity<Prescription>()
                        .Property(pr => pr.IdPrescription).ValueGeneratedOnAdd();

            modelBuilder.Entity<Prescription>()
                        .Property(pr => pr.Date).IsRequired();

            modelBuilder.Entity<Prescription>()
                        .HasOne(pr => pr.Patient)
                        .WithMany(p => p.Precriptions)
                        .HasForeignKey(pr => pr.IdPatient);

            modelBuilder.Entity<Prescription>()
                        .HasOne(pr => pr.Doctor)
                        .WithMany(d => d.Precriptions)
                        .HasForeignKey(pr => pr.IdDoctor);

            #endregion

            #region Medicament
            modelBuilder.Entity<Medicament>()
                        .HasKey(m => m.IdMedicament);

            modelBuilder.Entity<Medicament>()
                        .Property(m => m.IdMedicament).ValueGeneratedOnAdd();

            modelBuilder.Entity<Medicament>()
                        .Property(m => m.Name).HasMaxLength(100).IsRequired();

            #endregion

            #region Prescription_Medicament
            modelBuilder.Entity<Prescription_Medicament>()
                        .HasKey(pr_m => new { pr_m.IdMedicament, pr_m.IdPrescription });

            modelBuilder.Entity<Prescription_Medicament>()
                        .Property(pr_m => pr_m.Dose).HasMaxLength(250);

            modelBuilder.Entity<Prescription_Medicament>()
                        .HasOne(pr_m => pr_m.Medicament)
                        .WithMany(m => m.Prescriptions_Medicaments)
                        .HasForeignKey(pr_m => pr_m.IdMedicament);

            modelBuilder.Entity<Prescription_Medicament>()
                        .HasOne(pr_m => pr_m.Prescription)
                        .WithMany(pr => pr.Prescriptions_Medicaments)
                        .HasForeignKey(pr_m => pr_m.IdPrescription);

            #endregion
        }
    }
}