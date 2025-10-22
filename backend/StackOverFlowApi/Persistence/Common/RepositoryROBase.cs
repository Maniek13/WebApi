using Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Common;

public abstract class RepositoryROBase<TEntity, TDbContext> : IRepositoryROBase<TEntity>
    where TDbContext : DbContext
    where TEntity : class
{
    protected readonly TDbContext _dbContext;

    protected RepositoryROBase(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<bool> CheckHaveData(CancellationToken ct)
    {
        var entities = _dbContext.Set<TEntity>();
        return await entities.AnyAsync(ct);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }
}
