using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.HostedServices;

public class StartupSyncHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartupSyncHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scoped = _serviceProvider.CreateScope();
        var dataService = _serviceProvider.GetRequiredService<IStackOverFlowDataService>();
        await dataService.SyncAsync(false, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
