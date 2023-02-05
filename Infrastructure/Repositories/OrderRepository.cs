using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Infrastructure.Repositories;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    private readonly AppDbContext _dbContext;
    public OrderRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Result<Order> Get(Guid id)
    {
        Order? order = _dbContext.Find<Order>(id);
        if (order is null)
        {
            return Result.Failure<Order>(Error.NullValue);
        }

        return Result.Success(order);
    }
}
