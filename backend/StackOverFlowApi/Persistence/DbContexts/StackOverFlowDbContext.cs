using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.DbContexts;

public class StackOverFlowDbContext : DbContext
{
    public StackOverFlowDbContext(DbContextOptions<StackOverFlowDbContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Environment.GetEnvironmentVariable("TestsVariable") == "InMemoryDatabase" ||
            Environment.GetEnvironmentVariable("TestsVariable") == "WebApplicationFactory" ||
            Environment.GetEnvironmentVariable("TestsVariable") == "IntegrationTests") return;

        var cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        optionsBuilder.UseSqlServer(cfg.GetConnectionString("Default"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.GetExecutionAssembly);
    }

    public DbSet<Tag> tags => Set<Tag>();
}
