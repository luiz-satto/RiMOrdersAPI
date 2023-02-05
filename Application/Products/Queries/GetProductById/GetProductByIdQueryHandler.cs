using Application.Abstractions.Messaging;
using Dapper;
using Domain.Exceptions;
using System.Data;

namespace Application.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetProductByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<ProductResponse> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Name, Description, Price, Stock
              FROM ""Product""
             WHERE Id = @ProductId
        ";

        var product = await _dbConnection.QueryFirstOrDefaultAsync<ProductResponse>(sql, new { request.ProductId });
        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        return product;
    }
}