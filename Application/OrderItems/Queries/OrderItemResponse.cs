namespace Application.OrderItems.Queries.GetOrderItemById;

public sealed record OrderItemResponse(
    Guid Id,
    Guid OrderId,
    int Quantity,
    Guid ProductId,
    string ProductName,
    string ProductDescription,
    double ProductPrice,
    int ProductStock
);
