using Application.OrderItems.Queries.GetOrderItemById;
using Domain.Entities;

namespace Application.OrderItems.Dtos;

public sealed class OrderItemDto
{
    private OrderItemDto(Guid id, Guid orderId, int quantity, Product product)
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

    public static OrderItemDto Create(OrderItemResponse item)
    {
        var product = Product.Create(item.ProductId, item.ProductName, item.ProductDescription, item.ProductPrice, item.ProductStock);
        var orderItem = new OrderItemDto(item.Id, item.OrderId, item.Quantity, product);
        return orderItem;
    }
}
