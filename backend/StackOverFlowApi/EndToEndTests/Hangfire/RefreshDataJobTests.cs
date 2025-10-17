using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Hangfire;
using System.Diagnostics;

namespace EndToEndTests.Endpoints;
public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{

    [Fact]
    public async Task Hangfire_ShouldRefreshTags()
    {
        var jobId = RecurringJob.TriggerJob("RefreshDataJob");
        var connection = JobStorage.Current.GetConnection();

        string? state = null;
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed < TimeSpan.FromSeconds(10))
        {
            var jobData = connection.GetJobData(jobId);
            state = jobData?.State;

            if (state == "Succeeded" || state == "Failed")
                break;

            await Task.Delay(250);
        }

        state.Should().Be("Succeeded");
    }
}
