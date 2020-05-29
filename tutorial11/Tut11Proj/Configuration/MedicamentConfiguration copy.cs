using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Models;

namespace Tut11Proj.Configuration
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.IdMedicament);

            builder.Property(m => m.IdMedicament).ValueGeneratedOnAdd();

            builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
        }
    }
}