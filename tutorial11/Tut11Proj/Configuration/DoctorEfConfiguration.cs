using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tut11Proj.Entities;

namespace Tut11Proj.Configuration
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.IdDoctor);

            builder.Property(d => d.IdDoctor).ValueGeneratedOnAdd();

            builder.Property(d => d.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(d => d.LastName).HasMaxLength(100).IsRequired();
        }
    }
}