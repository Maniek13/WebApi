using Hangfire.Common;
using Hangfire.Server;
using System.Diagnostics;

namespace Infrastructure.Telemetries.Filters;

public class OpenTelemetryHangfireFilter : JobFilterAttribute, IServerFilter
{
    private static readonly ActivitySource MyActivitySource = new ActivitySource("HangfireJobs");

    public void OnPerforming(PerformingContext context)
    {
        var activity = MyActivitySource.StartActivity(context.BackgroundJob.Job.Type.Name);
        context.Items["ot_activity"] = activity;
        activity?.SetTag("job.id", context.BackgroundJob.Id);
        activity?.SetTag("job.queue", context.BackgroundJob.Job.Queue);
    }

    public void OnPerformed(PerformedContext context)
    {
        if (context.Items.TryGetValue("ot_activity", out var obj) && obj is Activity activity)
        {
            var ex = context.Exception;
            if (ex != null)
                activity?.SetTag("job.error", ex.Message);

            activity?.Stop();
        }
    }
}
