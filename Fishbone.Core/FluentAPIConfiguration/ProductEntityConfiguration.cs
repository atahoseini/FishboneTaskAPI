using Fishbone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fishbone.Core.FluentAPIConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(p => p.ProductDescription)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
