using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExTest2.Configuration
{
    public class OrderEfConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.IdOrder);
            builder.Property(o => o.IdOrder).ValueGeneratedNever();

            builder.Property(o => o.DateAccepted).IsRequired();

            builder.Property(o => o.DateFinished).HasDefaultValue(null);

            builder.Property(o => o.Notes).HasMaxLength(255);
            
            builder.HasOne(o => o.Customer)
                        .WithMany(c => c.Orders)
                        .HasForeignKey(o => o.IdCustomer);

            builder.HasOne(o => o.Employee)
                        .WithMany(e => e.Orders)
                        .HasForeignKey(o => o.IdEmployee);
        }
    }
}