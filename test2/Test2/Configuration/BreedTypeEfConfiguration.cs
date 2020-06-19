using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Entities;

namespace Test2.Configuration
{
    public class BreedTypeEfConfiguration : IEntityTypeConfiguration<BreedType>
    {
        public void Configure(EntityTypeBuilder<BreedType> builder)
        {
            builder.HasKey(bt => bt.IdBreedType);

            builder.Property(bt => bt.IdBreedType).ValueGeneratedNever();

            builder.Property(bt => bt.Name).HasDefaultValue("Unknown");

            builder.Property(bt => bt.Description).HasDefaultValue(null);

            builder.Property(bt => bt.Name).HasMaxLength(50);

            builder.Property(bt => bt.Description).HasMaxLength(500);
        }
    }
}