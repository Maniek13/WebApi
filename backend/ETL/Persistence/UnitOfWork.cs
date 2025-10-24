using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AbstractSOFDbContext _dbContext;

    public UnitOfWork(AbstractSOFDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
       await  _dbContext.SaveChangesAsync(cancellationToken);
}
