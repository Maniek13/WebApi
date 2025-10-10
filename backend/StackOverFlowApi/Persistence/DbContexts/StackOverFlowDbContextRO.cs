using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts;

public class StackOverFlowDbContextRO : DbContext
{
    public override int SaveChanges() => throw new NotImplementedException();
    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new NotImplementedException();
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
    
    public IQueryable<Tag> tags => Set<Tag>().AsNoTracking();
}
