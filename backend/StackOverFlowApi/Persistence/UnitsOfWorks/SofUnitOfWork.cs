using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Persistence.UnitsOfWorks;

public sealed class SofUnitOfWork : IUnitOfWork<AbstractSOFDbContext>
{
    public AbstractSOFDbContext Context { get; }

    public SofUnitOfWork(AbstractSOFDbContext context)
    {
        Context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}
