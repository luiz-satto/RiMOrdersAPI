using Application.Abstractions.Messaging;

namespace Application.Orders.Queries;

public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderResponse>;