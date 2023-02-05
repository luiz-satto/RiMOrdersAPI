using Domain.Entities;
using Domain.Shared;

namespace Domain.Abstractions;

public interface IOrderRepository : IEntityRepository<Order>
{
    Result<Order> Get(Guid id);
}
