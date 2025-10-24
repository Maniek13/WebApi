using Abstractions.DbContexts;
using Abstractions.Persistence;

namespace Persistence.UnitsOfWorks;

public sealed class AppUnitOfWork : IApiUnitOfWork<AbstractAppDbContext>
{
    public AbstractAppDbContext Context { get; }

    public AppUnitOfWork(AbstractAppDbContext context)
    {
        Context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}
