using Domain.Primitives;

namespace Domain.Abstractions;

public interface IEntityRepository<TEntity>
    where TEntity : Entity
{
    void Insert(TEntity entity);
    TEntity Update(TEntity entity);
    bool Delete(Guid id);
}
