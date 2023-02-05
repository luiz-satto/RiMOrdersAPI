using Application.Abstractions.Messaging;

namespace Application.OrderItems.Commands.DeleteOrderItem;

public sealed record DeleteOrderItemCommand(Guid OrderItemId) : ICommand<bool>;