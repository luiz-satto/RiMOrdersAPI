using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity
{
    private Order(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime creationDate,
        DateTime? dateCancelled)
        : base(id)
    {
        Email = email;
        DeliveryAddress = deliveryAddress;
        CreationDate = creationDate;
        DateCancelled = dateCancelled;
        IsCancelled = dateCancelled is not null;
    }

    public string Email { get; private set; }
    public string DeliveryAddress { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? DateCancelled { get; private set; }
    public bool IsCancelled { get; private set; }
    public List<OrderItem>? Items { get; private set; }

    public static Order Create(
        Guid id,
        string email,
        string deliveryAddress,
        DateTime creationDate,
        DateTime? dateCancelled = null,
        List<OrderItem>? items = null)
    {
        var order = new Order(id, email, deliveryAddress, creationDate, dateCancelled)
        {
            IsCancelled = dateCancelled is not null,
            Items = items
        };

        return order;
    }
}