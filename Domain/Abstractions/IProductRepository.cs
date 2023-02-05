using Domain.Entities;
using Domain.Shared;

namespace Domain.Abstractions;

public interface IProductRepository : IEntityRepository<Product>
{
    Result<Product> Get(Guid id);
}
