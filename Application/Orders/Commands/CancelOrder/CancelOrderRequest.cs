namespace Application.Orders.Commands.CancelOrder;

public sealed record CancelOrderRequest(Guid OrderId, DateTime DateCancelled);