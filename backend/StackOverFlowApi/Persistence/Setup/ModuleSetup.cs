using Abstractions.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;

namespace Persistence.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<StackOverFlowDbContext>();
        builder.Services.AddDbContext<StackOverFlowDbContextRO>();
    }
}
