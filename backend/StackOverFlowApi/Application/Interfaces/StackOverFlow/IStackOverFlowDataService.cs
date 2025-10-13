namespace Application.Interfaces.StackOverFlow;

public interface IStackOverFlowDataService
{
    Task SyncAsync(bool forceRefresh = true, CancellationToken cancellationToken = new CancellationToken());
}
