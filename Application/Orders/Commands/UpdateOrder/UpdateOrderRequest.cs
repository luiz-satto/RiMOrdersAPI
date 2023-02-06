using Domain.Entities;

namespace Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderRequest(
    Guid OrderId,
    string Email,
    string DeliveryAddress,
    DateTime? DateCancelled,
    List<OrderItem>? Items
);