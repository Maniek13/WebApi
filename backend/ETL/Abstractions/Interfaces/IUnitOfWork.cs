using Abstractions.DbContexts;

namespace Abstractions.Interfaces;

public interface IUnitOfWork
{
    AbstractSOFDbContext Context { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
