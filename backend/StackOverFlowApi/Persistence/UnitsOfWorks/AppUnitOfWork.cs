using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Persistence.UnitsOfWorks;

public sealed class AppUnitOfWork : IUnitOfWork<AbstractAppDbContext>
{
    public AbstractAppDbContext Context { get; }

    public AppUnitOfWork(AbstractAppDbContext context)
    {
        Context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}
