using Application.Abstractions.Messaging;
using Dapper;
using Domain.Exceptions;
using System.Data;

namespace Application.OrderItems.Queries.GetOrderItemById;

internal sealed class GetOrderItemsByIdQueryHandler : IQueryHandler<GetOrderItemsByIdQuery, IEnumerable<OrderItemResponse>>
{
    private readonly IDbConnection _dbConnection;
    public GetOrderItemsByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<IEnumerable<OrderItemResponse>> Handle(
        GetOrderItemsByIdQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT T1.Id,
                   T1.OrderId,
                   T1.Quantity,
                   T1.ProductId,
                   T2.Name AS ProductName,
                   T2.Description AS ProductDescription,
                   T2.Price AS ProductPrice,
                   T2.Stock AS ProductStock
              FROM ""OrderItem"" T1
              JOIN ""Product"" T2 ON T1.ProductId = T2.Id
             WHERE T1.OrderId = @OrderId
        ";

        var orderItems = await _dbConnection.QueryAsync<OrderItemResponse>(sql, new { request.OrderId });
        if (orderItems is null)
        {
            throw new OrderItemNotFoundException(request.OrderId);
        }

        return orderItems;
    }
}