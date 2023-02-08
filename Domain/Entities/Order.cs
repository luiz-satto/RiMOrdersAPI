using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity
{
    internal Order()
    {
        Email = string.Empty;
        DeliveryAddress = string.Empty;
    }

    private Order(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime? dateCancelled)
        : base(id)
    {
        Email = email;
        DeliveryAddress = deliveryAddress;
        DateCancelled = dateCancelled;
        IsCancelled = dateCancelled is not null;
        CreationDate = DateTime.UtcNow;
    }

    public string Email { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? DateCancelled { get; set; }
    public bool IsCancelled { get; set; }
    public IReadOnlyList<OrderItem>? Items { get; set; }

    public static Order Create(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime? dateCancelled = null,
        IReadOnlyList<OrderItem>? items = null)
    {
        var order = new Order(
            id,
            email,
            deliveryAddress,
            dateCancelled)
        {
            Items = items
        };

        return order;
    }
}