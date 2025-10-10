using Abstractions.Repositories;
using Abstractions.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Persistence.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<StackOverFlowDbContext>();
        builder.Services.AddDbContext<StackOverFlowDbContextRO>();

        builder.Services.AddScoped<ITagsRepository, TagsRepository>();
        builder.Services.AddScoped<ITagsRepositoryRO, TagsRepositoryRO>();
    }
}
