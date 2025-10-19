using Abstractions.Jobs;
using Application.Jobs;
using Hangfire;

namespace Infrastructure.Jobs.FetchQuestions;

public class FetchQuestionsJobSetter : IJobSetter
{
    public void AddRecuringJob()
    {
        RecurringJob.AddOrUpdate<FetchQuestionsJob>(
                "FetchQuestionsJob",
                job => job.RunAsync(CancellationToken.None),
                Cron.Daily
            );
    }
}
