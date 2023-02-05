using Application.Abstractions.Messaging;

namespace Application.Orders.Commands.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : ICommand<bool>;