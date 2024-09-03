using TaskManager.Domain.Core;

namespace TaskManager.Persistence.Core;

public abstract class Repository<TEntity>(ApplicationDbContext dbContext)
    where TEntity : Entity
{
    public async Task AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;

        await dbContext.Set<TEntity>()
            .AddAsync(entity);
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities) entity.CreatedAt = DateTime.UtcNow;

        await dbContext.Set<TEntity>()
            .AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>()
            .Remove(entity);
    }

    public void RemoveRange(List<TEntity> entities)
    {
        dbContext.Set<TEntity>()
            .RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        dbContext.Set<TEntity>()
            .Update(entity);
    }

    public void UpdateRange(List<TEntity> entities)
    {
        foreach (var entity in entities) entity.UpdatedAt = DateTime.UtcNow;

        dbContext.Set<TEntity>()
            .UpdateRange(entities);
    }
}