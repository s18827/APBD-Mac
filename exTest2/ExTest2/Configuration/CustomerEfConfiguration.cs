using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExTest2.Configuration
{
    public class CustomerEfConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // builder.HasKey(d => d.IdDoctor);
            // builder.Property(d => d.IdDoctor)
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // builder.Property(d => d.IdDoctor).ValueGeneratedOnAdd();

            builder.HasKey(c => c.IdCustomer);
            builder.Property(c => c.IdCustomer).ValueGeneratedNever();

            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();

            builder.Property(c => c.Surname).HasMaxLength(60).IsRequired();
        }
    }
}