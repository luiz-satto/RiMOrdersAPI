using Application.OrderItems.Dtos;

namespace Application.Orders.Dtos;

public sealed class OrderDto
{
    public OrderDto(Guid id, string email, string deliveryAddress, DateTime creationDate, DateTime? dateCancelled, List<OrderItemDto> orderItems)
    {
        Id = id;
        Email = email;
        DeliveryAddress = deliveryAddress;
        CreationDate = creationDate;
        DateCancelled = dateCancelled;
        IsCancelled = dateCancelled is not null;
        OrderItems = orderItems;
    }

    public Guid Id { get; }
    public string Email { get; }
    public string DeliveryAddress { get; }
    public DateTime CreationDate { get; }
    public DateTime? DateCancelled { get; }
    public bool IsCancelled { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; }
}
