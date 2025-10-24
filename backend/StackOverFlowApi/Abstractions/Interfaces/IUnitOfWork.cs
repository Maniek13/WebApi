using Microsoft.EntityFrameworkCore;

namespace Abstractions.Interfaces;

public interface IUnitOfWork<TContext>
    where TContext : DbContext
{
    TContext Context { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
