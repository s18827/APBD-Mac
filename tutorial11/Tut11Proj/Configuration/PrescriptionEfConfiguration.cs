using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Models;

namespace Tut11Proj.Configuration
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(pr => pr.IdPrescription);

            builder.Property(pr => pr.IdPrescription).ValueGeneratedOnAdd();

            builder.Property(pr => pr.Date).IsRequired();

            builder.HasOne(pr => pr.Patient)
                        .WithMany(p => p.Precriptions)
                        .HasForeignKey(pr => pr.IdPatient);

            builder.HasOne(pr => pr.Doctor)
                        .WithMany(d => d.Precriptions)
                        .HasForeignKey(pr => pr.IdDoctor);
        }
    }
}