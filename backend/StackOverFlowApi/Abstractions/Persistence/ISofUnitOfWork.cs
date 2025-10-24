using Abstractions.DbContexts;
using Abstractions.Interfaces;

namespace Abstractions.Persistence
{
    public interface ISofUnitOfWork<TContext> : IUnitOfWork<AbstractSOFDbContext>
    {
    }
}
