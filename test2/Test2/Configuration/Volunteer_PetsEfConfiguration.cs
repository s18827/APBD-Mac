using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test2.Entities;

namespace Test2.Configuration
{
    public class Volunteer_PetsEfConfiguration : IEntityTypeConfiguration<Volunteer_Pet>
    {
        public void Configure(EntityTypeBuilder<Volunteer_Pet> builder)
        {
            builder.HasKey(v_p => new { v_p.IdVolunteer, v_p.IdPet });

            builder.HasOne(v_p => v_p.Volunteer)
                        .WithMany(v => v.Volunteers_Pets)
                        .HasForeignKey(o => o.IdVolunteer);

            builder.HasOne(v_p => v_p.Pet)
                        .WithMany(p => p.Volunteers_Pets)
                        .HasForeignKey(v_p => v_p.IdPet);

            builder.Property(v_p => v_p.DateAccepted).IsRequired();
        }
    }
}