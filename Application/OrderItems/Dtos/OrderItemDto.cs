using Domain.Entities;

namespace Application.OrderItems.Dtos;

public sealed class OrderItemDto
{
    public OrderItemDto(Guid id, Guid orderId, int quantity, Product product)
    {
        Id = id;
        OrderId = orderId;
        Quantity = quantity;
        Product = product;
    }

    public Guid Id { get; }
    public Guid OrderId { get; }
    public int Quantity { get; }
    public Product Product { get; }
}
