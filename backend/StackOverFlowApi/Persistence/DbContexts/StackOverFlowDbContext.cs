using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.DbContexts;

public class StackOverFlowDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        optionsBuilder.UseSqlServer(cfg.GetConnectionString("Default"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        string targetNamespace = "Persistence.Configurations";
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StackOverFlowDbContext).Assembly, type => type.Namespace == targetNamespace);
    }

    public DbSet<Tag> tags => Set<Tag>();
}
