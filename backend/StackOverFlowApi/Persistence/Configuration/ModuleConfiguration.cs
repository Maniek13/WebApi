using Abstractions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;

namespace Persistence.Configuration;

public class ModuleConfiguration : IModuleConfiguration
{
    public void SetUp(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<StackOverFlowDbContext>();
        builder.Services.AddDbContext<StackOverFlowDbContextRO>();
    }
}
