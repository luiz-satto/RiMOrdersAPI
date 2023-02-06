using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.OrderItems.Commands.CreateOrderItem;

public sealed record CreateOrderItemCommand(Order Order, Product Product, int Quantity) : ICommand<bool>;