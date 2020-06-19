using Microsoft.EntityFrameworkCore;
using Test2.Configuration;

namespace Test2.Entities
{
    public class s18827DbContext : DbContext
    {
        public DbSet<BreedType> BreedTypes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Volunteer_Pet> Volunteers_Pets { get; set; }


        public s18827DbContext() { }
        public s18827DbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // to not mix bussiness logic with DataAdnotations
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BreedTypeEfConfiguration());
            modelBuilder.ApplyConfiguration(new PetEfConfiguration());
            modelBuilder.ApplyConfiguration(new VolunteerEfConfiguration());
            modelBuilder.ApplyConfiguration(new Volunteer_PetsEfConfiguration());

            // ADDING SAMPLE DATA:

            // var pets = new List<Pet>();
            // pets.Add(new Pet { IdPet = 1, FirstName = "aaaa", LastName = "xxx", Email = "asds@sda.com" });


            // var patients = new List<Patient>();
            // patients.Add(new Patient { IdPatient = 1, FirstName = "ala", LastName = "makota", Birthdate = new DateTime(2000, 12, 22) });

            // var prescriptions = new List<Prescription>();
            // prescriptions.Add(new Prescription { IdPrescription = 1, Date = new DateTime(2020, 02, 02), DueDate = new DateTime(2020, 03, 03), IdPatient = 1, IdDoctor = 1 });

            // var medicaments = new List<Medicament>();
            // medicaments.Add(new Medicament { IdMedicament = 1, Name = "medicament", Description = "desc", Type = "type" });

            // var prs_meds = new List<Prescription_Medicament>();
            // prs_meds.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1 });

            // modelBuilder.Entity<Doctor>().HasData(doctors);
            // modelBuilder.Entity<Patient>().HasData(patients);
            // modelBuilder.Entity<Prescription>().HasData(prescriptions);
            // modelBuilder.Entity<Medicament>().HasData(medicaments);
            // modelBuilder.Entity<Prescription_Medicament>().HasData(prs_meds);
        }
    }
}