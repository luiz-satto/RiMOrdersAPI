using Application.Abstractions.Messaging;
using Dapper;
using Domain.Shared;
using System.Data;

namespace Application.Orders.Queries.GetAllOrders;

internal sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderResponse>>
{
    private readonly IDbConnection _dbConnection;

    public GetAllOrdersQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<IEnumerable<OrderResponse>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            DECLARE @PageNumber AS INT SET @PageNumber = @Skip;
            DECLARE @RowsOfPage AS INT SET @RowsOfPage = @Take;

            SELECT T1.Id,
                   T1.Email,
                   T1.DeliveryAddress,
                   T1.CreationDate,
                   T1.DateCancelled,
                   (SELECT COUNT(1) FROM OrderItem AS T2 WHERE T2.OrderId = T1.Id) AS ItemCount
              FROM ""Order"" AS T1
             ORDER BY T1.Id

            OFFSET (@PageNumber - 1) * @RowsOfPage ROWS
             FETCH NEXT @RowsOfPage ROWS ONLY
        ";

        var skip = request.PageNumber > 0 ? request.PageNumber : 1;
        var take = request.RowsOfPage > 0 && request.RowsOfPage > skip ? request.RowsOfPage : skip + 5;
        var orderResponse = await _dbConnection.QueryAsync<OrderResponse>(sql, new { skip, take });
        if (orderResponse is null)
        {
            throw new Exception(Error.NullValue);
        }

        return orderResponse;
    }
}