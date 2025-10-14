using Abstractions.Startup;
using Hangfire;
using Infrastructure.Filters;
using Infrastructure.Hubs;
using Infrastructure.Jobs;
using Infrastructure.Loging;
using Infrastructure.Midlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Startup;

public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration)
    {

        application.UseMiddleware<ErrorLoggingMiddleware>();

        var hubContext = application.Services.GetRequiredService<IHubContext<LogsHub>>();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(application.Configuration)
            .WriteTo.Sink(new SignalRSink(hubContext))
            .CreateLogger();

        application.MapHub<ChatHub>("/chat");
        application.MapHub<LogsHub>("/logs");

        if (Environment.GetEnvironmentVariable("TestsVariable") == "WebApplicationFactory") return;

        application.UseHangfireDashboard("/dashbord", new DashboardOptions
        {
            Authorization = new[] { new AuthorizationFilter() }
        });

        ConfigureJobs.SetRecurngJobs();
    }
}


