using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExTest2.Configuration
{
    public class ConfectioneryEfConfiguration : IEntityTypeConfiguration<Confectionery>
    {
        public void Configure(EntityTypeBuilder<Confectionery> builder)
        {
            // builder.HasKey(d => d.IdDoctor);
            // builder.Property(d => d.IdDoctor)
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // builder.Property(d => d.IdDoctor).ValueGeneratedOnAdd();

            builder.HasKey(co => co.IdConfectionery);
            builder.Property(co => co.IdConfectionery).ValueGeneratedNever();

            builder.Property(co => co.Name).HasMaxLength(200).IsRequired();

            // builder.Property(co => co.PricePerItem).IsRequired();

            builder.Property(co => co.Type).HasMaxLength(40);
        }
    }
}