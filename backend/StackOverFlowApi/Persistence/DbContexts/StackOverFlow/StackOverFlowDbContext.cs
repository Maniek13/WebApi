using Abstractions.DbContexts;
using Domain.Entities.StackOverFlow;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.DbContexts.StackOverFlow;

public class StackOverFlowDbContext : AbstractSOFDbContext
{
    public StackOverFlowDbContext(DbContextOptions<StackOverFlowDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddTransactionalOutboxEntities();
        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.GetExecutionAssembly);
    }

    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<User> Users => Set<User>();
}
