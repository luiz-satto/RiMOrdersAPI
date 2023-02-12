using Application.Abstractions.Messaging;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed record CreateOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    string ProductName,
    string ProductDescription,
    double ProductPrice,
    int ProductStock,
    int Quantity
) : ICommand<bool>;