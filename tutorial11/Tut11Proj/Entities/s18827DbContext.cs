using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tut11Proj.Configuration;

namespace Tut11Proj.Entities
{
    public class s18827DbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }

        public s18827DbContext() { }
        public s18827DbContext(DbContextOptions options)
            : base(options) { } // constructor from superclass - we use it with options => ... in Startup.cs to pass conection string

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // to not mix bussiness logic with DataAdnotations

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());

            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());

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
            var doctors = new List<Doctor>();
            doctors.Add(new Doctor { IdDoctor = 1, FirstName = "aaaa", LastName = "xxx", Email = "asds@sda.com" });
            doctors.Add(new Doctor { IdDoctor = 2, FirstName = "bbbb", LastName = "yyyy", Email = "asds@sda.com" });
            doctors.Add(new Doctor { IdDoctor = 3, FirstName = "cccc", LastName = "zzzz", Email = "asds@sda.com" });

            var patients = new List<Patient>();
            patients.Add(new Patient { IdPatient = 1, FirstName = "ala", LastName = "makota", Birthdate = new DateTime(2000, 12, 22) });

            var prescriptions = new List<Prescription>();
            prescriptions.Add(new Prescription { IdPrescription = 1, Date = new DateTime(2020, 02, 02), DueDate = new DateTime(2020, 03, 03), IdPatient = 1, IdDoctor = 1 });

            var medicaments = new List<Medicament>();
            medicaments.Add(new Medicament { IdMedicament = 1, Name = "medicament", Description = "desc", Type = "type" });

            var prs_meds = new List<Prescription_Medicament>();
            prs_meds.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1 });

            modelBuilder.Entity<Doctor>().HasData(doctors);
            modelBuilder.Entity<Patient>().HasData(patients);
            modelBuilder.Entity<Prescription>().HasData(prescriptions);
            modelBuilder.Entity<Medicament>().HasData(medicaments);
            modelBuilder.Entity<Prescription_Medicament>().HasData(prs_meds);

        }
    }
}