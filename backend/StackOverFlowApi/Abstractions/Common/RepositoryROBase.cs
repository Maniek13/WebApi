using Abstractions.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces;

public abstract class RepositoryROBase<TEntity, TDbContext> : IRepositoryROBase<TEntity>
    where TDbContext : DbContext 
    where TEntity : Entity
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
}
