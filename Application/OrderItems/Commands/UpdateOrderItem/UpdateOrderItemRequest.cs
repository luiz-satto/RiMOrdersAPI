namespace Application.OrderItems.Commands.UpdateOrderItem;

public sealed record UpdateOrderItemRequest(Guid OrderItemId, int Quantity);