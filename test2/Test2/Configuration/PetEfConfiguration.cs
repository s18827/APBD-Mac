using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Entities;

namespace Test2.Configuration
{
    public class PetEfConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.IdPet);
            builder.Property(p => p.IdPet).ValueGeneratedNever();

            builder.Property(p => p.Name).HasDefaultValue("Not given");
            builder.Property(p => p.Name).HasMaxLength(80);

            builder.Property(p => p.DateRegistered).IsRequired();
            builder.Property(p => p.ApproxDateOfBirth).IsRequired();
            builder.Property(p => p.DateAdopted).HasDefaultValue(null);

            builder.HasOne(p => p.BreedType)
                        .WithMany(bt => bt.Pets)
                        .HasForeignKey(p => p.IdPet);
        }
    }
}