using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid OrderId,
    string Email,
    string DeliveryAddress,
    DateTime CreationDate,
    DateTime? DateCancelled,
    List<OrderItem>? Items
) : ICommand<Order>;
