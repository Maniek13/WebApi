using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts.StackOverFlow;

public class StackOverFlowDbContextRO : DbContext
{
    public StackOverFlowDbContextRO(DbContextOptions<StackOverFlowDbContextRO> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.GetExecutionAssembly);
    }
    public override int SaveChanges() => throw new NotImplementedException();
    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new NotImplementedException();
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
    
    public IQueryable<Tag> Tags => Set<Tag>().AsNoTracking();
    public IQueryable<Question> Questions => Set<Question>().AsNoTracking();
}
