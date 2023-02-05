using Domain.Abstractions;
using Domain.Primitives;

namespace Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IEntityRepository<TEntity>
    where TEntity : Entity
{
    private readonly AppDbContext _dbContext;
    protected RepositoryBase(AppDbContext dbContext) => _dbContext = dbContext;

    public void Insert(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public TEntity Update(TEntity entity) => _dbContext.Update(entity).Entity;

    public bool Delete(Guid entityId)
    {
        TEntity? entity = _dbContext.Find<TEntity>(entityId);
        if (entity is null)
        {
            return false;
        }

        _dbContext.Remove(entity);
        return true;
    }
}
