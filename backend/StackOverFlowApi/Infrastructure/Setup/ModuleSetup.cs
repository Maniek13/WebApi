using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.ExternalApies;
using Abstractions.Setup;
using Application.Interfaces.StackOverFlow;
using Hangfire;
using Infrastructure.Api;
using Infrastructure.Services.CacheServices;
using Infrastructure.Services.DataServices;
using Infrastructure.Services.HostedServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Setup;

public class ModuleSetup : IModuleSetup
{
    public void Setup(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(builder.Configuration)
            .CreateLogger();

        builder.Services.Configure<StackOverFlowOptions>(
            builder.Configuration.GetSection("ExternalApies:StackOverFlow"));

        builder.Services.AddSingleton<ICacheVersionService, CacheVersionService>();
        builder.Services.AddHttpClient<IStackOverFlowApiClient, StackOverFlowApiClient>();
        builder.Services.AddScoped<IStackOverFlowDataService, StackOverFlowDataService>();
        
        builder.Services.AddHostedService<StartupSyncHostedService>();
        builder.Services.AddMemoryCache();

        builder.Host.UseSerilog();

        if (Environment.GetEnvironmentVariable("TestsVariable") == "WebApplicationFactory") return;

        builder.Services.AddHangfire(c =>
        {
            c.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"));
        });
        builder.Services.AddHangfireServer();
    }
}
