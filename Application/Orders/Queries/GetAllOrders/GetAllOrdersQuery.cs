using Application.Abstractions.Messaging;

namespace Application.Orders.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery(int PageNumber, int RowsOfPage) : IQuery<IEnumerable<OrderResponse>>;