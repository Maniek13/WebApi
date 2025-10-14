using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.StartupTasks;

public class MigrationInitializer
{
    public static void ApplyMigrations(IServiceProvider service)
    {
        if (Environment.GetEnvironmentVariable("TestsVariable") == "WebApplicationFactory")
            return;

        using var scope = service.CreateScope();

        scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>().Database.Migrate();
    }
}
