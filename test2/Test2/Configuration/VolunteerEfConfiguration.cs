using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Entities;

namespace Test2.Configuration
{
    public class VolunteerEfConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.HasKey(v => v.IdVolunteer);
            builder.Property(v => v.IdVolunteer).ValueGeneratedNever();

            builder.Property(v => v.IdSupervisor).HasDefaultValue(null);
            builder.Property(v => v.Name).HasMaxLength(80);
            builder.Property(v => v.Name).IsRequired();

            builder.Property(v => v.Surname).HasMaxLength(80);
            builder.Property(v => v.Surname).IsRequired();

            builder.Property(v => v.Phone).HasMaxLength(30);
            builder.Property(v => v.Phone).IsRequired();
            
            builder.Property(v => v.Address).HasMaxLength(100);
            builder.Property(v => v.Address).IsRequired();
            
            builder.Property(v => v.Email).HasMaxLength(80);
            builder.Property(v => v.Email).IsRequired();

            builder.Property(v => v.StartingDate).IsRequired();

            builder.HasOne(v => v.Supervisor)
                        .WithMany(s => s.Volunteers)
                        .HasForeignKey(v => v.IdVolunteer);
        }
    }
}