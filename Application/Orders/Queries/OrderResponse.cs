namespace Application.Orders.Queries;

public sealed record OrderResponse(
    Guid Id,
    string Email,
    string DeliveryAddress,
    DateTime CreationDate,
    DateTime? DateCancelled,
    int ItemCount
);
