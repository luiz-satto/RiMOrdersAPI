using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity
{
    public Order(Guid id, string email, string deliveryAddress, DateTime creationDate, DateTime? dateCancelled)
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
    public DateTime? DateCancelled { get; set; }
    public bool IsCancelled { get; set; }

    public IReadOnlyList<OrderItem>? Items { get; private set; }
}