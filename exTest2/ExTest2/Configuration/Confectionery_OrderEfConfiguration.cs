using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExTest2.Configuration
{
    public class Confectionery_OrderEfConfiguration : IEntityTypeConfiguration<Confectionery_Order>
    {
        public void Configure(EntityTypeBuilder<Confectionery_Order> builder)
        {
            builder.HasKey(co_o => new { co_o.IdOrder, co_o.IdConfectionery });

            // builder.Property(co_o => co_o.Quantity).IsRequired();

            builder.Property(co_o => co_o.Notes).HasMaxLength(255);

            builder.HasOne(co_o => co_o.Confectionery)
                        .WithMany(c => c.Confectioneries_Orders)
                        .HasForeignKey(o => o.IdConfectionery);

            builder.HasOne(co_o => co_o.Order)
                        .WithMany(o => o.Confectioneries_Orders)
                        .HasForeignKey(co_o => co_o.IdOrder);
        }
    }
}