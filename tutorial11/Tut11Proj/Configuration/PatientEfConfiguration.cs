using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Models;

namespace Tut11Proj.Configuration
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.IdPatient);

            builder.Property(p => p.IdPatient).ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Birthdate).IsRequired();
        }
    }
}