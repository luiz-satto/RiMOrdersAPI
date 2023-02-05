using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Infrastructure.Repositories;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    private readonly AppDbContext _dbContext;
    public ProductRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Result<Product> Get(Guid id)
    {
        Product? product = _dbContext.Find<Product>(id);
        if (product is null)
        {
            return Result.Failure<Product>(Error.NullValue);
        }

        return Result.Success(product);
    }
}
