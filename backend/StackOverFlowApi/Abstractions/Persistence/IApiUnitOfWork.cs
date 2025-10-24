using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Abstractions.Persistence
{
    public interface IApiUnitOfWork<TContext> : IUnitOfWork<AbstractAppDbContext>
    {
    }
}
