using Domain.Primitives;

namespace Domain.Entities;

public sealed class OrderItem : Entity
{
    internal OrderItem()
    {
        OrderFk = new();
        ProductFk = new();
    }

    private OrderItem(
        Guid id,
        Guid orderId,
        Guid productId,
        int quantity)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        OrderFk = new();
        ProductFk = new();
    }

    public int Quantity { get; set; }

    public Guid OrderId { get; private set; }
    public Order OrderFk { get; init; }

    public Guid ProductId { get; private set; }
    public Product ProductFk { get; init; }

    public static OrderItem Create(
        Guid id,
        Guid orderId,
        string orderEmail,
        string orderDeliveryAddress,
        DateTime? dateCancelled,
        Guid productId,
        string productName,
        string productDescription,
        double productPrice,
        int productStock,
        int quantity)
    {
        var orderItem = new OrderItem(id, orderId, productId, quantity)
        {
            OrderFk = Order.Create(orderId, orderEmail, orderDeliveryAddress, dateCancelled),
            ProductFk = Product.Create(productId, productName, productDescription, productPrice, productStock)
        };

        return orderItem;
    }
}
