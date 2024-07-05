using Fishbone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fishbone.Core.FluentAPIConfiguration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.ProductId)
                .IsRequired();
            builder.Property(p => p.OrderQuantity)
                .IsRequired();
        }
    }
}
