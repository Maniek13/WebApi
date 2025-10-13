using Abstractions.Jobs;
using Application.Jobs;
using Hangfire;

namespace Infrastructure.Jobs;

public class RefreshDataJobISetter : IJobSetter
{
    public void AddRecuringJob()
    {
        RecurringJob.AddOrUpdate<RefreshDataJob>(
                "RefreshDataJob",
                job => job.RunAsync(CancellationToken.None),
                Cron.Daily
            );
    }
}
