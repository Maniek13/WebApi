using Abstractions.DbContexts;
using Abstractions.Persistence;

namespace Persistence.UnitsOfWorks;

public sealed class SofUnitOfWork : ISofUnitOfWork<AbstractSOFDbContext>
{
    public AbstractSOFDbContext Context { get; }

    public SofUnitOfWork(AbstractSOFDbContext context)
    {
        Context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}
