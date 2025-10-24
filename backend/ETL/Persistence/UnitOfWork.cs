using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    public AbstractSOFDbContext Context { get; }
    public UnitOfWork(AbstractSOFDbContext dbContext)
    {
        Context = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
       await  Context.SaveChangesAsync(cancellationToken);
}
