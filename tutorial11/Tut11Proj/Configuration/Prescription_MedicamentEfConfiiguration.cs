using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Models;

namespace Tut11Proj.Configuration
{
    public class Prescription_MedicamentConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
        {
            builder.HasKey(pr_m => new { pr_m.IdMedicament, pr_m.IdPrescription });

            builder.Property(pr_m => pr_m.Dose).HasMaxLength(250);

            builder.HasOne(pr_m => pr_m.Medicament)
                        .WithMany(m => m.Prescriptions_Medicaments)
                        .HasForeignKey(pr_m => pr_m.IdMedicament);

            builder.HasOne(pr_m => pr_m.Prescription)
                        .WithMany(pr => pr.Prescriptions_Medicaments)
                        .HasForeignKey(pr_m => pr_m.IdPrescription);
        }
    }
}