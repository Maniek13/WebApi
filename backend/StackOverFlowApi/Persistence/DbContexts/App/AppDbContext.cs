using Abstractions.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts.App;

public class AppDbContext : AbstractAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}