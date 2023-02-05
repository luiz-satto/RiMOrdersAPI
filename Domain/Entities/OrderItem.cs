using Domain.Primitives;

namespace Domain.Entities;

public sealed class OrderItem : Entity
{
    public OrderItem(Guid id, Guid orderId, Guid productId, int quantity)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public int Quantity { get; set; }

    public Guid OrderId { get; private set; }
    public Order? OrderFk { get; private set; }

    public Guid ProductId { get; private set; }
    public Product? ProductFk { get; private set; }
}
