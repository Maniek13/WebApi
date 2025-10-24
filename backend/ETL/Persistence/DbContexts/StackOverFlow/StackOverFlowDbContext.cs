using Domain.Entities.StackOverFlow;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using Abstractions.DbContexts;

namespace Persistence.DbContexts.StackOverFlow;

public class StackOverFlowDbContext : AbstractSOFDbContext
{
    public StackOverFlowDbContext(DbContextOptions<StackOverFlowDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var cfg = ConfigurationHelper.GetConfigurationBuilder();

        optionsBuilder.UseSqlServer(cfg.GetConnectionString("Default"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.GetExecutionAssembly);
    }

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<User> Users => Set<User>();
}
