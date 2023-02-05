using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Name).HasMaxLength(100);
        builder.Property(product => product.Description).HasMaxLength(1000);
        builder.Property(product => product.Price);
        builder.Property(product => product.Stock);
    }
}
