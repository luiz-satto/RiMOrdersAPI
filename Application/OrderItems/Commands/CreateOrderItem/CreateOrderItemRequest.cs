using Domain.Entities;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed record CreateOrderItemRequest(Order Order, Product Product, int Quantity);