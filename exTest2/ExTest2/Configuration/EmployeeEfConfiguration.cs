using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExTest2.Configuration
{
    public class EmployeeEfConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // builder.HasKey(d => d.IdDoctor);
            // builder.Property(d => d.IdDoctor)
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // builder.Property(d => d.IdDoctor).ValueGeneratedOnAdd();

            builder.HasKey(e => e.IdEmployee);
            builder.Property(e => e.IdEmployee).ValueGeneratedNever();

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.Property(e => e.Surname).HasMaxLength(60).IsRequired();
        }
    }
}