using Abstractions.Startup;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using Infrastructure.Filters;
using Infrastructure.Hubs;
using Infrastructure.Jobs;
using Infrastructure.Loging;
using Infrastructure.Midlewares;
using Infrastructure.Telemetries;
using Infrastructure.Telemetries.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics;

namespace Infrastructure.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration)
    {
        Configurator.AddFilters(application);

        application.UseCors("CorsPolice");

        application.UseMiddleware<ErrorLoggingMiddleware>();

        application.UseRouting();
        application.UseAuthentication();
        application.UseAuthorization();


        application.MapGraphQL("/graphql");

        var hubContext = application.Services.GetRequiredService<IHubContext<LogsHub>>();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(application.Configuration)
            .WriteTo.Sink(new SignalRSink(hubContext))
            .CreateLogger();


        application.MapHub<ChatHub>("/chat");
        application.MapHub<LogsHub>("/logs");
       
   

        application.UseHangfireDashboard("/dashboard", new DashboardOptions
        {
            Authorization = new[] { new AuthorizationFilter() }
        });

        ConfigureJobs.SetRecurngJobs();

        application.UseMiddleware<FastEndpointOTELMidleware>();

    }
}

