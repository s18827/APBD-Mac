using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Entities;

namespace Tut11Proj.Configuration
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.IdMedicament);

            // builder.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
            builder.Property(m => m.IdMedicament).ValueGeneratedNever();


            builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
        }
    }
}