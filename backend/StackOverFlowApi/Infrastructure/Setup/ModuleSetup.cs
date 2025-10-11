using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Setup;
using Application.Interfaces;
using Configuration.ExternalApies;
using Infrastructure.Api;
using Infrastructure.Cache;
using Infrastructure.HostedServices;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        builder.Services.Configure<StackOverFlowOptions>(
            builder.Configuration.GetSection("ExternalApies:StackOverFlow"));

        builder.Services.AddSingleton<ICacheVersionService, CacheVersionService>();
        builder.Services.AddScoped<IStackOverFlowDataService, StackOverFlowDataService>();
        builder.Services.AddHttpClient<IStackOverFlowApiClient, StackOverFlowApiClient>();
        builder.Services.AddHostedService<StartupSyncHostedService>();
        builder.Services.AddMemoryCache();
    }
}
