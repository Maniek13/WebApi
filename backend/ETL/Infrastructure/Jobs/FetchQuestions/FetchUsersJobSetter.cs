using Abstractions.Jobs;
using Application.Jobs;
using Hangfire;

namespace Infrastructure.Jobs.FetchQuestions;

public class FetchUsersJobSetter : IJobSetter
{
    public void AddRecuringJob()
    {
        RecurringJob.AddOrUpdate<FetchUsersJob>(
                "FetchUsersJob",
                job => job.RunAsync(CancellationToken.None),
                Cron.Daily
            );
    }
}
