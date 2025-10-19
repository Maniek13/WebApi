using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace Persistence.DbContexts.StackOverFlow;

public class StackOverFlowDbContextFactory : IDesignTimeDbContextFactory<StackOverFlowDbContext>
{
    public StackOverFlowDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StackOverFlowDbContext>();
        var connectionString = ConfigurationHelper.GetConfigurationBuilder().GetConnectionString("Default");
        optionsBuilder.UseSqlServer(connectionString);

        return new StackOverFlowDbContext(optionsBuilder.Options);
    }
}
