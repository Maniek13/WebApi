using Abstractions.Startup;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
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
using System.Diagnostics;

namespace Infrastructure.Startup;
public class OpenTelemetryHangfireFilter : JobFilterAttribute, IServerFilter
{
    private static readonly ActivitySource MyActivitySource = new ActivitySource("HangfireJobs");

    public void OnPerforming(PerformingContext context)
    {
        var activity = MyActivitySource.StartActivity(context.BackgroundJob.Job.Type.Name);
        context.Items["ot_activity"] = activity;
        activity?.SetTag("job.id", context.BackgroundJob.Id);
    }

    public void OnPerformed(PerformedContext context)
    {
        if (context.Items.TryGetValue("ot_activity", out var obj) && obj is Activity activity)
        {
            activity?.Stop();
        }
    }
}
public class ModuleStartup : IModuleStartup
{
    public void Startup(WebApplication application, IConfiguration configuration)
    {
        GlobalJobFilters.Filters.Add(new OpenTelemetryHangfireFilter());

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

   

        application.UseHangfireDashboard("/dashbord", new DashboardOptions
        {
            Authorization = new[] { new AuthorizationFilter() }
        });

        ConfigureJobs.SetRecurngJobs();

    }
}

