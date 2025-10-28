using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts.App;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.StartupTasks;

public class MigrationInitializer
{
    public static void ApplyMigrations(IServiceProvider service)
    {
        using var scope = service.CreateScope();

        scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>().Database.Migrate();
        scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
    }
}
