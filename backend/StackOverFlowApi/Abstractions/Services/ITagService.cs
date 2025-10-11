namespace Abstractions.Services;

public interface ITagService
{
    public Task RefreshTags(CancellationToken ct);

    public Task SetTags(CancellationToken ct);
}
