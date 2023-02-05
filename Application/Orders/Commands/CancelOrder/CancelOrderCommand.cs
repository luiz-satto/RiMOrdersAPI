using Application.Abstractions.Messaging;

namespace Application.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId, DateTime DateCancelled) : ICommand<bool>;
