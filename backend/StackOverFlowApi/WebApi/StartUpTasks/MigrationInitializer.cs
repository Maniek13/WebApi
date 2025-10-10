using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace WebApi.StartUpTasks;

public class MigrationInitializer
{
    public static void ApplyMigrations(IServiceProvider service)
    {
        using var scope = service.CreateScope();

        scope.ServiceProvider.GetRequiredService<StackOverFlowDbContext>().Database.Migrate();
    }
}
