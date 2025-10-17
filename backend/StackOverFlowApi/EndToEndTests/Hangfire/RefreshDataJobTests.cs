using Application.Jobs;
using EndToEndTests.ApplicationFactory;
using FastEndpoints;
using FluentAssertions;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.MemoryStorage.Dto;
using Hangfire.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using System;
using System.Diagnostics;

namespace EndToEndTests.Hangfire;

public class RefreshDataJobTests : IClassFixture<WebApiWebAplicationFactory>
{
    private readonly WebApiWebAplicationFactory _factory;

    public RefreshDataJobTests(WebApiWebAplicationFactory factory)
    {
        factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task ShouldRefreshTags()
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
