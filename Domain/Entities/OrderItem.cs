using Domain.Primitives;

namespace Domain.Entities;

public sealed class OrderItem : Entity
{
    internal OrderItem()
    {
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
        ProductFk = new();
    }

    public int Quantity { get; set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public Product ProductFk { get; init; }

    public static OrderItem Create(
        Guid id,
        Guid orderId,
        Guid productId,
        string productName,
        string productDescription,
        double productPrice,
        int productStock,
        int quantity)
    {
        var orderItem = new OrderItem(id, orderId, productId, quantity)
        {
            ProductFk = Product.Create(productId, productName, productDescription, productPrice, productStock)
        };

        return orderItem;
    }
}
