using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.OrderItems.Commands.UpdateOrderItem;

public sealed record UpdateOrderItemCommand(Guid OrderItemId, int Quantity) : ICommand<OrderItem>;