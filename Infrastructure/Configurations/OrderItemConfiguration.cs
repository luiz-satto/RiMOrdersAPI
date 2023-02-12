using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);
        builder.Property(orderItem => orderItem.OrderId).IsRequired();

        builder.Property(orderItem => orderItem.ProductId).IsRequired();
        builder.HasOne(orderItem => orderItem.ProductFk);
        builder.Property(orderItem => orderItem.Quantity);
    }
}
