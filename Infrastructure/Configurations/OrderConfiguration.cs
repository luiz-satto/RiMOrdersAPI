using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        builder.Property(order => order.Email).HasMaxLength(100);
        builder.Property(order => order.DeliveryAddress).HasMaxLength(100);
        builder.Property(order => order.CreationDate).HasDefaultValue(DateTime.Today);
        builder.Property(order => order.DateCancelled).HasDefaultValue(null);
        builder.Property(order => order.IsCancelled).HasDefaultValue(false);
    }
}
