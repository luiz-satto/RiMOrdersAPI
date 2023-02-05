using Application.Abstractions.Messaging;

namespace Application.OrderItems.Queries.GetOrderItemById;

public sealed record GetOrderItemsByIdQuery(Guid OrderId) : IQuery<IEnumerable<OrderItemResponse>>;