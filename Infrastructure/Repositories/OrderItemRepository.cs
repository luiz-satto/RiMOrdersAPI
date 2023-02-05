using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Infrastructure.Repositories;

public sealed class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
{
    private readonly AppDbContext _dbContext;
    public OrderItemRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Result<OrderItem> Get(Guid id)
    {
        OrderItem? orderItem = _dbContext.Find<OrderItem>(id);
        if (orderItem is null)
        {
            return Result.Failure<OrderItem>(Error.NullValue);
        }

        return Result.Success(orderItem);
    }
}
