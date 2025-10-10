using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Configurations;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly);
    }
    public DbSet<Tag> tags => Set<Tag>();
}
