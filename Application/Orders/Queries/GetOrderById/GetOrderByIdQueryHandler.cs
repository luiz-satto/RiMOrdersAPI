using Application.Abstractions.Messaging;
using Dapper;
using Domain.Exceptions;
using System.Data;

namespace Application.Orders.Queries;

internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetOrderByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<OrderResponse> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT T1.Id,
                   T1.Email,
                   T1.DeliveryAddress,
                   T1.CreationDate,
                   T1.DateCancelled,
                   (SELECT COUNT(1) FROM OrderItem AS T2 WHERE T2.OrderId = T1.Id) AS ItemCount
              FROM ""Order"" AS T1
             WHERE T1.Id = @OrderId";

        var orderResponse = await _dbConnection.QueryFirstOrDefaultAsync<OrderResponse>(sql, new { request.OrderId });
        if (orderResponse is null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        return orderResponse;
    }
}
