namespace Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderRequest(
    string Email, 
    string DeliveryAddress, 
    DateTime CreationDate
);