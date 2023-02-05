namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed record CreateOrderItemRequest(Guid OrderId, Guid ProductId, int Quantity);