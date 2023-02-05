using Domain.Entities;
using Domain.Shared;

namespace Domain.Abstractions;

public interface IOrderItemRepository : IEntityRepository<OrderItem>
{
    Result<OrderItem> Get(Guid id);
}
