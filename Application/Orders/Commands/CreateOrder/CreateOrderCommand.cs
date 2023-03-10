using Application.Abstractions.Messaging;

namespace Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    string Email,
    string DeliveryAddress,
    DateTime? DateCancelled
) : ICommand<Guid>;