using Application.Abstractions.Messaging;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed record CreateOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity) : ICommand<bool>;